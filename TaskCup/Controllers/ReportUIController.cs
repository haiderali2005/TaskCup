using Microsoft.AspNetCore.Mvc;
using TaskCup.Models;

namespace TaskCup.Controllers
{
    public class ReportUIController : Controller
    {
        ApplicationDbContext con;
        public ReportUIController(ApplicationDbContext _Con)
        {
            this.con = _Con;
        }
        public IActionResult Reports()
        {
            var userId = HttpContext.Session.GetInt32("myuser");
            if (userId == null)
            {
                return RedirectToAction("Login", "User"); 
            }

            var taskProgressReport = con.tasks
                .Where(t => t.U_Id == userId) 
                .GroupBy(t => t.Status)
                .Select(group => new TaskProgressReportViewModel
                {
                    Status = group.Key,
                    TaskCount = group.Count()
                }).ToList();

            return View(taskProgressReport);
        }

    }
}
