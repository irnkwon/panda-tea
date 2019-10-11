/* ReviewController.cs
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
    /// Controller class for ReviewModel
    /// </summary>
    public class ReviewController : Controller
    {
        private readonly PandaTeaContext _context;

        public ReviewController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// </summary>
        /// <returns>View with ReviewModel data</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReviewModel.ToListAsync());
        }

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">ReviewId to select</param>
        /// <returns>View with ReviewModel data</returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTbl = await _context.ReviewModel
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (reviewTbl == null)
            {
                return NotFound();
            }

            return View(reviewTbl);
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
        /// Inserts ReviewModel data to the database
        /// </summary>
        /// <param name="reviewTbl">ReviewModel data to insert</param>
        /// <returns>View with ReviewModel data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,UserId,ProductId,Score,Text,DateReviewed")] ReviewModel reviewTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviewTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reviewTbl);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">ReviewId to update</param>
        /// <returns>View with ReviewModel data</returns>
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTbl = await _context.ReviewModel.FindAsync(id);
            if (reviewTbl == null)
            {
                return NotFound();
            }
            return View(reviewTbl);
        }

        /// <summary>
        /// Updates ReviewModel data with a ReviewId to the database
        /// </summary>
        /// <param name="id">ReviewId to update</param>
        /// <param name="reviewTbl">ReviewModel data to update</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ReviewId,UserId,ProductId,Score,Text,DateReviewed")] ReviewModel reviewTbl)
        {
            if (id != reviewTbl.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviewTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewModelExists(reviewTbl.ReviewId))
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
            return View(reviewTbl);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">ReviewId to delete</param>
        /// <returns>View with ReviewModel data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTbl = await _context.ReviewModel
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (reviewTbl == null)
            {
                return NotFound();
            }

            return View(reviewTbl);
        }

        /// <summary>
        /// Deletes ReviewModel data with a given ReviewId from the database
        /// </summary>
        /// <param name="id">ReviewId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var reviewTbl = await _context.ReviewModel.FindAsync(id);
            _context.ReviewModel.Remove(reviewTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if ReviewModel data with the given ReviewId exists
        /// </summary>
        /// <param name="id">ReviewId to verify</param>
        /// <returns>true if a ReviewModel exists</returns>
        private bool ReviewModelExists(decimal id)
        {
            return _context.ReviewModel.Any(e => e.ReviewId == id);
        }
    }
}
