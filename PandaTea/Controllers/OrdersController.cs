/* OrdersController.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-09-12: Created
 *          Ji Hong Ahn, 2019-10-10: Refined codes
 *                                   Added documentation comments and header comments
 *          Samantha Dang, 2019-10-23: Add user identification
 *          Samantha Dang, 2019-10-25: Sessions for items added to order (StoreQuantity, Create)
 *          Samantha Dang, 2019-10-31: Set the Order/Create values from Session Variables
 *          Ji Hong Ahn, 2019-11-18: Modified models for Index action
 *          Samantha Dang, 2019-11-21: Updatied Create method so View includes user friendly values
 */
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

namespace PandaTea.Controllers
{
    /// <summary>
    /// Controller class for OrderModel
    /// </summary>
    public class OrdersController : Controller
    {
        private readonly PandaTeaContext _context;

        public OrdersController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// View with Order data if the user is logged in. A user only sees their own orders. If the user is not logged in, they are prompted to log in.
        /// </summary>
        /// <returns>A View with list of User's orders or redirects user to log in screen</returns>
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                decimal userId = Decimal.Parse(HttpContext.Session.GetString("UserId"));
                var pandaTeaContext = _context.Order
                    .Include(s => s.Store)
                    .Include(m => m.Menu)
                    .ThenInclude(p => p.Product)
                    .Where(u => u.UserId == userId)
                    .OrderByDescending(o => o.DatePurchased);
                return View(await pandaTeaContext.ToListAsync());
            }
            else
            {
                TempData["LoginRequiredMessage"] = "Please log in to view your orders.";
                return RedirectToAction("Login", "Users");
            }
        }

        /// <summary>
        /// Store the Quantity of drinks the user wants
        /// </summary>
        /// <returns>Status and message</returns>
        public ActionResult StoreQuantity(Order order)
        {
            try
            {
                var qty = order.Quantity;
                HttpContext.Session.SetString("Quantity", qty.ToString());
                return Json(new { status = true, message = "Quantity storage successful: " + qty });
            }
            catch (Exception e)
            {
                return Json(new { status = false, message = "Quantity storage failed: " + e });
            }
        }

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">OrderId to select</param>
        /// <returns>View with Order data</returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Menu)
                .Include(o => o.Store)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        /// <summary>
        /// Returns View for Create action
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                TempData["LoginRequiredMessage"] = "Please log in to view place an order.";
                return RedirectToAction("Login", "Users");
            }

            var menuId = Decimal.Parse(HttpContext.Session.GetString("MenuId"));
            var productId = Decimal.Parse(HttpContext.Session.GetString("ProductId"));

            //ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId");
            ViewData["MenuId"] = new SelectList(_context.Menu.Include(p => p.Product).Where(u => u.ProductId == productId), "MenuId", "ProductName");
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreName");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            var test = ViewBag.MenuId;
            return View();
        }


        /// <summary>
        /// Inserts Order data to the database
        /// </summary>
        /// <param name="order">Order data to insert</param>
        /// <returns>View with Order data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UserId,MenuId,StoreId,Quantity,DatePurchased")] Order order)
        {
            order.UserId = Decimal.Parse(HttpContext.Session.GetString("UserId"));
            order.Quantity = int.Parse(HttpContext.Session.GetString("Quantity"));
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", order.MenuId);
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreId", order.StoreId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", order.UserId);
            return View(order);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">OrderId to update</param>
        /// <returns>View with Order data</returns>
        public async Task<IActionResult> Edit(decimal? id)
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
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", order.MenuId);
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreId", order.StoreId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", order.UserId);
            return View(order);
        }

        /// <summary>
        /// Updates Order data with a OrderId to the database
        /// </summary>
        /// <param name="id">OrderId to update</param>
        /// <param name="orderModel">Order data to update</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("OrderId,UserId,MenuId,StoreId,Quantity,DatePurchased")] Order order)
        {
            if (id != order.OrderId)
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
                    if (!OrderExists(order.OrderId))
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
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", order.MenuId);
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreId", order.StoreId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", order.UserId);
            return View(order);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">OrderId to delete</param>
        /// <returns>View with Order data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Menu)
                .Include(o => o.Store)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        /// <summary>
        /// Deletes Order data with a given OrderId from the database
        /// </summary>
        /// <param name="id">OrderId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if Order data with the given OrderId exists
        /// </summary>
        /// <param name="id">OrderId to verify</param>
        /// <returns>true if an Order exists</returns>
        private bool OrderExists(decimal id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
