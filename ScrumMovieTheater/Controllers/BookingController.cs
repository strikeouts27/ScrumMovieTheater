using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tickets page
        public IActionResult Tickets(int showtimeId)
        {
            var showtime = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .FirstOrDefault(s => s.Id == showtimeId);

            if (showtime == null)
                return RedirectToAction("Index", "Showtimes");

            ViewBag.ShowtimeId = showtime.Id;
            ViewBag.Movie = showtime.Movie?.Title;
            ViewBag.Time = showtime.ShowDate.ToShortDateString() + " " + showtime.TimeSlot;

            return View(showtime);
        }

           // POST: Confirm booking
        [HttpPost]
        public IActionResult Confirm(int showtimeId, string customerName, int adults, int kids)
        {
            int adultPrice = 10;
            int kidPrice = 5;

            int total = (adults * adultPrice) + (kids * kidPrice);

            var booking = new Booking
            {
                ShowtimeId = showtimeId,
                CustomerName = customerName,
                Adults = adults,
                Kids = kids,
                TotalPrice = total,
                BookedAt = DateTime.Now
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            ViewBag.TotalPrice = total;

            return View("Success");
        }

    }

}