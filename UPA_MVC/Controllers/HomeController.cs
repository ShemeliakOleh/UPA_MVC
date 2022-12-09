﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UPA_MVC.Models;

namespace UPA_MVC.Controllers;
public class HomeController : Controller
{

    public HomeController()
    {
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
