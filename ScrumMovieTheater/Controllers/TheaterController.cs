using Microsoft.AspNetCore.Mvc;
    
namespace ScrumMovieTheater.Controllers;

public class TheaterController : Controller
{   
    public IActionResult Index()
    {
        return View();
    }
}