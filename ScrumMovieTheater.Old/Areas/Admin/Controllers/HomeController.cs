using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Old.Data;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
{
    _context = context;
}
    public IActionResult Index()
{
    var movies = _context.Movies
        .Include(m => m.Showtimes)
        .ToList();
    return View(movies);
}


}