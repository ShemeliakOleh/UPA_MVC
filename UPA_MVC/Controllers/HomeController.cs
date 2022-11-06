using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UPA_MVC.Models;

namespace UPA_MVC.Controllers;
public class HomeController : Controller
{
    private readonly TestService _testService;
    public HomeController(TestService testService)
    {
        _testService = testService;
    }

    public IActionResult Index()
    {
        return View(_testService.GetTests().ToList());
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
