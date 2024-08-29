using Microsoft.AspNetCore.Mvc;

namespace TaskCup.Controllers
{
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
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
    }
}
