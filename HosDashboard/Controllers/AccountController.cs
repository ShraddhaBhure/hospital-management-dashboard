using HS.Data;
using HS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HosDashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly MainContext db;
        public AccountController(MainContext _db)
        {
            db = _db;
        }
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //public ActionResult Validate(Admin admin)
        //{
        //    var _admin = db.Admins.Where(s => s.Email == admin.Email);
        //    if (_admin.Any())
        //    {
        //        if (_admin.Where(s => s.Password == admin.Password).Any())
        //        {

        //            return Json(new { status = true, message = "Login Successfull!" });
        //        }
        //        else
        //        {
        //            return Json(new { status = false, message = "Invalid Password!" });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { status = false, message = "Invalid Email!" });
        //    }
        //}

        [HttpGet]
        public IActionResult Login()
        {
            Admin admin = new Admin();
           // Userlogin2 _user = new Userlogin2();
            TempData["toast"] = "Welcome!";

           /// _toastNotification.Success("Login Successfull.");

            return View(admin);
        }

        [HttpPost]
        public IActionResult Login(Admin userp)
        {
        //    MainContext _dash = new MainContext();
           // TDbContext _dash = new TDbContext();
            var status = db.Admins.Where(m => m.Name == userp.Name && m.Password == userp.Password).SingleOrDefault();
            if (status != null)
            {
                bool isValid = (status.Name == userp.Name && status.Password == userp.Password);
                if (isValid)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userp.Name) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("userId", userp.Name);


                    ////////HttpContext.Session.SetString("Role", loginResult.Role);
                    //HttpContext.Session.SetInt32("CurrentUserId", loginResult.Id);
                  //  _toastNotification.Success("Login Successfull.");

                    //--------------------new //add user datata to database---------------------
                    //var username = User.Identity.Name;

                    //var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    //var visitedAction = ControllerContext.RouteData.Values["action"].ToString();

                    //var loggedInUser = new LoggedInUser
                    //{
                    //    Username = username,
                    //    IpAddress = ipAddress,
                    //    LoginTime = DateTime.Now,
                    //    VisitedAction = visitedAction
                    //};

                    //_dash.LoggedInUsers.Add(loggedInUser);
                    //_dash.SaveChangesAsync();
                    //-------------------end-----------
                    TempData["toast"] = "Login Successfull!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["toast"] = "Invalid Password!";
                TempData["errorUsername"] = "Invalid Username Or Password";
             //   _toastNotification.Success("Login Failed");
                return View(userp);
            }
            return View(userp);
        }




        public IActionResult Logout()
        {
            AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
            var storeCookies = Request.Cookies.Keys;
            foreach (var cookies in storeCookies)
            {
                Response.Cookies.Delete(cookies);
            }
            TempData["toast"] = "User Logged Out!";
            return RedirectToAction("Login", "login");

        }

        //------------Another Method----------------
        //HttpContext.Session.Clear();
        //HttpContext.Session.Remove("UserName");
        //public IActionResult GetUserList()
        //{
        //    TDbContext _dash = new TDbContext();
        //    var status = _dash.Userlogin2.ToList();
        //    return View(status);
        //}
        //public IActionResult GetUserLogsActivity()
        //{
        //    TDbContext _dash = new TDbContext();
        //    var statusi = _dash.LoggedInUsers.ToList();


        //    return View(statusi);
        //}
    //    //------------------add user datata to database--------------------------
    //    public async Task<IActionResult> Index()
    //    {
    //        TDbContext _dash = new TDbContext();

    //        var username = User.Identity.Name;
    //        //  String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString().Replace("::ffff:", ""); ;
    //        var visitedAction = ControllerContext.RouteData.Values["action"].ToString();
    //        if (ipAddress == "::1")
    //        {
    //            ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
    //        }
    //        var loggedInUser = new LoggedInUser
    //        {
    //            Username = username,
    //            IpAddress = ipAddress,
    //            LoginTime = DateTime.Now,
    //            VisitedAction = visitedAction
    //        };

    //        _dash.LoggedInUsers.Add(loggedInUser);
    //        await _dash.SaveChangesAsync();
    //        //await _signInManager.SignInAsync(user, model.RememberMe);

    //        //return RedirectToAction("Index", "Home");

    //        //ModelState.AddModelError("", "Invalid username or password.");
    //        return View();
    //    }


    //    [HttpGet]
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public IActionResult Create(Userlogin2 customer)
    //    {
    //        TDbContext _dash = new TDbContext();
    //        if (ModelState.IsValid)
    //        {
    //            _dash.Userlogin2.Add(customer);
    //            _dash.SaveChanges();
    //            TempData["errorUsername"] = "Saved data";
    //        }

    //        return View(customer);
    //    }
    //}
}
    }

