using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Old.Data;
using ScrumMovieTheater.Old.Models;

namespace ScrumMovieTheater.Old.Areas.Manager.Controllers
{
    [Area("Manager")]

    [Authorize(Roles = "Manager")]
    public class ConcessionInventoryController : Controller
    {
        private readonly AppDbContext _context;

        public ConcessionInventoryController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var inventory = _context.ConcessionInventories
                .Include(i => i.ConcessionItem)
                .Include(i => i.Theater)
                .ToList();

            return View(inventory);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.ConcessionInventories
                .Include(x => x.ConcessionItem)
                .Include(x => x.Theater)
                .FirstOrDefaultAsync(x => x.InventoryId == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        // Post method for edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConcessionInventory inventory)
        {
            if (id != inventory.InventoryId)
                return NotFound();

            var existingInventory = await _context.ConcessionInventories
                .FirstOrDefaultAsync(x => x.InventoryId == id);

            if (existingInventory == null)
                return NotFound();

            existingInventory.QuantityOnHand = inventory.QuantityOnHand;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Inventory updated successfully!";

            return RedirectToAction(nameof(Index));
        }

        // Restock get action
        public async Task<IActionResult> Restock(int id)
        {
            var inventory = await _context.ConcessionInventories
                .Include(x => x.ConcessionItem)
                .Include(x => x.Theater)
                .FirstOrDefaultAsync(x => x.InventoryId == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // Restock post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restock(
             int id,
             int quantityChange,
             string reason)
        {
            var inventory = await _context.ConcessionInventories
                .FirstOrDefaultAsync(x => x.InventoryId == id);

            if (inventory == null)
            {
                return NotFound();
            }


            // Update current inventory
            inventory.QuantityOnHand += quantityChange;


            // Record transaction
            var transaction = new InventoryTransaction
            {
                InventoryId = inventory.InventoryId,
                QuantityChange = quantityChange,
                Reason = reason,
                Date = DateTime.Now
            };


            _context.InventoryTransactions.Add(transaction);

            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] =
                "Inventory restock recorded successfully!";


            return RedirectToAction(nameof(Index));
        }

    }
}