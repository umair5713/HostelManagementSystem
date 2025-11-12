using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        public static StudentLinkedList students = new StudentLinkedList();
        public IActionResult Index()
        {
            var allStudents = students.GetStudents();
            return View("~/Views/Student/Index.cshtml", allStudents);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Student/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(Student student)
        {
            students.add_student(student);
            return RedirectToAction("Index");
        }
    }
}
