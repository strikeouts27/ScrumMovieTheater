using ScrumMovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Models;
using Microsoft.AspNetCore.Mvc;

namespace ScrumMovieTheater.Controllers;

public class TheaterController : Controller
{
    private readonly AppDbContext _context;

    public TheaterController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var theaters = _context.Theaters.ToList();
        return View(theaters);
    }
}