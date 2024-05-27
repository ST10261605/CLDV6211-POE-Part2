using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCKhumaloCraftFinal2.Data;
using MVCKhumaloCraftFinal2.Models;
using MVCKhumaloCraftFinal2.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCKhumaloCraftFinal2.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly MVCKhumaloCraftFinal2Context _context;
       

        public CheckoutsController(MVCKhumaloCraftFinal2Context context)
        {
            _context = context;
   
        }

        // GET: Checkouts
        public async Task<IActionResult> Index()
        {
            var cartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();
            return View(cartItems);
        }

        //public async Task<IActionResult> CustomCheckout()
        //{
        //    var cartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();
        //    var viewModel = new CheckoutViewModel
        //    {
        //        CartItems = cartItems,
        //        OrderDate = DateTime.Now
        //    };
        //    return View(viewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CustomCheckout(CheckoutViewModel viewModel)
        //{
        //    _logger.LogInformation("CustomCheckout POST method called.");

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogWarning("Model state is invalid.");
        //        viewModel.CartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();
        //        return View(viewModel);
        //    }

        //    // Retrieve the user ID from the cookie
        //    if (!HttpContext.Request.Cookies.TryGetValue("userID", out string userIdString) || !int.TryParse(userIdString, out int userId))
        //    {
        //        _logger.LogWarning("User ID not found in cookies.");
        //        return Unauthorized();
        //    }

        //    _logger.LogInformation("User ID retrieved from cookies: {UserId}", userId);
        //    var cartItems = await _context.CartItem.Include(c => c.Product).ToListAsync();

        //    if (cartItems == null || !cartItems.Any())
        //    {
        //        _logger.LogWarning("No cart items found for checkout.");
        //        return BadRequest("No items in the cart.");
        //    }

        //    foreach (var item in cartItems)
        //    {
        //        var order = new Order
        //        {
        //            userID = userId,
        //            productID = item.ProductID,
        //            Status = "Pending",
        //            OrderDate = viewModel.OrderDate,
        //            shippingMethod = viewModel.ShippingMethod,
        //            shippingAmount = item.Product.Price, // Calculate shipping amount as needed
        //            shippingCurrency = viewModel.ShippingCurrency,
        //            shippingAddress = viewModel.ShippingAddress
        //        };

        //        try
        //        {
        //            _context.Order.Add(order);
        //            _logger.LogInformation("Order added: {@Order}", order);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Error adding order: {@Order}", order);
        //            return StatusCode(500, "Internal server error while adding orders.");
        //        }
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        _logger.LogInformation("Orders saved to the database.");
        //        return RedirectToAction("Index", "Orders");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error saving orders to the database.");
        //        return StatusCode(500, "Internal server error while saving orders.");
        //    }
        //}
 

// GET: Checkouts/Details/5
public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkout
                .Include(c => c.CartItem)
                .FirstOrDefaultAsync(m => m.checkoutID == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // GET: Checkouts/Create
        public IActionResult Create()
        {
            ViewData["CartItemID"] = new SelectList(_context.CartItem, "CartItemID", "CartItemID");
            return View();
        }

        // POST: Checkouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("checkoutID,CartItemID")] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartItemID"] = new SelectList(_context.CartItem, "CartItemID", "CartItemID", checkout.CartItemID);
            return View(checkout);
        }

        // GET: Checkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkout.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }
            ViewData["CartItemID"] = new SelectList(_context.CartItem, "CartItemID", "CartItemID", checkout.CartItemID);
            return View(checkout);
        }

        // POST: Checkouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("checkoutID,CartItemID")] Checkout checkout)
        {
            if (id != checkout.checkoutID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckoutExists(checkout.checkoutID))
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
            ViewData["CartItemID"] = new SelectList(_context.CartItem, "CartItemID", "CartItemID", checkout.CartItemID);
            return View(checkout);
        }

        // GET: Checkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkout
                .Include(c => c.CartItem)
                .FirstOrDefaultAsync(m => m.checkoutID == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // POST: Checkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkout = await _context.Checkout.FindAsync(id);
            if (checkout != null)
            {
                _context.Checkout.Remove(checkout);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkout.Any(e => e.checkoutID == id);
        }
    }
}
