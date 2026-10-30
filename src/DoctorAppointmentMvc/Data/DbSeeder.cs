using DoctorAppointmentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        if (!await db.Doctors.AnyAsync())
        {
            db.Doctors.AddRange(
                new Doctor { FullName = "Dr. Mark Harris", Specialty = "General Physician", Bio = "Preventive care, annual physicals and everyday health concerns.", Clinic = "Sacramento Medical Center", ConsultationFee = 20, ImagePath = "/images/doctor-mark.svg" },
                new Doctor { FullName = "Dr. Tom Smith", Specialty = "Gynecology", Bio = "Child birth, women care, pregnency risk assessment.", Clinic = "Elk Grove Medical Center", ConsultationFee = 30, ImagePath = "/images/doctor-tom.svg" },
                new Doctor { FullName = "Dr. Sandra Armstong", Specialty = "Dermatology", Bio = "Skin health, acne, eczema and routine dermatological consultations.", Clinic = "Florin Specialty Clinic", ConsultationFee = 20, ImagePath = "/images/doctor-sandra.svg" }
            );
            await db.SaveChangesAsync();
        }

        var users = services.GetRequiredService<UserManager<ApplicationUser>>();
        foreach (var role in new[] { "Patient", "Admin" })
            if (!await services.GetRequiredService<RoleManager<IdentityRole>>().RoleExistsAsync(role))
                await services.GetRequiredService<RoleManager<IdentityRole>>().CreateAsync(new IdentityRole(role));

        const string email = "patient@example.com";
        if (await users.FindByEmailAsync(email) is null)
        {
            var user = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true, FullName = "Sample Patient" };
            var result = await users.CreateAsync(user, "Patient@12345");
            if (result.Succeeded) await users.AddToRoleAsync(user, "Patient");
        }
    }
}
