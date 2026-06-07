using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Data;

namespace ScrumMovieTheater.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }
    }
}