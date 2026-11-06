using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Old.Data;
using ScrumMovieTheater.Old.Extensions;
using ScrumMovieTheater.Old.Models;

namespace ScrumMovieTheater.Old.Controllers
{
    public class ConcessionController : Controller
    {
        private const string CartKey = "ConcessionCart";

        private readonly AppDbContext _context;

        public ConcessionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var concessions = await _context.ConcessionItems
                .Where(c => c.Active)
                .OrderBy(c => c.Category)
                .ThenBy(c => c.Name)
                .ToListAsync();

            return View(concessions);
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

            var item = await _context.ConcessionItems
                .FirstOrDefaultAsync(c =>
                    c.ConcessionItemId == concessionItemId &&
                    c.Active);

            if (item == null)
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
                    ConcessionItemId = item.ConcessionItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = quantity
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            HttpContext.Session.SetObject(CartKey, cart);

            TempData["SuccessMessage"] =
                $"{item.Name} was added to your cart.";

            return RedirectToAction(nameof(Index));
        }

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

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
            }

            HttpContext.Session.SetObject(CartKey, cart);

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
                TempData["CartMessage"] = "Your cart is empty.";
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
                    "Your cart is empty.");
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
                    // Find booking theater
                    var booking = await _context.Bookings
                        .Include(b => b.Showtime)
                        .FirstOrDefaultAsync(b => b.Id == model.BookingId);

                    if (booking == null || booking.Showtime == null)
                    {
                        throw new InvalidOperationException(
                            "Booking information not found.");
                    }


                    // Find inventory for this theater and item
                    var inventory = await _context.ConcessionInventories
                        .FirstOrDefaultAsync(i =>
                            i.ConcessionItemId == cartItem.ConcessionItemId &&
                            i.TheaterId == booking.Showtime.TheaterId);


                    if (inventory == null)
                    {
                        throw new InvalidOperationException(
                            "Inventory record not found.");
                    }


                    // Check stock
                    if (inventory.QuantityOnHand < cartItem.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"{cartItem.Name} does not have enough stock.");
                    }


                    // Reduce inventory
                    inventory.QuantityOnHand -= cartItem.Quantity;


                    // Create order item
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        ConcessionItemId = cartItem.ConcessionItemId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Price
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
                    "The order is out of stock. Please try again later.");

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