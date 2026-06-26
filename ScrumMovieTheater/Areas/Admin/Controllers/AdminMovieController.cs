using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security;
using ScrumMovieTheater.Controllers;
using ScrumMovieTheater.DTOs;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Services;

 

namespace ScrumMovieTheater.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminMovieController : Controller
{
    private readonly IMovieCatalog movieCatalog;

    public AdminMovieController(IMovieCatalog movieCatalog)
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


    private readonly MovieDataService _movieDataService;

    // its a good idea to pull up another controller and use visual studio codes split view. 
     //_movieDataService = movieDataService;
    // }public AdminMovieController(MovieDataService movieDataService)
    // {
       


    // [HttpGet]
    //  public async Task<IActionResult> Manager()
    // {
    //     List<MovieDTO> movies = await _movieDataService.GetMoviesAsync();
    //     MovieListViewModel movieTemplate = new MovieListViewModel(movies); 
    //     return View(movieTemplate);
    // }

   
    // [HttpGet]
    // public IActionResult EditMovie(int id)
    // {
    //     return View();
    // }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult EditMovie(MovieViewModel model)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return View(model);
    //     }
    //     return RedirectToAction("Manager");
    // }


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
    public IActionResult Ticketing()

    {

        return View();

    }

    public IActionResult DeleteMovie()

    {

        return View();

    }





}