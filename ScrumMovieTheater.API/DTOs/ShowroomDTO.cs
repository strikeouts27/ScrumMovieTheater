using Microsoft.AspNetCore.Mvc;

namespace ScrumMovieTheater.API.DTOs
{
    public class ShowroomDTO : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
