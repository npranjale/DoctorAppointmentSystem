using DoctorAppointmentSystem.Data;
using DoctorAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetForPatientAsync(string patientId);
    Task<(bool Success, string Message)> BookAsync(Appointment appointment);
}
public class AppointmentService(ApplicationDbContext db) : IAppointmentService
{
    public Task<List<Appointment>> GetForPatientAsync(string patientId) =>
        db.Appointments.Include(a => a.Doctor).Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate).ToListAsync();

    public async Task<(bool Success, string Message)> BookAsync(Appointment appointment)
    {
        if (appointment.AppointmentDate.Date <= DateTime.Today)
            return (false, "Choose a future date.");
        var doctorExists = await db.Doctors.AnyAsync(d => d.Id == appointment.DoctorId && d.IsActive);
        if (!doctorExists) return (false, "The selected doctor is not available.");
        var occupied = await db.Appointments.AnyAsync(a => a.DoctorId == appointment.DoctorId
            && a.AppointmentDate.Date == appointment.AppointmentDate.Date
            && a.TimeSlot == appointment.TimeSlot && a.Status != AppointmentStatus.Cancelled);
        if (occupied) return (false, "That time slot has already been requested. Please choose another.");
        db.Appointments.Add(appointment);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { return (false, "That slot was just taken. Please choose another."); }
        return (true, "Appointment request submitted.");
    }
}
