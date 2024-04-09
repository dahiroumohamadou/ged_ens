using Microsoft.AspNetCore.Mvc;

namespace soft.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
