using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskCup.Models;

namespace TaskCup.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext _con;
        public UserController(ApplicationDbContext con)
        {
            this._con = con;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Userauth _user)
        {
            _con.userauths.Add(_user);
            _con.SaveChanges();
            TempData["register"] = "SUCCESSFULLY REGISTERED";
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Userauth _user)
        {
            var data = _con.userauths.Where(user => user.U_Email == _user.U_Email && user.U_Password == _user.U_Password).FirstOrDefault();

            if (data != null)
            {
                HttpContext.Session.SetInt32("myuser", data.U_Id);
                return RedirectToAction("Managetasks", "UserUI");
            }
            else
            {
                TempData["login"] = "THIS EMAIL DOESN'T EXIST";
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("myuser") != null)
            {
                HttpContext.Session.Remove("myuser");
                TempData["logout"] = "SUCCESSFULLY LOGOUT";
                return RedirectToAction("login");
            }
            return View();
        }
        public IActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forgot(Userauth _user, string U_Password)
        {
            var data = _con.userauths.Where(a => a.U_Email == _user.U_Email).FirstOrDefault();
            if (data != null)
            {
                data.U_Password= U_Password;
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
    }
}
