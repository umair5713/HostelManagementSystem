
using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _service;
        private readonly IStudentService _studentservice;
        public AttendanceController(IAttendanceService service, IStudentService studentservice)
        {
            _service = service;
            _studentservice = studentservice;
        }

        public IActionResult Index()
        {
            var records = _service.GetAll();
            ViewBag.Students = _studentservice.GetAllStudents();
            return View(records);
        }

        [HttpPost]
        public IActionResult Mark(int studentId, string studentName)
        {
            if (studentId == 0)
            {
                TempData["Error"] = "Invalid Student ID";
                return RedirectToAction("Index");
            }
            _service.MarkAttendance(studentId,studentName);
            return RedirectToAction("Index");
        }

        //public IActionResult StudentView(int studentId)
        //{
        //    var records = _service.GetByStudent(studentId);
        //    ViewBag.StudentId = studentId;
        //    return View(records);
        //}

        public IActionResult StudentView(int studentId)
        {
            // If studentId not passed, get from session
            if (studentId == 0)
                studentId = HttpContext.Session.GetInt32("StudentID") ?? 0;

            if (studentId == 0)
                return RedirectToAction("Login", "Auth");

            var records = _service.GetByStudent(studentId);
            ViewBag.StudentId = studentId;
            return View(records);
        }

        public IActionResult HasAttendance(int studentId)
        {
            var hasAttendance = _service.HasAttendance(studentId);
            ViewBag.HasAttendance = hasAttendance;
            return View();
        }

        
        public IActionResult AttendanceCount(int studentId)
        {
            var count = _service.GetCount(studentId);
            ViewBag.Count = count;
            return View();
        }
    }
}

