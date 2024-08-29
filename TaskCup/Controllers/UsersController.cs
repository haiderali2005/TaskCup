using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskCup.Models;

namespace TaskCup.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext con;
        public UsersController(ApplicationDbContext _con)
        {
            this.con = _con;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                var data = con.userauths.ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            }
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            }
        }
        [HttpPost]
        public IActionResult Create(Userauth _user)
        {
            con.userauths.Add(_user);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                var data = con.userauths.Find(id);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            }  
        }
        [HttpPost]
        public IActionResult Edit(Userauth _user)
        {
            con.userauths.Update(_user);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                var data = con.userauths.Where(a => a.U_Id == id).FirstOrDefault();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            }
        }
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                var data = con.userauths.Where(a => a.U_Id == id).FirstOrDefault();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            }        
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult Delete2(int id)
        {
            var data = con.userauths.Find(id);
            con.userauths.Remove(data);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
