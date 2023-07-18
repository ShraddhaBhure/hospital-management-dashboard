using Microsoft.AspNetCore.Mvc;
using HS.Models;
using Microsoft.AspNetCore.Identity;
using HS.Data;

namespace HosDashboard.Controllers
{
    public class LoginController : Controller
    {

        private readonly MainContext _db;

        public LoginController(MainContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoginPage2()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPage(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.UserLogin.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Redirect to the desired page after successful login
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPage2(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.UserLogin.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Redirect to the desired page after successful login
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            // Perform the necessary logout actions, such as clearing authentication cookies or session variables
            // Example:
            // HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }

}

