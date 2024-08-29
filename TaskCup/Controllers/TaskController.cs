using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskCup.Models;

namespace TaskCup.Controllers
{
    public class TaskController : Controller
    {
        ApplicationDbContext con;
        public TaskController(ApplicationDbContext _con)
        {
            this.con = _con;
        }
        public IActionResult Index(string statusFilter, string priorityFilter, string searchQuery)
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                var tasksQuery = con.tasks.Include(a => a.userauth_).AsQueryable();

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    tasksQuery = tasksQuery.Where(t => t.Status == statusFilter);
                }

                if (!string.IsNullOrEmpty(priorityFilter))
                {
                    tasksQuery = tasksQuery.Where(t => t.Priority == priorityFilter);
                }

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    tasksQuery = tasksQuery.Where(t => t.Title.Contains(searchQuery) || t.Description.Contains(searchQuery));
                }

                var tasks = tasksQuery.ToList();
                return View(tasks);
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
                List<Userauth> userauths = con.userauths.ToList();
                ViewData["userz"] = userauths;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            } 
        }
        [HttpPost]
        public IActionResult Create(Tasks _task)
        {
            con.tasks.Add(_task);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                List<Userauth> userauths = con.userauths.ToList();
                ViewData["userz"] = userauths;
                var data = con.tasks.Find(id);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            } 
        }
        [HttpPost]
        public IActionResult Edit(Tasks _task)
        {
            con.tasks.Update(_task);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                var data = con.tasks.Include(x => x.userauth_).Where(a => a.Id == id).FirstOrDefault();
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
                var data = con.tasks.Include(x => x.userauth_).Where(a => a.Id == id).FirstOrDefault();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult dlt(int id)
        {
            var data = con.tasks.Find(id);
            con.tasks.Remove(data);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult TaskProgressReport()
        {
            if (HttpContext.Session.GetString("myadmin") != null)
            {
                var taskProgressReport = con.tasks
               .GroupBy(t => t.Status)
               .Select(group => new TaskProgressReportViewModel
               {
                   Status = group.Key,
                   TaskCount = group.Count()
               }).ToList();

                return View(taskProgressReport);
            }
            else
            {
                return RedirectToAction("Login", "AdminAuth");
            } 
        }
    }
}
