/* OrderController.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-09-12: Created
 *          Ji Hong Ahn, 2019-10-10: Refined codes
 *                                   Added documentation comments and header comments
 */
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

namespace PandaTea.Controllers
{
    /// <summary>
    /// Controller class for OrderModel
    /// </summary>
    public class OrderController : Controller
    {
        private readonly PandaTeaContext _context;

        public OrderController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// </summary>
        /// <returns>View with OrderModel data</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderModel.ToListAsync());
        }

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">OrderId to select</param>
        /// <returns>View with OrderModel data</returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        /// <summary>
        /// Returns View for Create action
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Inserts OrderModel data to the database
        /// </summary>
        /// <param name="orderModel">OrderModel data to insert</param>
        /// <returns>View with OrderModel data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UserId,ProductId,StoreId,Quantity,Price,DatePurchased")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">OrderId to update</param>
        /// <returns>View with OrderModel data</returns>
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }

        /// <summary>
        /// Updates OrderModel data with a OrderId to the database
        /// </summary>
        /// <param name="id">OrderId to update</param>
        /// <param name="orderModel">OrderModel data to update</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("OrderId,UserId,ProductId,StoreId,Quantity,Price,DatePurchased")] OrderModel orderModel)
        {
            if (id != orderModel.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.OrderId))
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
            return View(orderModel);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">OrderId to delete</param>
        /// <returns>View with OrderModel data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OrderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (OrderModel == null)
            {
                return NotFound();
            }

            return View(OrderModel);
        }

        /// <summary>
        /// Deletes OrderModel data with a given OrderId from the database
        /// </summary>
        /// <param name="id">OrderId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var orderModel = await _context.OrderModel.FindAsync(id);
            _context.OrderModel.Remove(orderModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if OrderModel data with the given OrderId exists
        /// </summary>
        /// <param name="id">OrderId to verify</param>
        /// <returns>true if a OrderModel exists</returns>
        private bool OrderModelExists(decimal id)
        {
            return _context.OrderModel.Any(e => e.OrderId == id);
        }
    }
}
