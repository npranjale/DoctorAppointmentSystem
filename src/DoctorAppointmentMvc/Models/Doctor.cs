using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentSystem.Models;

public class Doctor
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string FullName { get; set; } = "";
    [Required, MaxLength(80)] public string Specialty { get; set; } = "";
    [MaxLength(500)] public string Bio { get; set; } = "";
    [MaxLength(200)] public string Clinic { get; set; } = "";
    [Range(0, 1000)] public decimal ConsultationFee { get; set; }
    [MaxLength(300)] public string ImagePath { get; set; } = "/images/doctor-placeholder.svg";
    public bool IsActive { get; set; } = true;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
