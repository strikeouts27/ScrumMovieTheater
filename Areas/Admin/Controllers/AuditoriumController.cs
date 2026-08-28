using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuditoriumController : Controller
    {
        private readonly AppDbContext _context;

        public AuditoriumController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Manager")]
        // Show all auditoriums
        public IActionResult Index()
        {
            var auditoriums = _context.Auditoriums
                .Include(a => a.Theater)
                .ToList();

            return View(auditoriums);
        }
        [Authorize(Roles = "Admin, Manager")]
        // Add Auditorium Get Method
        [HttpGet]
        public IActionResult AddAuditorium()
        {
            ViewBag.Theaters = _context.Theaters
                .Where(t => t.Active)
                .ToList();

            return View();
        }

        // Add Auditorium Post method
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult AddAuditorium(Auditorium auditorium)
        {
            if (ModelState.IsValid)
            {
                auditorium.Active = true;

                _context.Auditoriums.Add(auditorium);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Auditorium added successfully!";

                return RedirectToAction("Index");
            }

            ViewBag.Theaters = _context.Theaters
                .Where(t => t.Active)
                .ToList();

            return View(auditorium);
        }

        // Edit Auditorium get method
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public IActionResult EditAuditorium(int id)
        {
            var auditorium = _context.Auditoriums
                .FirstOrDefault(a => a.AuditoriumId == id);

            if (auditorium == null)
            {
                return NotFound();
            }

            ViewBag.Theaters = _context.Theaters.ToList();

            return View(auditorium);
        }

        // Edit Auditorium post method
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult EditAuditorium(Auditorium auditorium)
        {
            var existing = _context.Auditoriums
                .FirstOrDefault(a => a.AuditoriumId == auditorium.AuditoriumId);

            if (existing == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existing.Name = auditorium.Name;
                existing.Capacity = auditorium.Capacity;
                existing.TheaterId = auditorium.TheaterId;

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Auditorium updated successfully!";

                return RedirectToAction("Index");
            }

            ViewBag.Theaters = _context.Theaters
                .Where(t => t.Active)
                .ToList();

            return View(auditorium);
        }

        // Deactivate post method
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult DeactivateAuditorium(int id)
        {
            var auditorium = _context.Auditoriums
                .FirstOrDefault(a => a.AuditoriumId == id);

            if (auditorium == null)
            {
                return NotFound();
            }

            auditorium.Active = false;

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Auditorium deactivated successfully!";

            return RedirectToAction("Index");
        }
    }
}