using Microsoft.AspNetCore.Mvc;

namespace ScrumMovieTheater.Controllers
{
    public class ShowtimesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}