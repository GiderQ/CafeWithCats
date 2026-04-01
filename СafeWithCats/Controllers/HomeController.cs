using Microsoft.AspNetCore.Mvc;

namespace CafeWithCats.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Welcome to CafeWithCats!";
            return View();
        }
    }
}