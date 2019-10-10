using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("FirstName");
            RedirectToAction("Index", "Menu");
            return Json(new { status = true, message = "Logout Successful" });
        }

        public ActionResult Validate(UserTbl user)
        {
            var userTbl = _context.UserTbl.Where(s => s.Email == user.Email);
            string userId = "";
            string firstName = "";
            if (userTbl.Any())
            {
                if (userTbl.Where(s => s.Password == user.Password).Any())
                {
                    foreach (var item in userTbl.ToList())
                    {
                        userId = item.UserId.ToString();
                        firstName = item.FirstName.ToString();
                    }

                    HttpContext.Session.SetString("UserId", userId);
                    HttpContext.Session.SetString("FirstName", firstName);

                    return Json(new { status = true, message = "Logged In" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email" });
            }
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
