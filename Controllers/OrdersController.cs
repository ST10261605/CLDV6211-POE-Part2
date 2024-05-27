using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCKhumaloCraftFinal2.Controllers;
using MVCKhumaloCraftFinal2.Data;
using MVCKhumaloCraftFinal2.Models;
using MVCKhumaloCraftFinal2.ViewModels;

namespace MVCKhumaloCraftFinal.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MVCKhumaloCraftFinal2Context _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(MVCKhumaloCraftFinal2Context context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Order.Include(o => o.Product).Include(o => o.User).ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> CustomCheckout()
        {
            var cartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();
            var viewModel = new CheckoutViewModel
            {
                CartItems = cartItems,
                OrderDate = DateTime.Now
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomCheckout(CheckoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user ID from the cookie
                if (!HttpContext.Request.Cookies.TryGetValue("userID", out string userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    _logger.LogWarning("User ID not found in cookies.");
                    return Unauthorized();
                }

                _logger.LogInformation("User ID retrieved from cookies: {UserId}", userId);
                var cartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();

                foreach (var item in cartItems)
                {
                    var order = new Order
                    {
                        userID = userId,
                        productID = item.ProductID,
                        Status = "Pending",
                        OrderDate = viewModel.OrderDate,
                        shippingMethod = viewModel.shippingMethod,
                        shippingAmount = item.Product.Price, // Calculate shipping amount as needed
                        shippingCurrency = viewModel.shippingCurrency,
                        shippingAddress = viewModel.shippingAddress
                    };
                    _context.Order.Add(order);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }

            viewModel.CartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();
                // Retrieve the user ID from the cookie
                if (!HttpContext.Request.Cookies.TryGetValue("userID", out string userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    _logger.LogWarning("User ID not found in cookies.");
                    return Unauthorized();
                }

                foreach (var item in cartItems)
                {
                    var order = new Order
                    {
                        userID = userId,
                        productID = item.ProductID,
                        Status = "Pending",
                        OrderDate = viewModel.OrderDate,
                        shippingMethod = viewModel.shippingMethod,
                        shippingAmount = item.Product.Price,
                        shippingCurrency = viewModel.shippingCurrency,
                        shippingAddress = viewModel.shippingAddress
                    };

                    _context.Order.Add(order);
                }

                await _context.SaveChangesAsync();

                // Optionally clear the cart
                _context.CartItem.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Orders");
            }

            viewModel.CartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();
            return View("CustomCheckout", viewModel);
        }



        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.orderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("orderID,userID,productID,Status,OrderDate,shippingMethod,shippingAmount,shippingCurrency,shippingAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("orderID,userID,productID,Status,OrderDate,shippingMethod,shippingAmount,shippingCurrency,shippingAddress")] Order order)
        {
            if (id != order.orderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.orderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.orderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.orderID == id);
        }
    }
}
