using Microsoft.AspNetCore.Mvc;
    
namespace ScrumMovieTheater.Controllers;

public class MoviesController : Controller
{   


    public IActionResult Index()
    {
        return View();
    }
}
