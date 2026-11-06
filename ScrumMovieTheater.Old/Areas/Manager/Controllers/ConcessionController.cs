using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.Old.Data;
using ScrumMovieTheater.Old.Models;

namespace ScrumMovieTheater.Old.Areas.Manager.Controllers
{
    [Area("Manager")]

    [Authorize(Roles = "Manager")]
    public class ConcessionController : Controller
    {
        private readonly AppDbContext _context;

        public ConcessionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.ConcessionItems.ToList();

            return View(items);
        }


        // Show Add Concession Item form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // Save new Concession Item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ConcessionItem item)
        {
            if (ModelState.IsValid)
            {
                _context.ConcessionItems.Add(item);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Concession item added successfully!";


                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        // edit get concession item method
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.ConcessionItems
                .FirstOrDefault(x => x.ConcessionItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        
        // Edit Concession items post method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ConcessionItem item)
        {
            if (ModelState.IsValid)
            {
                _context.ConcessionItems.Update(item);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Concession item updated successfully!";

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        // Active and inactive methods
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleActive(int id)
        {
            var item = _context.ConcessionItems
                .FirstOrDefault(x => x.ConcessionItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            item.Active = !item.Active;

            _context.SaveChanges();

            TempData["SuccessMessage"] = item.Active
                ? "Concession item activated successfully!"
                : "Concession item deactivated successfully!";

            return RedirectToAction(nameof(Index));
        }

    }
}
    
