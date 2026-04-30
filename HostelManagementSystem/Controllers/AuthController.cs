using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _service;
        private readonly AppDbContext _context;

        public AuthController(IUserService service, AppDbContext context)
        {
            _service = service;
            _context = context;
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
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("Role", user.FkRoleName);

                //if (user.FkRoleName == "Admin")
                //{
                //    HttpContext.Session.SetString("Username", "Admin");
                //    return RedirectToAction("Dashboard", "Admin");
                //}
                //else
                //{
                //    // ✅ Find student by email and store StudentName in session
                //    var student = _context.Students
                //                     .FirstOrDefault(s => s.Email == user.Email);

                //    if (student != null)
                //    {
                //        HttpContext.Session.SetString("Username", student.StudentName);
                //        HttpContext.Session.SetInt32("StudentID", student.StudentID);
                //    }
                //    else
                //    {
                //        HttpContext.Session.SetString("Username", user.Email);
                //    }

                //    return RedirectToAction("Dashboard", "Student");
                //}

                if (user.FkRoleName == "Admin")
                {
                    HttpContext.Session.SetString("Username", "Admin");
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    // USE fk_student_id directly — much more reliable
                    if (user.FkStudentId.HasValue)
                    {
                        var student = _context.Students
                            .FirstOrDefault(s => s.StudentID == user.FkStudentId.Value);

                        if (student != null)
                        {
                            HttpContext.Session.SetString("Username", student.StudentName);
                            HttpContext.Session.SetInt32("StudentID", student.StudentID);
                        }
                    }
                    else
                    {
                        // Fallback if fk_student_id not set
                        HttpContext.Session.SetString("Username", user.Email);
                    }

                    return RedirectToAction("Dashboard", "Student");
                }
            }

            ViewBag.Error = "Invalid Credentials";
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
            user.FkRoleName = "Student"; // default role
            _service.RegisterUser(user);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
