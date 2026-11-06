using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Old.Data;

namespace ScrumMovieTheater.Old.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        const int moviesPerSection = 8;

        var today = DateTime.Today;

        var nowShowing = _context.Movies
            .Where(m => m.Showtimes.Any(s => s.ShowDate >= today))
            .OrderBy(m => m.Title)
            .Take(moviesPerSection)
            .ToList();

        if (nowShowing.Count == 0)
        {
            nowShowing = _context.Movies
                .Where(m => m.ReleaseDate <= today)
                .OrderByDescending(m => m.ReleaseDate)
                .Take(moviesPerSection)
                .ToList();
        }

        var comingSoon = _context.Movies
            .Where(m => m.ReleaseDate > today)
            .OrderBy(m => m.ReleaseDate)
            .Take(moviesPerSection)
            .ToList();

        var model = new HomeViewModel
        {
            NowShowing = nowShowing,
            ComingSoon = comingSoon
        };

        return View(model);
    }
    public IActionResult Privacy()
{
    return View("~/Views/Home/Privacy.cshtml");
}
}