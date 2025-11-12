using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _service;
        public AuthController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _service.ValidateUser(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Email);
                HttpContext.Session.SetString("Role", user.FkRoleName);
                if (user.FkRoleName == "Admin")
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    return RedirectToAction("Index", "Student");

                }
            }
            ViewBag.Error = "Invalid Credentials ";
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            _service.RegisterUser(user);
            return RedirectToAction("Login");
        }
    }
}
