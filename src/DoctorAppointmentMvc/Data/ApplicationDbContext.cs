using DoctorAppointmentSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Doctor>().Property(x => x.ConsultationFee).HasPrecision(10, 2);
        builder.Entity<Appointment>()
            .HasOne(x => x.Doctor).WithMany(x => x.Appointments).HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Appointment>()
            .HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Appointment>().HasIndex(x => new { x.DoctorId, x.AppointmentDate, x.TimeSlot })
            .IsUnique();
    }
}
