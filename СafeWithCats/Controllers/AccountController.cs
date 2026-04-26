using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using CafeWithCats.Models.ViewModels;

namespace CafeWithCats.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var result = await _signInManager.PasswordSignInAsync(
            vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Invalid email or password.");
        return View(vm);
    }

    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var user = new AppUser
        {
            UserName = vm.Email,
            Email = vm.Email,
            FullName = vm.FullName
        };

        var result = await _userManager.CreateAsync(user, vm.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied() => View();
}