using DoctorAppointmentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers;
public class AccountController(UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn) : Controller
{
    [HttpGet] public IActionResult Register() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
        var result = await users.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await users.AddToRoleAsync(user, "Patient");
            await signIn.SignInAsync(user, false);
            return RedirectToAction("Index", "Appointments");
        }
        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        return View(model);
    }
    [HttpGet] public IActionResult Login(string? returnUrl = null) { ViewBag.ReturnUrl = returnUrl; return View(); }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await signIn.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
        if (result.Succeeded) return LocalRedirect(Url.IsLocalUrl(returnUrl) ? returnUrl! : Url.Action("Index", "Appointments")!);
        ModelState.AddModelError("", "Invalid login attempt.");
        return View(model);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() { await signIn.SignOutAsync(); return RedirectToAction("Index", "Home"); }
    public IActionResult AccessDenied() => View();
}
