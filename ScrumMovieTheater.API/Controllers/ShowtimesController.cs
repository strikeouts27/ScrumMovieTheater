using Microsoft.AspNetCore.Mvc;

namespace ScrumMovieTheater.API.Controllers
{
    public class ShowtimesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
