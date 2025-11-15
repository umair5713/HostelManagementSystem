using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        // ❗ Ideally yeh DI se aana chahiye — but keeping your existing structure
        private static readonly StudentLinkedList _students = new StudentLinkedList();

        // GET: /Student/
        public IActionResult Index()
        {
            var allStudents = _students.GetStudents();
            return View("~/Views/Student/Index.cshtml", allStudents);
        }

        // GET: /Student/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Student/Register.cshtml");
        }

        // POST: /Student/Register
        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Student/Register.cshtml", student);
            }

            _students.add_student(student);
            return RedirectToAction("Index");
        }

        // GET: /Student/SortByID
        public IActionResult SortByID()
        {
            _students.SortbyID();
            var sortedStudents = _students.GetStudents();

            return View("~/Views/Student/Index.cshtml", sortedStudents);
        }
    }
}
