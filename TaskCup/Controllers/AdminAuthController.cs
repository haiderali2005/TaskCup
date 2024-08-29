using Microsoft.AspNetCore.Mvc;
using TaskCup.Models;

namespace TaskCup.Controllers
{
    public class AdminAuthController : Controller
    {
        ApplicationDbContext _con;
        public AdminAuthController(ApplicationDbContext con)
        {
            this._con = con;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Adminauth _admin)
        {
            _con.adminauths.Add(_admin);
            _con.SaveChanges();
            TempData["register"] = "SUCCESSFULLY REGISTERED";
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Adminauth _admin)
        {
            var data = _con.adminauths.Where(a => a.A_Email == _admin.A_Email && a.A_Password == _admin.A_Password).FirstOrDefault();

            if (data != null)
            {
                HttpContext.Session.SetString("myadmin", data.A_Username);
                return RedirectToAction("Index", "AdminHome");
            }
            else
            {
                TempData["login"] = "THIS EMAIL DOESN'T EXIST";
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                HttpContext.Session.Remove("myadmin");
                TempData["logout"] = "SUCCESSFULLY LOGOUT";
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forgot(Adminauth _admin, string A_Password)
        {
            var data = _con.adminauths.Where(a => a.A_Email == _admin.A_Email).FirstOrDefault();
            if (data != null)
            {
                data.A_Password = A_Password;
                _con.SaveChanges();
                TempData["forgot"] = "YOUR PASSWORD HAS BEEN CHANGED";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["forgot2"] = "THIS EMAIL DOESN'T EXIST";
            }
            return View();
        }
        public IActionResult UpdateProfile()
        {
            var adminUsername = HttpContext.Session.GetString("myadmin");

            if (string.IsNullOrEmpty(adminUsername))
            {
                return RedirectToAction("Login"); 
            }
            var admin = _con.adminauths.FirstOrDefault(a => a.A_Username == adminUsername);

            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            return View(admin); 
        }
        [HttpPost]
        public IActionResult UpdateProfile(Adminauth updatedAdmin)
        {
            if (ModelState.IsValid)
            {
                var existingAdmin = _con.adminauths.FirstOrDefault(a => a.A_Id == updatedAdmin.A_Id);

                if (existingAdmin != null)
                {
                    existingAdmin.A_Username = updatedAdmin.A_Username;
                    existingAdmin.A_Email = updatedAdmin.A_Email;
                    existingAdmin.A_Password = updatedAdmin.A_Password;
                    _con.adminauths.Update(existingAdmin);
                    _con.SaveChanges();
                    TempData["update"] = "Profile updated successfully.";
                    return RedirectToAction("UpdateProfile"); 
                }
                else
                {
                    return NotFound("Admin not found.");
                }
            }
            return View(updatedAdmin); 
        }
    }
}
