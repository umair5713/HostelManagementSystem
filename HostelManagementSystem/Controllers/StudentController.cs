using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var students = _service.GetAllStudents();
            return View("~/Views/Student/Index.cshtml", students);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Student/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(Student student)
        {
            _service.RegisterStudent(student);
            return RedirectToAction("Index");
        }

        public IActionResult SortByID()
        {
            var sorted = _service.SortByID();
            return View("~/Views/Student/Index.cshtml", sorted);
        }

    }
}
