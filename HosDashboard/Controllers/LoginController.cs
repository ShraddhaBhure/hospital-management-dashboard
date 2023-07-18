using Microsoft.AspNetCore.Mvc;
using HS.Models;
using Microsoft.AspNetCore.Identity;
using HS.Data;
using Microsoft.EntityFrameworkCore;

namespace HosDashboard.Controllers
{
    public class LoginController : Controller
    {

        private readonly MainContext _dbContext;

        public LoginController(MainContext db)
        {
            _dbContext = db;
        }



        [HttpGet]
        public IActionResult LoginPage2()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPage2(UserLogin model)
        {
         
                var user = await _dbContext.UserLogin.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
           
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    
     
        public IActionResult Logout()
        {
            // Perform the necessary logout actions, such as clearing authentication cookies or session variables
            // Example:
            // HttpContext.Session.Clear();

            return RedirectToAction("LoginPage2");
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _dbContext.UserLogin.ToListAsync();
            return View(users);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.UserLogin.Add(user);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


    
            public IActionResult CreateUser()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.UserLogin.Add(user);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.UserLogin.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserLogin user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.UserLogin.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.UserLogin.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _dbContext.UserLogin.FindAsync(id);
            _dbContext.UserLogin.Remove(user);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _dbContext.UserLogin.Any(e => e.Id == id);
        }
    }

}

