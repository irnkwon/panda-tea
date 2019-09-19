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
    public class StoreController : Controller
    {
        private readonly PandaTeaContext _context;

        public StoreController(PandaTeaContext context)
        {
            _context = context;
        }

        // GET: Store
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoreTbl.ToListAsync());
        }

        // GET: Store/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTbl = await _context.StoreTbl
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (storeTbl == null)
            {
                return NotFound();
            }

            return View(storeTbl);
        }

        // GET: Store/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreName,Address,Street,City,State,Country,PostalCode")] StoreTbl storeTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeTbl);
        }

        // GET: Store/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTbl = await _context.StoreTbl.FindAsync(id);
            if (storeTbl == null)
            {
                return NotFound();
            }
            return View(storeTbl);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("StoreId,StoreName,Address,Street,City,State,Country,PostalCode")] StoreTbl storeTbl)
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
                    if (!StoreTblExists(storeTbl.StoreId))
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

        // GET: Store/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTbl = await _context.StoreTbl
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (storeTbl == null)
            {
                return NotFound();
            }

            return View(storeTbl);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var storeTbl = await _context.StoreTbl.FindAsync(id);
            _context.StoreTbl.Remove(storeTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreTblExists(decimal id)
        {
            return _context.StoreTbl.Any(e => e.StoreId == id);
        }
    }
}
