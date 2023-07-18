using HS.Data;
using HS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class UserController : Controller
{
    private readonly MainContext _dbContext;

    public UserController(MainContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLogin model)
    {
        if (ModelState.IsValid)
        {
            var user = await _dbContext.UserLogin.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                // Perform the necessary login actions, such as setting authentication cookies or session variables
                // Example:
                // HttpContext.Session.SetString("UserId", user.Id.ToString());

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password.");
        }

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
