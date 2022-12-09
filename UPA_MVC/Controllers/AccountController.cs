using Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UPA_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace UPA_MVC.Controllers;
public class AccountController : Controller
{
    private readonly Application.ServiceProvider _services;
    public AccountController(Application.ServiceProvider services)
    {
        _services = services;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
    {
        if (ModelState.IsValid)
        {
            User user = await _services._accountService.GetUserbyEmail(model.Email);
            if (user != null && user.PasswordHash == _services._accountService.ComputeHash(model.Password) && user.Role != null)
            {
                await Authenticate(user);

                if (_services._accountService.IsEmailConfirmed(user.Id))
                {
                    return RedirectToAction("ConfirmEmail", "Account");
                }
                string altitudeString = Request.Query.FirstOrDefault(p => p.Key == "ReturnUrl").Value;
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    if (user.Role.RoleName == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    if (user.Role.RoleName == "User")
                    {
                        return RedirectToAction("Index", "User");
                    }
                    return RedirectToAction("Index", "Home");

                }
            }
            ModelState.AddModelError("", "Incorrect login or password");
        }
        return View(model);
    }
    private async Task Authenticate(User user)
    {

        var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName)
            };
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        ViewBag.IncorrectEmail = false;
        if (userId == null || code == null)
        {
            return View();
        }
        var user = _services._accountService.GetUser(new Guid(userId));
        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }
        if (user.Email == SymmetricEncryptor.DecryptToString(Convert.FromBase64String(code), user.Id.ToString().Replace("-", "")))
        {
            bool result = _services._accountService.ConfirmEmail<User>("Users", new Guid(userId));
            if (result)
            {
                await Authenticate(user);
                if (User.IsInRole("Admin"))
                {
                    ViewBag.UserName = User.Identity.Name;
                    return RedirectToAction("Index", "Admin");
                }
                if (User.IsInRole("User"))
                {
                    ViewBag.UserName = User.Identity.Name;
                    return RedirectToAction("Index", "User");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string email)
    {
        if (email == null)
        {
            ViewBag.IncorrectEmail = true;
            return View();
        }
        User user = await _services._accountService.GetUserbyEmail(email);
        ViewBag.IncorrectEmail = false;
        if (user != null)
        {
            var bytes = SymmetricEncryptor.EncryptString(user.Email, user.Id.ToString().Replace("-", ""));
            string base64Encoded = Convert.ToBase64String(bytes);

            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id.ToString(), code = base64Encoded },
                protocol: HttpContext.Request.Scheme);
            return RedirectToAction("SendEmailConfirmation", new { toAddress = user.Email, callbackUrl = callbackUrl });
        }
        else
        {
            ViewBag.IncorrectEmail = true;
            return View();
        }
    }

    public IActionResult Index()
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

}
