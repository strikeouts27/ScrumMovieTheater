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

   
    [HttpGet]
    public IActionResult Manager()
    {
        return View();
    }

   
    [HttpGet]
    public IActionResult EditMovie(int id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditMovie(MovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        return RedirectToAction("Manager");
    }


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