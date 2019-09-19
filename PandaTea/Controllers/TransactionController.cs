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
    public class TransactionController : Controller
    {
        private readonly pandaTeaContext _context;

        public TransactionController(pandaTeaContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var pandaTeaContext = _context.TransactionTbl.Include(t => t.Product).Include(t => t.Store).Include(t => t.User);
            return View(await pandaTeaContext.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTbl = await _context.TransactionTbl
                .Include(t => t.Product)
                .Include(t => t.Store)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionTbl == null)
            {
                return NotFound();
            }

            return View(transactionTbl);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName");
            ViewData["StoreId"] = new SelectList(_context.StoreTbl, "StoreId", "StoreId");
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,UserId,ProductId,StoreId,Quantity,Price,DatePurchased")] TransactionTbl transactionTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName", transactionTbl.ProductId);
            ViewData["StoreId"] = new SelectList(_context.StoreTbl, "StoreId", "StoreId", transactionTbl.StoreId);
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId", transactionTbl.UserId);
            return View(transactionTbl);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTbl = await _context.TransactionTbl.FindAsync(id);
            if (transactionTbl == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName", transactionTbl.ProductId);
            ViewData["StoreId"] = new SelectList(_context.StoreTbl, "StoreId", "StoreId", transactionTbl.StoreId);
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId", transactionTbl.UserId);
            return View(transactionTbl);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TransactionId,UserId,ProductId,StoreId,Quantity,Price,DatePurchased")] TransactionTbl transactionTbl)
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
                    if (!TransactionTblExists(transactionTbl.TransactionId))
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
            ViewData["ProductId"] = new SelectList(_context.ProductTbl, "ProductId", "ProductName", transactionTbl.ProductId);
            ViewData["StoreId"] = new SelectList(_context.StoreTbl, "StoreId", "StoreId", transactionTbl.StoreId);
            ViewData["UserId"] = new SelectList(_context.UserTbl, "UserId", "UserId", transactionTbl.UserId);
            return View(transactionTbl);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTbl = await _context.TransactionTbl
                .Include(t => t.Product)
                .Include(t => t.Store)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionTbl == null)
            {
                return NotFound();
            }

            return View(transactionTbl);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var transactionTbl = await _context.TransactionTbl.FindAsync(id);
            _context.TransactionTbl.Remove(transactionTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionTblExists(decimal id)
        {
            return _context.TransactionTbl.Any(e => e.TransactionId == id);
        }
    }
}
