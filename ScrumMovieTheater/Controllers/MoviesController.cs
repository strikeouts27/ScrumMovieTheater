using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;
using System.Linq;

namespace ScrumMovieTheater.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string q)
        {
            var movies = _context.Movies.ToList();

            if (!string.IsNullOrEmpty(q))
            {
                movies = movies
                    .Where(m => m.Title.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(movies);
        }
    }
}