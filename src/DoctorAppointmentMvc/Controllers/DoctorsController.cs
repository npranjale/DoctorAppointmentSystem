using DoctorAppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers;
public class DoctorsController(IDoctorService service) : Controller
{
    public async Task<IActionResult> Index() => View(await service.GetActiveAsync());
    public async Task<IActionResult> Details(int id)
    {
        var doctor = await service.GetByIdAsync(id);
        return doctor is null ? NotFound() : View(doctor);
    }
}
