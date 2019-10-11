/* StoreController.cs
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
    /// Controller class for StoreModel
    /// </summary>
    public class StoreController : Controller
    {
        private readonly PandaTeaContext _context;

        public StoreController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// </summary>
        /// <returns>View with StoreModel data</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoreModel.ToListAsync());
        }

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">StoreId to select</param>
        /// <returns>View with StoreModel data</returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTbl = await _context.StoreModel
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (storeTbl == null)
            {
                return NotFound();
            }

            return View(storeTbl);
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
        /// Inserts StoreModel data to the database
        /// </summary>
        /// <param name="storeTbl">StoreModel data to insert</param>
        /// <returns>View with StoreModel data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreName,Address,Street,City,State,Country,PostalCode")] StoreModel storeTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeTbl);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">StoreId to update</param>
        /// <returns>View with StoreModel data</returns>
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTbl = await _context.StoreModel.FindAsync(id);
            if (storeTbl == null)
            {
                return NotFound();
            }
            return View(storeTbl);
        }

        /// <summary>
        /// Updates StoreModel data with a StoreId to the database
        /// </summary>
        /// <param name="id">StoreId to update</param>
        /// <param name="storeTbl">StoreModel data to update</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("StoreId,StoreName,Address,Street,City,State,Country,PostalCode")] StoreModel storeTbl)
        {
            if (id != storeTbl.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreModelExists(storeTbl.StoreId))
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
            return View(storeTbl);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">StoreId to delete</param>
        /// <returns>View with StoreModel data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTbl = await _context.StoreModel
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (storeTbl == null)
            {
                return NotFound();
            }

            return View(storeTbl);
        }

        /// <summary>
        /// Deletes StoreModel data with a given StoreId from the database
        /// </summary>
        /// <param name="id">StoreId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var storeTbl = await _context.StoreModel.FindAsync(id);
            _context.StoreModel.Remove(storeTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if StoreModel data with the given StoreId exists
        /// </summary>
        /// <param name="id">StoreId to verify</param>
        /// <returns>true if a StoreModel exists</returns>
        private bool StoreModelExists(decimal id)
        {
            return _context.StoreModel.Any(e => e.StoreId == id);
        }
    }
}
