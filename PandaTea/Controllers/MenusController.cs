/* OrderController.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-09-12: Created
 *          Ji Hong Ahn, 2019-10-10: Refined codes
 *                                   Added documentation comments and header comments
 *          Samantha Dang, 2019-10-31: add CheckMenuItem function again
 *  */

using System;
using System.Collections.Generic;
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

        // GET: Menus
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
            //var qty = menu.qty;
            if (menuModel.Any())
            {
                foreach (var item in menuModel.ToList())
                {
                    menuId = item.MenuId.ToString();
                }
                
                HttpContext.Session.SetString("MenuId", menuId);

                return Json(new { status = true, message = "Check menu item successful: " + menuId });
            }
            else
            {
                return Json(new { status = false, message = "Check menu item failed" });
            }

        }

        // GET: Menus/Details/5
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

        // GET: Menus/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Menus/Edit/5
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

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Menus/Delete/5
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

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var menu = await _context.Menu.FindAsync(id);
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(decimal id)
        {
            return _context.Menu.Any(e => e.MenuId == id);
        }
    }
}
