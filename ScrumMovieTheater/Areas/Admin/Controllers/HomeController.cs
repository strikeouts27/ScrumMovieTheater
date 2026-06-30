using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Data;
[Area("Admin")]
public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
{
    _context = context;
}
    public IActionResult Index()
{
    var movies = _context.Movies.ToList();
    return View(movies);
}


}