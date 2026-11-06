using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Old.Data;

namespace ScrumMovieTheater.Old.Controllers
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
                .Include(s => s.Auditorium)
                .FirstOrDefault(s => s.Id == showtimeId);

            if (showtime == null)
                return RedirectToAction("Index", "Showtimes");

            int ticketsSold = _context.Bookings
               .Where(b => b.ShowtimeId == showtimeId)
               .Sum(b => b.Adults + b.Kids);

            ViewBag.RemainingSeats = showtime.Auditorium!.Capacity - ticketsSold;
            

            ViewBag.ShowtimeId = showtime.Id;
            ViewBag.Movie = showtime.Movie?.Title;
            ViewBag.Time = showtime.ShowDate.ToShortDateString() + " " + showtime.TimeSlot;

            return View(showtime);
        }

        // POST: Confirm booking
        // POST: Confirm booking
        [HttpPost]
        public IActionResult Confirm(int showtimeId, string customerName, int adults, int kids)
        {
            // Get the selected showtime price
            var showtime = _context.Showtimes
               .Include(s => s.Movie)
               .Include(s => s.Theater)
               .Include(s => s.Auditorium)
               .FirstOrDefault(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return NotFound();
            }

            int ticketsRequested = adults + kids;

            int ticketsSold = _context.Bookings
                .Where(b => b.ShowtimeId == showtimeId)
                .Sum(b => b.Adults + b.Kids);

            int remainingSeats = showtime.Auditorium!.Capacity - ticketsSold;

            if (ticketsRequested > remainingSeats)
            {
                ViewBag.ErrorMessage =
                    $"Sorry, only {remainingSeats} seats are available.";

                return View("Tickets", showtime);
            }

            decimal adultPrice = showtime.Price;
            decimal kidPrice = showtime.Price * 0.5m; // kids pay 50%

            decimal total = (adults * adultPrice) + (kids * kidPrice);

            // Generate confirmation code
            string confirmationCode = Guid.NewGuid()
                .ToString()
                .Substring(0, 8)
                .ToUpper();

            var booking = new Booking
            {
                ShowtimeId = showtimeId,
                CustomerName = customerName,
                Adults = adults,
                Kids = kids,
                TotalPrice = total,
                BookedAt = DateTime.Now,
                ConfirmationCode = confirmationCode
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Get complete booking information
            var confirmedBooking = _context.Bookings
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Theater)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Auditorium)
                .FirstOrDefault(b => b.Id == booking.Id);
               

            return View("Success", confirmedBooking);
        }

    }
}