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
    
}
