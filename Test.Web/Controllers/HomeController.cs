using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Test.Entity.Data;
using Test.Web.Attributes;
using Test.Web.Models;

namespace Test.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

[CustomAuthorize("Admin","User")]
    public IActionResult MainPage(User user)
    {
        return View(user);
    }


[CustomAuthorize("Admin")]
    public IActionResult AdminPage()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
