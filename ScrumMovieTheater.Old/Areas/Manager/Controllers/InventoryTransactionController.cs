using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Old.Data;

namespace ScrumMovieTheater.Old.Areas.Manager.Controllers
{
    [Area("Manager")]

    [Authorize(Roles = "Manager")]
    public class InventoryTransactionController : Controller
    {
        private readonly AppDbContext _context;

        public InventoryTransactionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var transactions = _context.InventoryTransactions
                .Include(t => t.Inventory)
                    .ThenInclude(i => i.ConcessionItem)
                .Include(t => t.Inventory)
                    .ThenInclude(i => i.Theater)
                .OrderByDescending(t => t.Date)
                .ToList();

            return View(transactions);
        }
    }
}