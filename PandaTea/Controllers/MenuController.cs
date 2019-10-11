/* MenuController.cs
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

namespace PandaTea.Controllers
{
    /// <summary>
    /// Controller class for MenuModel
    /// </summary>
    public class MenuController : Controller
    {
        private readonly PandaTeaContext _context;

        public MenuController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// </summary>
        /// <returns>View with MenuModel data</returns>
        public async Task<IActionResult> Index()
        {
            string message = HttpContext.Session.GetString("FirstName") == null ? "" : "Welcome " + HttpContext.Session.GetString("FirstName") + "!";

            TempData["Message"] = message;
            ViewBag.Message = TempData["Message"];
            return View(await _context.MenuModel.ToListAsync());
        }

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">MenuId to select</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.MenuModel
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
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
        /// Inserts MenuModel data to the database
        /// </summary>
        /// <param name="menuModel">MenuModel data to insert</param>
        /// <returns>View with MenuModel data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,ProductId,Size,Price,Calories")] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuModel);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">MenuId to update</param>
        /// <returns>View with MenuModel data</returns>
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.MenuModel.FindAsync(id);
            if (menuModel == null)
            {
                return NotFound();
            }
            return View(menuModel);
        }

        /// <summary>
        /// Updates MenuModel data with a MenuId to the database
        /// </summary>
        /// <param name="id">MenuId to update</param>
        /// <param name="menuModel">MenuModel data to update</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("MenuId,ProductId,Size,Price,Calories")] MenuModel menuModel)
        {
            if (id != menuModel.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuModelExists(menuModel.MenuId))
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
            return View(menuModel);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">MenuId to delete</param>
        /// <returns>View with MenuModel data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.MenuModel
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
        }

        /// <summary>
        /// Deletes MenuModel data with a given MenuId from the database
        /// </summary>
        /// <param name="id">MenuId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var menuModel = await _context.MenuModel.FindAsync(id);
            _context.MenuModel.Remove(menuModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if MenuModel data with the given MenuId exists
        /// </summary>
        /// <param name="id">MenuId to verify</param>
        /// <returns>true if a MenuModel exists</returns>
        private bool MenuModelExists(decimal id)
        {
            return _context.MenuModel.Any(e => e.MenuId == id);
        }
    }
}
