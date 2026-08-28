using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;


namespace ScrumMovieTheater.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }
        // save movie in database and return to home
        [HttpPost]

        [Authorize(Roles = "Admin, Manager")]
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

        [Authorize(Roles = "Admin, Manager")]
        // LIST movies
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public IActionResult EditMovie(int movieId, int showtimeId)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == movieId);

            if (movie == null)
                return NotFound();

            var showtime = _context.Showtimes
                .FirstOrDefault(s => s.Id == showtimeId);

            var model = new UpdateMovieViewModel
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                RuntimeMinutes = movie.RuntimeMinutes,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                ImageUrl = movie.ImageUrl
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult EditMovie(UpdateMovieViewModel model)
        {
            var existing = _context.Movies
                .FirstOrDefault(m => m.MovieId == model.MovieId);

            if (existing == null)
                return NotFound();


            // Update Movie
            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.Genre = model.Genre;
            existing.RuntimeMinutes = model.RuntimeMinutes;
            existing.Rating = model.Rating;
            existing.ReleaseDate = model.ReleaseDate;
            existing.ImageUrl = model.ImageUrl;


            _context.SaveChanges();

            TempData["SuccessMessage"] = "Movie and Showtime updated successfully!";

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Manager")]
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

        [Authorize(Roles = "Admin, Manager")]
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

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult AddShowTime()
    {
        ViewBag.Movies = _context.Movies.ToList();
        ViewBag.Theaters = _context.Theaters.ToList(); // if you have Theater table
        ViewBag.Auditoriums = _context.Auditoriums
            .Include(a => a.Theater)
            .ToList();



            // _context.Auditoriums.First() = 
            // @Auditorium.Theater.Name



            return View();
    }

        [Authorize(Roles = "Admin, Manager")]
        // post method for showtime
        [HttpPost]
    public IActionResult AddShowTime(Showtime showTime)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("AddShowTime POST reached");
            Console.WriteLine($"Theater: {showTime.TheaterId}");
            Console.WriteLine($"Auditorium: {showTime.AuditoriumId}");
            Console.WriteLine($"Date: {showTime.ShowDate}");
            Console.WriteLine($"Time: {showTime.TimeSlot}");

                // Check auditorium schedule conflict
                var conflict = _context.Showtimes
               .Any(s =>
                   s.TheaterId == showTime.TheaterId &&
                   s.AuditoriumId == showTime.AuditoriumId &&
                   s.ShowDate == showTime.ShowDate &&
                   s.TimeSlot == showTime.TimeSlot
               );

                if (conflict)
                {
                    ModelState.AddModelError("",
                        "This auditorium already has a showtime at this date and time.");
                    ViewBag.Movies = _context.Movies.ToList();
                    ViewBag.Theaters = _context.Theaters.ToList();
                    ViewBag.Auditoriums = _context.Auditoriums
                        .Include(a => a.Theater)
                        .ToList();

                    return View(showTime);

                }
                _context.Showtimes.Add(showTime);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Showtime added successfully!";
                return RedirectToAction("Index");
             }

             foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
             {
                Console.WriteLine(error.ErrorMessage);
             }

            ViewBag.Movies = _context.Movies.ToList();
            ViewBag.Theaters = _context.Theaters.ToList();
            ViewBag.Auditoriums = _context.Auditoriums
                .Include(a => a.Theater)
                .ToList();

            return View(showTime);
        }

        
       
        // Get method for update showtime
        [HttpGet]
        public IActionResult EditShowtime(int showtimeId)
        {
            var showtime = _context.Showtimes
                .FirstOrDefault(s => s.Id == showtimeId);

            if (showtime == null)
                return NotFound();

            var model = new UpdateShowtimeViewModel
            {
                ShowtimeId = showtime.Id,
                MovieId = showtime.MovieId,
                TheaterId = showtime.TheaterId,
                AuditoriumId = showtime.AuditoriumId,
                ShowDate = showtime.ShowDate,
                TimeSlot = showtime.TimeSlot,
                Price = showtime.Price
            };

            ViewBag.Movies = new SelectList(
                _context.Movies,
                "MovieId",
                "Title",
                model.MovieId
            );

            ViewBag.Theaters = new SelectList(
                _context.Theaters,
                "TheaterId",
                "Name",
                model.TheaterId
            );

            ViewBag.Auditoriums = new SelectList(
                _context.Auditoriums,
                "AuditoriumId",
                "Name",
                model.AuditoriumId
            );

            return View(model);
        }

        // update showtime

        [HttpPost]
        public IActionResult EditShowtime(UpdateShowtimeViewModel model)
        {
            var showtime = _context.Showtimes
                .FirstOrDefault(s => s.Id == model.ShowtimeId);

            if (showtime == null)
                return NotFound();

            showtime.MovieId = model.MovieId;
            showtime.TheaterId = model.TheaterId;
            showtime.AuditoriumId = model.AuditoriumId;
            showtime.ShowDate = model.ShowDate;
            showtime.TimeSlot = model.TimeSlot;
            showtime.Price = model.Price;

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Showtime updated successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteShowtime(int showtimeId)
        {
            var showtime = _context.Showtimes
                .FirstOrDefault(s => s.Id == showtimeId);

            if (showtime == null)
                return NotFound();

            _context.Showtimes.Remove(showtime);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Showtime deleted successfully!";

            return RedirectToAction("ManageShowtimes");
        }

        // Manage showtimes
        [HttpGet]
        public IActionResult ManageShowtimes()
        {
            var showtimes = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .Include(s => s.Auditorium)
                .OrderBy(s => s.ShowDate)
                .ThenBy(s => s.TimeSlot)
                .ToList();

            return View(showtimes);
        }

        [Authorize(Roles = "Admin, Manager")]
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



        // If separated like this than the person has to be all three. Using the && statement. 
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles="Manager")]
        //[Authorize(Roles="Employee")]
        [Authorize(Roles="Admin, Manager, Employee")]
    public async Task<IActionResult> BoxOfficePurchase()
    {
        // 1. Await the database call
        // 2. Use _context instead of _dbContext
        // 3. Select the TheaterName property, then call ToListAsync() at the end
        var theaterNames = await _context.Theaters.Select(t => t.Name).ToListAsync();

        // Pass the list of theater names to the view using 
        ViewBag.TheaterNames = theaterNames;

        // LINQ 
        var genres = await _context.Movies
                .Select(m => m.Genre)
                .Distinct()
                .Where(g => !string.IsNullOrEmpty(g))
                .OrderBy(g => g)
                .ToListAsync();

        ViewBag.Genre = genres;

        var movies = await _context.Movies
                .Select(m => m.Title)
                .Distinct()
                .ToListAsync();

         

        return View();
    }
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Manager()
        {
            return View();
        }

        /* 
         These methods depend on each other to find information from the database so that the user can make choices on the webpage. When the user makes a choice, that choice is stored on the webpage and can be used as a paramter for the next method. That next method will be responsbile for the next choice. Understand that you don't need to repeat queries. 
        Other names for this pattern are called progressive disclousre or multi step checkout. 

        Also note that parameters increase and are in order. 
         
         */
        [HttpPost]
        public  IActionResult pickTheater(string selectedTheater)
        {
            ViewBag.SelectedTheater = selectedTheater;
            ViewBag.TheaterNames = new List<string> {selectedTheater};

            return View("BoxOfficePurchase");
             
        }

        [HttpPost]
        public async Task<IActionResult> selectedMovieDate(string selectedTheater, DateTime date) 
        {
            ViewBag.SelectedTheater = selectedTheater;
            ViewBag.TheaterNames = new List<string> { selectedTheater };
            ViewBag.Date = date;
            

            var theaterId = await _context.Theaters
                .Where(t => t.Name == selectedTheater)
                .Select(t => t.TheaterId)
                // find the first instance, if none can be found return null.
                .FirstOrDefaultAsync(); 

            var movieIds = await _context.Showtimes

                .Where(s => s.ShowDate.Date == date.Date)//.Where(o => o.OrderDate.Date == targetDay.Date);
                .Where(s => s.TheaterId == theaterId)
                .Select(s => s.MovieId)
                .ToListAsync(); 
                
            var movies = await _context.Movies
                .Where(m => movieIds.Contains(m.MovieId))
                .Select(m => m.Title)          
                .ToListAsync();

            ViewBag.Movies = movies;
            ViewBag.TheaterId = theaterId; 
            
            return View("BoxOfficePurchase");
        }

        [HttpPost]
        public async Task<IActionResult> selectedMovie(string selectedTheaterId, DateTime date, string movieTitle)
        {

            var theaterId = int.Parse(selectedTheaterId);

            var theaterName = await _context.Theaters
                .Where(t => t.TheaterId == int.Parse(selectedTheaterId))
                .Select(t => t.Name)
                .FirstOrDefaultAsync();

            /* look at the view for the left side of the equation.look at the LINQ query that coreesponds for the variable. */

            ViewBag.SelectedTheater = theaterName;

            ViewBag.TheaterNames = new List<string> { theaterName };

            var movieId = await _context.Movies
                .Where(m => m.Title == movieTitle)
                .Select(m => m.MovieId)
                .FirstOrDefaultAsync();

            
            var showtimes = await _context.Showtimes
                .Where(s => movieId == s.MovieId)
                .Where(s => s.TheaterId == theaterId)
                .Where(s => s.ShowDate == date)
                .Select(s => s.TimeSlot)
                .ToListAsync();


            ViewBag.TheaterId = theaterId;  
            ViewBag.Date = date;
            ViewBag.MovieId = movieId; 
            ViewBag.Movies = new List<string> { movieTitle };
            ViewBag.Showtimes = showtimes;



            return View("BoxOfficePurchase");
        }

        [HttpPost]
        public async Task<IActionResult> selectedShowtime(string selectedTheaterId, DateTime date, string selectedMovieId, string selectedShowtime)
        {
            var theaterId = int.Parse(selectedTheaterId);
            var movieId = int.Parse(selectedMovieId);
            var showtime = TimeSpan.Parse(selectedShowtime); 
            

        /* We queried this information in a prior method call and sent it over. There is no need for the
              query to run in this application but its saved here in comments so that you know Andrew thought about it. (thanks Joseph)
        */
            var theaterName = await _context.Theaters
                .Where(t => t.TheaterId == int.Parse(selectedTheaterId))
                .Select(t => t.Name)
                .FirstOrDefaultAsync();
            



            /* We queried this information in a prior method call and sent it over. There is no need for the 
             * query to run in this application but its saved here in comments so that you know Andrew thought about it. (thanks Joseph) 
             
            var movieId = await _context.Movies
                .Where(m => m.Title == movieTitle)
                .Select(m => m.MovieId)
                .FirstOrDefaultAsync();
            */

            var movieTitle = await _context.Movies
                .Where(m => m.MovieId == movieId)
                .Select(m => m.Title)
                .FirstOrDefaultAsync();
            
            /* 
             TODO: Come back later and implement microsoft best practices for naming conventions. 

             */
            var selectedShowtimeInfo = await _context.Showtimes
                .Where(s => movieId == s.MovieId)
                .Where(s => s.TheaterId == theaterId)
                .Where(s => s.ShowDate == date)
                .Where(s => s.TimeSlot == showtime)
                .Include(s => s.Auditorium)
                .FirstOrDefaultAsync();

            /* 
             Id's are easy to transmit. These particular View Bag options in this comment section are user choices.
             */
            ViewBag.TheaterId = theaterId; 
            ViewBag.Date = date;
            ViewBag.MovieId = movieId;
            ViewBag.ShowtimeId = selectedShowtimeInfo.Id;
            ViewBag.ShowtimePrice = selectedShowtimeInfo.Price; 

            /* we need to display these bits of information on the webpage */ 
            ViewBag.Movies = new List<string> { movieTitle };
            ViewBag.Showtimes = new List<string> { selectedShowtime };
            ViewBag.SelectedTheater = theaterName;
            ViewBag.TheaterNames = new List<string> { theaterName };

            // TODO update database with an adult price. 
            ViewBag.TotalAdultTicketPrice = selectedShowtimeInfo.Price;

            // TODO update database with an adult price. 
            ViewBag.TotalChildTicketPrice = selectedShowtimeInfo.Price;

            decimal totalTicketPrice = ViewBag.TotalAdultTicketPrice * ViewBag.TotalChildTicketPrice;

            string totalTicketPriceOutput = totalTicketPrice.ToString("C2");
            ViewBag.TotalPrice = totalTicketPriceOutput;

            var maxCapacity = selectedShowtimeInfo.Auditorium.Capacity;
            var currentCapactiy = selectedShowtimeInfo.Auditorium.Capacity;

            // SELECT 
            // FROM 
            // USE the first letter of the thing you r
            var auditoriumCapacity =  _context.Bookings
                                        .Where(b => b.ShowtimeId == selectedShowtimeInfo.Id)
                                        .Sum(b => b.Adults + b.Kids);

            var remainingCapacity = maxCapacity - auditoriumCapacity; 

            ViewBag.AuditoriumMaxCapacity = maxCapacity;
            ViewBag.AuditoriumCapacity = auditoriumCapacity;
            ViewBag.RemainingCapacity = remainingCapacity; 

            return View("BoxOfficePurchase");

        //    MAKE A FUNCTION than make the lambda.
        //    public calcTickets(Booking b)
        //{
        //    int totalTickets = b.adults + b.kids
        //        return totalTickets

        //}
    }

    }
    
    
}