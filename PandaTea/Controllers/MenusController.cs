﻿/* MenusController.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-09-12: Created
 *          Ji Hong Ahn, 2019-10-10: Refined codes
 *                                   Added documentation comments and header comments
 * 
 */
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

namespace PandaTea.Controllers
{
    public class MenusController : Controller
    {
        private readonly PandaTeaContext _context;

        public MenusController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            string message = HttpContext.Session.GetString("FirstName") == null ?
                "" : "Welcome " + HttpContext.Session.GetString("FirstName") + "!";
            TempData["Message"] = message;
            ViewBag.Message = TempData["Message"];

            var pandaTeaContext = _context.Menu.Include(m => m.Product);
            return View(await pandaTeaContext.ToListAsync());
        }

        /// <summary>
        /// Check user selection against possiblemenu items
        /// </summary>
        /// <param name="menu">MenuModel data to validate</param>
        /// <returns>ActionResult with status and message</returns>
        public ActionResult CheckMenuItem(Menu menu)
        {
            var menuModel = _context.Menu.Where(s => s.ProductId == menu.ProductId && s.Size == menu.Size);
            var menuId = "";

            if (menuModel.Any())
            {
                foreach (var item in menuModel.ToList())
                {
                    menuId = item.MenuId.ToString();
                }

                HttpContext.Session.SetString("MenuId", menuId);
                HttpContext.Session.SetString("ProductId", (menu.ProductId).ToString());
                HttpContext.Session.SetString("Size", (menu.Size).ToString());
                if(menu.Size == "S")
                {
                    menu.Price = decimal.Parse("3.99");
                }
                else if (menu.Size == "M")
                {
                    menu.Price = decimal.Parse("4.49");
                }
                else if (menu.Size == "L")
                {
                    menu.Price = decimal.Parse("4.99");
                }
                else
                {
                    return Json(new { status = false, message = "Check menu item failed" });
                }

                HttpContext.Session.SetString("UnitPrice", (menu.Price).ToString());

                return Json(new { status = true, message = "Check menu item successful: " + menuId });
            }
            else
            {
                return Json(new { status = false, message = "Check menu item failed" });
            }

        }

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">MenuId to select</param>
        /// <returns>View with Menu data</returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        /// <summary>
        /// Returns View for Create action
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName");
            return View();
        }

        /// <summary>
        /// Inserts Menu data to the database
        /// </summary>
        /// <param name="menu">Menu data to insert</param>
        /// <returns>View with Menu data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,ProductId,Size,Price,Calories")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", menu.ProductId);
            return View(menu);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">MenuId to update/param>
        /// <returns>View with Menu data</returns>
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", menu.ProductId);
            return View(menu);
        }

        /// <summary>
        /// Updates Menu data with a MenuId to the database
        /// </summary>
        /// <param name="id">MenuId to update</param>
        /// <param name="menu">Menu data to update</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("MenuId,ProductId,Size,Price,Calories")] Menu menu)
        {
            if (id != menu.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.MenuId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", menu.ProductId);
            return View(menu);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">MenuId to delete</param>
        /// <returns>View with Menu data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        /// <summary>
        /// Deletes Menu data with a given MenuId from the database
        /// </summary>
        /// <param name="id">MenuId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var menu = await _context.Menu.FindAsync(id);
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if Menu data with the given OrderId exists
        /// </summary>
        /// <param name="id">MenuId to verify</param>
        /// <returns>true if a Menu exists</returns>
        private bool MenuExists(decimal id)
        {
            return _context.Menu.Any(e => e.MenuId == id);
        }
    }
}
