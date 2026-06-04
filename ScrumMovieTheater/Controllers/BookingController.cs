using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Tickets(int scheduleId, string movie, string time)
        {
            ViewBag.ScheduleId = scheduleId;
            ViewBag.Movie = movie;
            ViewBag.Time = time;

            return View();
        }

        [HttpPost]
        public IActionResult Confirm(int scheduleId, string customerName, int adults, int kids)
        {
            decimal totalPrice = (adults * 10) + (kids * 5);

            var booking = new Booking
            {
                ScheduleId = scheduleId,
                CustomerName = customerName,
                Adults = adults,
                Kids = kids,
                TotalPrice = totalPrice
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            ViewBag.TotalPrice = totalPrice;

            return View("Success");
        }
    }
}