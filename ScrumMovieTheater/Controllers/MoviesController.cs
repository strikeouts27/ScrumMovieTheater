using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.DTOs;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Services;

namespace ScrumMovieTheater.Controllers
{
    public class MoviesController : Controller
    {
        //private readonly AppDbContext _context;

        //public MoviesController(AppDbContext context)
        //{
        //    //_context = context;
        //}

        private readonly MovieDataService _movieDataService;

        public MoviesController(MovieDataService movieDataService)
        {
            _movieDataService = movieDataService;
        }


        // Create a constructor using a service that you have to create for method usage.
        // write the service 
        // than come back the HTTP method and establish a link between the two sections. 
        // using that link call the method that you wrote in the service for retreiving information. 
        // if writing from front to back you will be using stuff that doesn't exist yet. 


        // GET ALL() && GetById(id)
        /* 
        1. create a varaible that can hold the container. 
        2. create a constructor to create the object that can hold the data. 
        3. We want to create a get route so we specify the HTTP GET tag/attribute. 
        4. We 
        */



        [HttpGet]
        // This is a get method 
        public async Task<IActionResult> Index()
        {
            List<MovieDTO> movies = await _movieDataService.GetMoviesAsync();
            // we use <> for templates. 
            // new movie list view model. 1. use a viewmodel type 2. name the variable. 3 use the new keyword 4. call the list of movies, by calling the constructor you create an object with the infomration you need. 
            MovieListViewModel movieTemplate = new MovieListViewModel(movies);
            // it should now return the movieTemplate for movie View. 
            return View(movieTemplate);
        }

        // This method is differnt in that we are trying to filter the information from what it gathers. 
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoviesByTheater([FromRoute]int id) {
            // getting a list of movies 
            List<MovieDTO> movies = await _movieDataService.GetMoviesByTheaterAsync(id);
            MovieListViewModel movieTemplate = new MovieListViewModel(movies); 
            return View(movieTemplate); 

            // filter by theater 
            
        }

    }
}