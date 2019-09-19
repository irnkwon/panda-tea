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
    public class ProductController : Controller
    {
        private readonly PandaTeaContext _context;

        public ProductController(PandaTeaContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductTbl.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTbl = await _context.ProductTbl
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productTbl == null)
            {
                return NotFound();
            }

            return View(productTbl);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Size,Price")] ProductTbl productTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTbl);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTbl = await _context.ProductTbl.FindAsync(id);
            if (productTbl == null)
            {
                return NotFound();
            }
            return View(productTbl);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ProductId,ProductName,Size,Price")] ProductTbl productTbl)
        {
            if (id != productTbl.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTblExists(productTbl.ProductId))
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
            return View(productTbl);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTbl = await _context.ProductTbl
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productTbl == null)
            {
                return NotFound();
            }

            return View(productTbl);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productTbl = await _context.ProductTbl.FindAsync(id);
            _context.ProductTbl.Remove(productTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTblExists(decimal id)
        {
            return _context.ProductTbl.Any(e => e.ProductId == id);
        }
    }
}
