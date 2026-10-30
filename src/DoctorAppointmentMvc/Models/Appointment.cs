using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentSystem.Models;

public enum AppointmentStatus { Requested, Confirmed, Cancelled, Completed }

public class Appointment
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    [Required] public string PatientId { get; set; } = "";
    public ApplicationUser? Patient { get; set; }
    [Required] public DateTime AppointmentDate { get; set; }
    [Required, MaxLength(20)] public string TimeSlot { get; set; } = "";
    [MaxLength(1000)] public string? Reason { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Requested;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
