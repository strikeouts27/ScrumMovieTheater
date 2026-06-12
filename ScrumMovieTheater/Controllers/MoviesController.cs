using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Controllers
{
    public class MoviesController : Controller
    {
        //private readonly AppDbContext _context;

        //public MoviesController(AppDbContext context)
        //{
        //    //_context = context;
        //}

        public IActionResult Index()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "John Wick",
                    Description = "the john wick movie"
                },
                new Movie
                {
                    Title = "The Pokemon movie",
                    Description = "the one where mewtwo gets angy"
                }
            };
                    return View(movies);
        }
    }
}