using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Old.Data;
using ScrumMovieTheater.Old.Extensions;
using ScrumMovieTheater.Old.Models;

namespace ScrumMovieTheater.Old.Controllers
{
    public class KioskController : Controller
    {
        private const string CartKey = "KioskConcessionCart";

        private readonly AppDbContext _context;

        public KioskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var concessions = await _context.ConcessionItems
                .Where(c => c.Active)
                .OrderBy(c => c.Category)
                .ThenBy(c => c.Name)
                .ToListAsync();

            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            var model = new KioskMenuViewModel
            {
                Concessions = concessions,
                CartItems = cart
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(
            int concessionItemId,
            int quantity)
        {
            if (quantity < 1)
            {
                quantity = 1;
            }

            var concession = await _context.ConcessionItems
                .FirstOrDefaultAsync(c =>
                    c.ConcessionItemId == concessionItemId &&
                    c.Active);

            if (concession == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            var existingItem = cart.FirstOrDefault(c =>
                c.ConcessionItemId == concessionItemId);

            if (existingItem == null)
            {
                cart.Add(new ConcessionCartItem
                {
                    ConcessionItemId = concession.ConcessionItemId,
                    Name = concession.Name,
                    Price = concession.Price,
                    Quantity = quantity
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            HttpContext.Session.SetObject(CartKey, cart);

            TempData["KioskMessage"] =
                $"{concession.Name} added to your order.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(
            int concessionItemId,
            int quantity)
        {
            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            var item = cart.FirstOrDefault(c =>
                c.ConcessionItemId == concessionItemId);

            if (item == null)
            {
                TempData["KioskMessage"] =
                    "Item was not found in the order.";

                return RedirectToAction(nameof(Cart));
            }

            if (quantity <= 0)
            {
                cart.Remove(item);

                TempData["KioskMessage"] =
                    "Item removed from the order.";
            }
            else
            {
                item.Quantity = quantity;

                TempData["KioskMessage"] =
                    "Order quantity updated.";
            }

            HttpContext.Session.SetObject(CartKey, cart);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IncreaseQuantity(int concessionItemId)
        {
            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            var item = cart.FirstOrDefault(c =>
                c.ConcessionItemId == concessionItemId);

            if (item != null)
            {
                item.Quantity++;

                HttpContext.Session.SetObject(CartKey, cart);

                TempData["KioskMessage"] =
                    "Quantity increased.";
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DecreaseQuantity(int concessionItemId)
        {
            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            var item = cart.FirstOrDefault(c =>
                c.ConcessionItemId == concessionItemId);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);

                    TempData["KioskMessage"] =
                        "Item removed from the order.";
                }
                else
                {
                    TempData["KioskMessage"] =
                        "Quantity decreased.";
                }

                HttpContext.Session.SetObject(CartKey, cart);
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int concessionItemId)
        {
            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            var item = cart.FirstOrDefault(c =>
                c.ConcessionItemId == concessionItemId);

            if (item != null)
            {
                cart.Remove(item);
            }

            HttpContext.Session.SetObject(CartKey, cart);

            TempData["KioskMessage"] =
                "Item removed from the order.";

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            if (!cart.Any())
            {
                TempData["KioskMessage"] =
                    "Your order is empty.";

                return RedirectToAction(nameof(Cart));
            }

            var model = new ConcessionCheckoutViewModel
            {
                CartItems = cart
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(
            ConcessionCheckoutViewModel model)
        {
            var cart = HttpContext.Session
                .GetObject<List<ConcessionCartItem>>(CartKey)
                ?? new List<ConcessionCartItem>();

            model.CartItems = cart;

            if (!cart.Any())
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Your order is empty.");
            }

            var bookingExists = await _context.Bookings
                .AnyAsync(b => b.Id == model.BookingId);

            if (!bookingExists)
            {
                ModelState.AddModelError(
                    nameof(model.BookingId),
                    "The booking ID was not found.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var order = new Order
                {
                    BookingId = model.BookingId,
                    OrderDate = DateTime.Now
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var cartItem in cart)
                {
                    var concession = await _context.ConcessionItems
                        .FirstOrDefaultAsync(c =>
                            c.ConcessionItemId == cartItem.ConcessionItemId &&
                            c.Active);

                    if (concession == null)
                    {
                        throw new InvalidOperationException(
                            "A concession item is unavailable.");
                    }


                    // Get booking and theater information
                    var booking = await _context.Bookings
                        .Include(b => b.Showtime)
                        .FirstOrDefaultAsync(b => b.Id == model.BookingId);

                    if (booking == null || booking.Showtime == null)
                    {
                        throw new InvalidOperationException(
                            "Booking information not found.");
                    }


                    // Find inventory for this theater
                    var inventory = await _context.ConcessionInventories
                        .FirstOrDefaultAsync(i =>
                            i.ConcessionItemId == cartItem.ConcessionItemId &&
                            i.TheaterId == booking.Showtime.TheaterId);


                    if (inventory == null)
                    {
                        throw new InvalidOperationException(
                            "Inventory record not found.");
                    }


                    // Check available stock
                    if (inventory.QuantityOnHand < cartItem.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"{concession.Name} does not have enough stock.");
                    }


                    // Reduce inventory
                    inventory.QuantityOnHand -= cartItem.Quantity;


                    // Create order item
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        ConcessionItemId = concession.ConcessionItemId,
                        Quantity = cartItem.Quantity,
                        Price = concession.Price
                    };

                    _context.OrderItems.Add(orderItem);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                HttpContext.Session.Remove(CartKey);

                return RedirectToAction(
                    nameof(OrderSuccess),
                    new { orderId = order.OrderId });
            }
            catch
            {
                await transaction.RollbackAsync();

                ModelState.AddModelError(
                    string.Empty,
                    "The item is out of stock.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> OrderSuccess(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ConcessionItem)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}