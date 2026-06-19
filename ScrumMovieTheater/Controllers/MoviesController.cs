using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        //  Show all movies
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        //  Movie details

       
       public IActionResult Details(int id)
        {
           var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);

           if (movie == null)
             return NotFound();

           return View(movie);
        }
    }
}