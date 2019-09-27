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
    public class UserController : Controller
    {
        private readonly PandaTeaContext _context;

        public UserController(PandaTeaContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserTbl.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTbl = await _context.UserTbl
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTbl == null)
            {
                return NotFound();
            }

            return View(userTbl);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,PhoneNumber,Email,DateRegistered,Password")] UserTbl userTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userTbl);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTbl = await _context.UserTbl.FindAsync(id);
            if (userTbl == null)
            {
                return NotFound();
            }
            return View(userTbl);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,FirstName,LastName,PhoneNumber,Email,DateRegistered,Password")] UserTbl userTbl)
        {
            if (id != userTbl.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTblExists(userTbl.UserId))
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
            return View(userTbl);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTbl = await _context.UserTbl
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTbl == null)
            {
                return NotFound();
            }

            return View(userTbl);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userTbl = await _context.UserTbl.FindAsync(id);
            _context.UserTbl.Remove(userTbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTblExists(decimal id)
        {
            return _context.UserTbl.Any(e => e.UserId == id);
        }
    }
}
