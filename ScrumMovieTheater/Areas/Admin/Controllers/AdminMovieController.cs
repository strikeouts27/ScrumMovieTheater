using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Services;

namespace ScrumMovieTheater.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminMovieController : Controller
{

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

        //movieCatalog.Add(model);

        return RedirectToAction("Index", "AdminMovies");
    }
}
