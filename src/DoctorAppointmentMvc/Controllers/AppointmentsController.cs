using System.Security.Claims;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers;
[Authorize]
public class AppointmentsController(IAppointmentService appointments, IDoctorService doctors) : Controller
{
    public async Task<IActionResult> Index() => View(await appointments.GetForPatientAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

    [HttpGet] public async Task<IActionResult> Create(int doctorId)
    {
        var doctor = await doctors.GetByIdAsync(doctorId);
        if (doctor is null) return NotFound();
        return View(new BookAppointmentViewModel { DoctorId = doctor.Id, DoctorName = doctor.FullName, AppointmentDate = DateTime.Today.AddDays(1) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookAppointmentViewModel model)
    {
        var doctor = await doctors.GetByIdAsync(model.DoctorId);
        if (doctor is null) return NotFound();
        model.DoctorName = doctor.FullName;
        if (!new[] { "09:00 AM", "10:00 AM", "11:00 AM", "01:00 PM", "02:00 PM", "03:00 PM" }.Contains(model.TimeSlot))
            ModelState.AddModelError(nameof(model.TimeSlot), "Select a valid time slot.");
        if (!ModelState.IsValid) return View(model);
        var result = await appointments.BookAsync(new Appointment {
            DoctorId = model.DoctorId, PatientId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            AppointmentDate = model.AppointmentDate.Date, TimeSlot = model.TimeSlot, Reason = model.Reason
        });
        if (!result.Success) { ModelState.AddModelError("", result.Message); return View(model); }
        TempData["Success"] = result.Message;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var list = await appointments.GetForPatientAsync(patientId);
        var item = list.FirstOrDefault(a => a.Id == id);
        if (item is null) return NotFound();
        if (item.AppointmentDate.Date < DateTime.Today || item.Status == AppointmentStatus.Completed) return BadRequest();
        item.Status = AppointmentStatus.Cancelled;
        // Service method intentionally keeps data access encapsulated in this sample; cancellation is handled in next iteration.
        TempData["Info"] = "Cancellation workflow is a sample placeholder. Contact the clinic to cancel.";
        return RedirectToAction(nameof(Index));
    }
}
