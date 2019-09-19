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
    public class ReviewController : Controller
    {
        private readonly pandaTeaContext _context;

        public ReviewController(pandaTeaContext context)
        {
            _context = context;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            var pandaTeaContext = _context.ReviewTbl.Include(r => r.Product).Include(r => r.User);
            return View(await pandaTeaContext.ToListAsync());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTbl = await _context.ReviewTbl
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (reviewTbl == null)
            {
                return NotFound();
            }

            return View(reviewTbl);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName");
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,UserId,ProductId,Score,Text,DateReviewed")] ReviewTbl reviewTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviewTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName", reviewTbl.ProductId);
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId", reviewTbl.UserId);
            return View(reviewTbl);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTbl = await _context.ReviewTbl.FindAsync(id);
            if (reviewTbl == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName", reviewTbl.ProductId);
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId", reviewTbl.UserId);
            return View(reviewTbl);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ReviewId,UserId,ProductId,Score,Text,DateReviewed")] ReviewTbl reviewTbl)
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
                    if (!ReviewTblExists(reviewTbl.ReviewId))
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
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName", reviewTbl.ProductId);
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId", reviewTbl.UserId);
            return View(reviewTbl);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTbl = await _context.ReviewTbl
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (reviewTbl == null)
            {
                return NotFound();
            }

            return View(reviewTbl);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var reviewTbl = await _context.ReviewTbl.FindAsync(id);
            _context.ReviewTbl.Remove(reviewTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewTblExists(decimal id)
        {
            return _context.ReviewTbl.Any(e => e.ReviewId == id);
        }
    }
}
