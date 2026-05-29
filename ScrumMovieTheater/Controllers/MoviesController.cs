using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Services;

namespace ScrumMovieTheater.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieCatalog movieCatalog;

    public MoviesController(IMovieCatalog movieCatalog)
    {
        this.movieCatalog = movieCatalog;
    }

    public IActionResult Index()
    {
        return View(movieCatalog.GetAll());
    }
}
