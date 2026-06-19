using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Models; // or where your models are
using ScrumMovieTheater.Data;   // IMPORTANT (this is AppDbContext location)

namespace ScrumMovieTheater.Controllers
{
    public class ShowtimesController : Controller
    {
        private readonly AppDbContext _context;

        public ShowtimesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var showtimes = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .ToList();

            return View(showtimes);
        }
    }
}