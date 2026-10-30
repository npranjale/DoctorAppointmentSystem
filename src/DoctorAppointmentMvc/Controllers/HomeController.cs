using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Services;

namespace DoctorAppointmentSystem.Controllers;
public class HomeController(IDoctorService doctors) : Controller
{
    public async Task<IActionResult> Index() => View(await doctors.GetActiveAsync());
    public IActionResult Error() => View();
}
