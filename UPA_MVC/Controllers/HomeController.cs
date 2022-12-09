using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UPA_MVC.ViewModels;

namespace UPA_MVC.Controllers;
public class HomeController : Controller
{
    private readonly Application.ServiceProvider _services;
    public HomeController(Application.ServiceProvider services)
    {
        _services = services;
    }
    public IActionResult Index()
    {
        if (User.IsInRole("Admin"))
        {
            return RedirectToAction("Index", "Admin");
        }
        if (User.IsInRole("User"))
        {
            return RedirectToAction("Index", "User");
        }
        return View();
    }
    public IActionResult DeletedUser()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult ResetPassword(string userId, string code)
    {
        var user = _services._accountService.GetUser(new Guid(userId));
        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }
        List<string> emailAndExpiration = SymmetricEncryptor.DecryptToString(Convert.FromBase64String(code), user.Id.ToString().Replace("-", "")).Split('*').ToList();
        if (DateTime.TryParse(emailAndExpiration[1], out _))
        {

            if (DateTime.Parse(emailAndExpiration[1]) >= DateTime.Now)
            {
                HttpContext.Session.SetString("userId", userId);
                return RedirectToAction("ForgotPassword");
            }
            else
            {
                return RedirectToAction("ResetPasswordExpired", "Account");
            }
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]
    public IActionResult ForgotPassword(string newPassword, string newPasswordConfirm)
    {
        string userId = HttpContext.Session.GetString("userId");
        var user = _services._accountService.GetUser(new Guid(userId));
        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            if (newPassword == newPasswordConfirm)
            {
                _services._accountService.UpdateUser(user.Id, "Password", _services._accountService.ComputeHash(newPassword));
                return RedirectToAction("PasswordChangedSuccessfully", "Account");
            }
            else
            {
                ViewBag.PasswordsDoesntMatch = true;
                return View();
            }
        }
    }
}
