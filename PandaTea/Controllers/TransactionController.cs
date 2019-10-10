using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

namespace PandaTea.Controllers
{
    public class TransactionController : Controller
    {
        private readonly PandaTeaContext _context;

        public TransactionController(PandaTeaContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionTbl.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTbl = await _context.TransactionTbl
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
