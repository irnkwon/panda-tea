/* UserController.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-09-12: Created
 *          Ji Hong Ahn, 2019-09-27: Refined Model classes
 *          Ji Hong Ahn, 2019-10-10: Refined codes
 *                                   Added documentation comments and header comments
 *                                   Added Login, Logout, and Validate action
 */
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

namespace PandaTea.Controllers
{
    /// <summary>
    /// Controller class for UserModel
    /// </summary>
    public class UserController : Controller
    {
        private readonly PandaTeaContext _context;

        public UserController(PandaTeaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns View for Index action
        /// </summary>
        /// <returns>View with UserModel data</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserModel.ToListAsync());
        }

        /// <summary>
        /// Returns View for Login action
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Removes login session and returns ActionResult object
        /// </summary>
        /// <returns>ActionResult object with status and message</returns>
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("FirstName");
            RedirectToAction("Index", "Menu");
            return Json(new { status = true, message = "Logout Successful" });
        }

        /// <summary>
        /// Validates email and password to login
        /// </summary>
        /// <param name="user">UserModel data to validate</param>
        /// <returns>ActionResult object with status and message</returns>
        public ActionResult Validate(UserModel user)
        {
            var userModel = _context.UserModel.Where(s => s.Email == user.Email);
            string userId = "";
            string firstName = "";
            if (userModel.Any())
            {
                if (userModel.Where(s => s.Password == user.Password).Any())
                {
                    foreach (var item in userModel.ToList())
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

        /// <summary>
        /// Returns View for Details action
        /// </summary>
        /// <param name="id">UserId to select</param>
        /// <returns>View with UserModel data</returns>
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        /// <summary>
        /// Returns View for Create action
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Inserts UserModel data to the database
        /// </summary>
        /// <param name="userModel">UserModel data to insert</param>
        /// <returns>View with UserModel data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,PhoneNumber,Email,DateRegistered,Password")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        /// <summary>
        /// Returns View for Edit action
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns>View with UserModel data</returns>
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        /// <summary>
        /// Updates UserModel data with a UserId to the database
        /// </summary>
        /// <param name="id">UserId to update</param>
        /// <param name="userModel">UserModel data to update</param>
        /// <returns>View with UserModel data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,FirstName,LastName,PhoneNumber,Email,DateRegistered,Password")] UserModel userModel)
        {
            if (id != userModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.UserId))
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
            return View(userModel);
        }

        /// <summary>
        /// Returns View for Delete action
        /// </summary>
        /// <param name="id">UserId to delete</param>
        /// <returns>View with UserModel data</returns>
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        /// <summary>
        /// Deletes UserModel data with a given UserId from the database
        /// </summary>
        /// <param name="id">UserId to delete</param>
        /// <returns>IActionResult to return to Index action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userModel = await _context.UserModel.FindAsync(id);
            _context.UserModel.Remove(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifies if UserModel data with the given UserId exists
        /// </summary>
        /// <param name="id">UserId to verify</param>
        /// <returns>true if a UserModel exists</returns>
        private bool UserModelExists(decimal id)
        {
            return _context.UserModel.Any(e => e.UserId == id);
        }
    }
}
