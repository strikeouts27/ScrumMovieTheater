using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;
using Microsoft.EntityFrameworkCore;

namespace ScrumMovieTheater.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }
        // save movie in database and return to home
        [HttpPost]
        [HttpPost]
        public IActionResult AddMovie(Movie movie, IFormFile ImageFile)
        {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            string fileName = Path.GetFileName(ImageFile.FileName);

            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images",
                fileName
            );

            using (var stream = new FileStream(path, FileMode.Create))
            {
                ImageFile.CopyTo(stream);
            }

           // THIS is what saves to database
           movie.ImageUrl = "/images/" + fileName;
        }
        else
        {
            movie.ImageUrl = "";
        }

        _context.Movies.Add(movie);
        _context.SaveChanges();

        TempData["SuccessMessage"] = "Movie added successfully!";
        return RedirectToAction("Index");
        }
          // LIST movies
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

       [HttpGet]
        public IActionResult EditMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
        }

        [HttpPost]
        public IActionResult EditMovie(Movie movie)
        {
            var existing = _context.Movies.FirstOrDefault(m => m.MovieId == movie.MovieId);

        if (existing == null)
            return NotFound();

        existing.Title = movie.Title;
        existing.Description = movie.Description;
        existing.Genre = movie.Genre;
        existing.RuntimeMinutes = movie.RuntimeMinutes;
        existing.Rating = movie.Rating;
        existing.ReleaseDate = movie.ReleaseDate;
        existing.ImageUrl = movie.ImageUrl;

        _context.SaveChanges();

        TempData["SuccessMessage"] = "Movie updated successfully!";
        return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);

         if (movie == null)
        {
            return NotFound();
        }

        // ADD IMAGE DELETE CODE HERE (BEFORE REMOVE)
        if (!string.IsNullOrEmpty(movie.ImageUrl))
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                movie.ImageUrl.TrimStart('/')
            );

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        _context.Movies.Remove(movie);
        _context.SaveChanges();

        TempData["SuccessMessage"] = "Movie deleted successfully!";
        return RedirectToAction("Index");
        }

        // showtime get action

    public IActionResult AddShowTime()
    {
        ViewBag.Movies = _context.Movies.ToList();
        ViewBag.Theaters = _context.Theaters.ToList(); // if you have Theater table

        return View();
    }

    // post method for showtime
    [HttpPost]
    public IActionResult AddShowTime(Showtime showTime)
    {
        if (ModelState.IsValid)
        {
            _context.Showtimes.Add(showTime);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Showtime added successfully!";
            return RedirectToAction("Index");
        }

        ViewBag.Movies = _context.Movies.ToList();
        ViewBag.Theaters = _context.Theaters.ToList();

        return View(showTime);
    }

    public IActionResult Bookings()
    {
        var bookings = _context.Bookings
            .Include(b => b.Showtime)
                .ThenInclude(s => s.Movie)
           .Include(b => b.Showtime)
                .ThenInclude(s => s.Theater)
            .ToList();

        return View(bookings);
    }



        public IActionResult Manager()
        {
            return View();
        }
    }
}