using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Old.Data;

namespace ScrumMovieTheater.Old.Areas.Manager.Controllers
{
    [Area("Manager")]

    [Authorize(Roles = "Manager")]
    public class LowStockController : Controller
    {
        private readonly AppDbContext _context;

        public LowStockController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var lowStockItems = _context.ConcessionInventories
                .Include(i => i.ConcessionItem)
                .Include(i => i.Theater)
                .Where(i => i.QuantityOnHand <= i.ConcessionItem!.LowStockThreshold)
                .ToList();

            return View(lowStockItems);
        }
    }
}