using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.DTOs;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Services;


namespace ScrumMovieTheater.Controllers;

public class TheaterController : Controller
{   
    private readonly TheaterDataService _theaterDataService; 
    // once you create this you need to tell Program.cs that this service exists. 
    public TheaterController(TheaterDataService theaterData)
    {
        _theaterDataService = theaterData; 
    }

    [HttpGet]
    // This is a get method 
    public async Task<IActionResult> Index()
    {
        List<TheaterDTO> theaters = await _theaterDataService.GetTheatersAsync();
        // we use <> for templates. 
        // new movie list view model. 1. use a viewmodel type 2. name the variable. 3 use the new keyword 4. call the list of movies, by calling the constructor you create an object with the infomration you need. 
        TheaterListViewModel theaterTemplate = new TheaterListViewModel(theaters);
        return View(theaterTemplate); 
    }
}