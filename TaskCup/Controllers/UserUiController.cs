using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskCup.Models;

namespace TaskCup.Controllers
{
    public class UserUiController : Controller
    {
        ApplicationDbContext con;
        public UserUiController(ApplicationDbContext _con)
        {
            this.con = _con;
        }
        public IActionResult ManageTasks()
        {
            var userId = HttpContext.Session.GetInt32("myuser");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var tasks = con.tasks
                .Include(t => t.userauth_)
                .Where(t => t.U_Id == userId)
                .ToList();

            return View(tasks);
        }
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var task = con.tasks.Find(id);
            if (task != null)
            {
                task.Status = status;
                con.SaveChanges();
            }
            return RedirectToAction("ManageTasks");
        }
        [HttpGet]
        public IActionResult Search(string query)
        {
            var userId = HttpContext.Session.GetInt32("myuser");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = con.tasks
                .Include(t => t.userauth_)
                .Where(t => t.U_Id == userId && (t.Title.Contains(query) || t.Description.Contains(query)))
                .ToList();

            return View("ManageTasks", tasks);
        }
    }
}
