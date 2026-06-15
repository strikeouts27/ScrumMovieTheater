using Microsoft.AspNetCore.Mvc;

namespace ScrumMovieTheater.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddMovie()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        return RedirectToAction("Manager", "Movie");
    }
}