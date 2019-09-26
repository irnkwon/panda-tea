using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

namespace PandaTea.Controllers
{
    public class MenuController : Controller
    {
        private readonly PandaTeaContext _context;

        public MenuController(PandaTeaContext context)
        {
            _context = context;
        }

        // GET: Menu
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuTbl.ToListAsync());
        }

        // GET: Menu/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuTbl = await _context.MenuTbl
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuTbl == null)
            {
                return NotFound();
            }

            return View(menuTbl);
        }

        // GET: Menu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,ProductId,Size,Price,Calories")] MenuTbl menuTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuTbl);
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuTbl = await _context.MenuTbl.FindAsync(id);
            if (menuTbl == null)
            {
                return NotFound();
            }
            return View(menuTbl);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("MenuId,ProductId,Size,Price,Calories")] MenuTbl menuTbl)
        {
            if (id != menuTbl.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuTblExists(menuTbl.MenuId))
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
            return View(menuTbl);
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuTbl = await _context.MenuTbl
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuTbl == null)
            {
                return NotFound();
            }

            return View(menuTbl);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var menuTbl = await _context.MenuTbl.FindAsync(id);
            _context.MenuTbl.Remove(menuTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuTblExists(decimal id)
        {
            return _context.MenuTbl.Any(e => e.MenuId == id);
        }
    }
}
