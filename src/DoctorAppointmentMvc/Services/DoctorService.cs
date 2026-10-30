using DoctorAppointmentSystem.Data;
using DoctorAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Services;
public interface IDoctorService { Task<List<Doctor>> GetActiveAsync(); Task<Doctor?> GetByIdAsync(int id); }
public class DoctorService(ApplicationDbContext db) : IDoctorService
{
    public Task<List<Doctor>> GetActiveAsync() => db.Doctors.Where(d => d.IsActive).OrderBy(d => d.Specialty).ToListAsync();
    public Task<Doctor?> GetByIdAsync(int id) => db.Doctors.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
}
