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

       
       public IActionResult Index(int? theaterId, DateTime? showDate)
        {
            var showtimes = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .AsQueryable();

            // FILTER: theater
            if (theaterId.HasValue)
            {
                showtimes = showtimes.Where(s => s.TheaterId == theaterId);
            }

           // FILTER: date
            if (showDate.HasValue)
            {
                showtimes = showtimes.Where(s => s.ShowDate == showDate);
            }

           // dropdown data
            ViewBag.Theaters = _context.Theaters.ToList();

            ViewBag.Dates = _context.Showtimes
                .Select(s => s.ShowDate)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

           // keep selected values
            ViewBag.SelectedTheater = theaterId;
            ViewBag.SelectedDate = showDate;

            return View(showtimes.ToList());
        }
    }
}