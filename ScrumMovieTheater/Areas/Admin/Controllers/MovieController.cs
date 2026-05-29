using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Services;

namespace ScrumMovieTheater.Areas.Admin.Controllers;

[Area("Admin")]
public class MovieController : Controller
{
    private readonly IMovieCatalog movieCatalog;

    public MovieController(IMovieCatalog movieCatalog)
    {
        this.movieCatalog = movieCatalog;
    }

// http://localhost:5013/Admin/Movie/AddMovie 
    [HttpGet]
    public IActionResult AddMovie()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddMovie(MovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        movieCatalog.Add(model);

        return RedirectToAction("Index", "Movies");
    }
}
