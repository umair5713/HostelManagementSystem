using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
