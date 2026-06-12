using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Controllers
{
    public class ShowtimesController : Controller
    {
        public IActionResult Index()
        {
            var schedules = new List<Schedule>
            {
                new Schedule
                {
                    TheaterName = "Dallas",
                    MovieTitle = "Avatar 3",
                    Showtime = DateTime.Now
                },

                new Schedule
                {
                    TheaterName = "Houston",
                    MovieTitle = "Superman",
                    Showtime = DateTime.Now.AddHours(1)
                },

                new Schedule
                {
                    TheaterName = "Carrollton",
                    MovieTitle = "Batman",
                    Showtime = DateTime.Now.AddHours(2)
                }
            };

            return View(schedules);
        }
    }
}