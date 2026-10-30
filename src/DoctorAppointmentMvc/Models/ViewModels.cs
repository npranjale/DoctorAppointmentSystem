using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentSystem.Models;

public class RegisterViewModel
{
    [Required, StringLength(100)] public string FullName { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, DataType(DataType.Password), MinLength(10)] public string Password { get; set; } = "";
    [Required, DataType(DataType.Password), Compare(nameof(Password))] public string ConfirmPassword { get; set; } = "";
}
public class LoginViewModel
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
    public bool RememberMe { get; set; }
}
public class BookAppointmentViewModel
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = "";
    [Required, DataType(DataType.Date)] public DateTime AppointmentDate { get; set; } = DateTime.Today.AddDays(1);
    [Required] public string TimeSlot { get; set; } = "";
    [StringLength(1000)] public string? Reason { get; set; }
}
