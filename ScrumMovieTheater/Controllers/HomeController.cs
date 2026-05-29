using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
