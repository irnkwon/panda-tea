/* TransactionController.cs
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
    /// Controller class for TransactionModel
    /// </summary>
    public class TransactionController : Controller
    {
        private readonly PandaTeaContext _context;

        public TransactionController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// </summary>
        /// <returns>View with TransactionModel data</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionModel.ToListAsync());
        }

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">TransactionId to select</param>
        /// <returns>View with TransactionModel data</returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTbl = await _context.TransactionModel
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionTbl == null)
            {
                return NotFound();
            }

            return View(transactionTbl);
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
        /// Inserts TransactionModel data to the database
        /// </summary>
        /// <param name="transactionTbl">TransactionModel data to insert</param>
        /// <returns>View with TransactionModel data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,UserId,ProductId,StoreId,Quantity,Price,DatePurchased")] TransactionModel transactionTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionTbl);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">TransactionId to update</param>
        /// <returns>View with TransactionModel data</returns>
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTbl = await _context.TransactionModel.FindAsync(id);
            if (transactionTbl == null)
            {
                return NotFound();
            }
            return View(transactionTbl);
        }

        /// <summary>
        /// Updates TransactionModel data with a TransactionId to the database
        /// </summary>
        /// <param name="id">TransactionId to update</param>
        /// <param name="transactionTbl">TransactionModel data to update</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TransactionId,UserId,ProductId,StoreId,Quantity,Price,DatePurchased")] TransactionModel transactionTbl)
        {
            if (id != transactionTbl.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionModelExists(transactionTbl.TransactionId))
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
            return View(transactionTbl);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">TransactionId to delete</param>
        /// <returns>View with TransactionModel data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTbl = await _context.TransactionModel
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionTbl == null)
            {
                return NotFound();
            }

            return View(transactionTbl);
        }

        /// <summary>
        /// Deletes TransactionModel data with a given TransactionId from the database
        /// </summary>
        /// <param name="id">TransactionId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var transactionTbl = await _context.TransactionModel.FindAsync(id);
            _context.TransactionModel.Remove(transactionTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if TransactionModel data with the given TransactionId exists
        /// </summary>
        /// <param name="id">TransactionId to verify</param>
        /// <returns>true if a TransactionModel exists</returns>
        private bool TransactionModelExists(decimal id)
        {
            return _context.TransactionModel.Any(e => e.TransactionId == id);
        }
    }
}
