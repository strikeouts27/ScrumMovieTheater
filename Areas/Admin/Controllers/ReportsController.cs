using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Models;
using System.Text;

namespace ScrumMovieTheater.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin,Manager")]
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Sales(DateTime? startDate, DateTime? endDate)
        {
            var bookings = _context.Bookings
                .Include(b => b.Showtime)
                .ThenInclude(s => s.Theater)
                .AsQueryable();


            if (startDate.HasValue)
            {
                bookings = bookings
                    .Where(b => b.BookedAt >= startDate.Value);
            }


            if (endDate.HasValue)
            {
                bookings = bookings
                    .Where(b => b.BookedAt <= endDate.Value);
            }


            var report = bookings
                .GroupBy(b => b.Showtime!.Theater!.Name)
                .Select(g => new SalesReportViewModel
                {
                    TheaterName = g.Key,

                    TicketsSold = g.Sum(b => b.Adults + b.Kids),

                    Revenue = g.Sum(b => b.TotalPrice)
                })
                .ToList();

            return View(report);
        }

        public IActionResult ConcessionSales(DateTime? startDate, DateTime? endDate)
        {
            var orderItems = _context.OrderItems
                .Include(o => o.Order)
                .ThenInclude(o => o!.Booking)
                .Include(o => o.ConcessionItem)
                .AsQueryable();


            if (startDate.HasValue)
            {
                orderItems = orderItems.Where(o =>
                    o.Order!.OrderDate >= startDate.Value);
            }


            if (endDate.HasValue)
            {
                orderItems = orderItems.Where(o =>
                    o.Order!.OrderDate <= endDate.Value);
            }


            var report = orderItems
                .GroupBy(o => o.ConcessionItem!.Name)
                .Select(g => new ConcessionSalesViewModel
                {
                    ItemName = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Quantity * x.Price)
                })
                .ToList();


            return View(report);
        }
        // Method for exporting file
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult ExportCsv()
        {
            var report = _context.Bookings
                .Include(b => b.Showtime)
                .ThenInclude(s => s.Theater)
                .GroupBy(b => b.Showtime!.Theater!.Name)
                .Select(g => new SalesReportViewModel
                {
                    TheaterName = g.Key,
                    TicketsSold = g.Sum(b => b.Adults + b.Kids),
                    Revenue = g.Sum(b => b.TotalPrice)
                })
                .ToList();

            var csv = new StringBuilder();

            csv.AppendLine("Theater,Tickets Sold,Revenue");

            foreach (var item in report)
            {
                csv.AppendLine($"{item.TheaterName},{item.TicketsSold},{item.Revenue}");
            }

            csv.AppendLine();

            csv.AppendLine(
                $"TOTAL,{report.Sum(r => r.TicketsSold)},{report.Sum(r => r.Revenue)}");

            return File(
                Encoding.UTF8.GetBytes(csv.ToString()),
                "text/csv",
                "SalesReport.csv");
        }

        // Export and date method for concession sales
        public IActionResult ExportConcessionCsv()
        {
            var report = _context.OrderItems
                .Include(o => o.ConcessionItem)
                .GroupBy(o => o.ConcessionItem!.Name)
                .Select(g => new ConcessionSalesViewModel
                {
                    ItemName = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Quantity * x.Price)
                })
                .ToList();


            var csv = new StringBuilder();

            csv.AppendLine("Item,Quantity Sold,Revenue");


            foreach (var item in report)
            {
                csv.AppendLine(
                    $"{item.ItemName},{item.QuantitySold},{item.Revenue}");
            }


            csv.AppendLine();

            csv.AppendLine(
                $"TOTAL,{report.Sum(x => x.QuantitySold)},{report.Sum(x => x.Revenue)}");


            return File(
                Encoding.UTF8.GetBytes(csv.ToString()),
                "text/csv",
                "ConcessionSalesReport.csv");
        }
    }
}