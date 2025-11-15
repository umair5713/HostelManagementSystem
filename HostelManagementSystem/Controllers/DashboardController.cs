using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
