
using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var records = _service.GetAll();
            return View(records);
        }

        [HttpPost]
        public IActionResult Mark(string studentId, string studentName)
        {
            _service.MarkAttendance(studentId,studentName);
            return RedirectToAction("Index");
        }

        public IActionResult Undo()
        {
            _service.UndoAttendance();
            return RedirectToAction("Index");
        }

        public IActionResult StudentView(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                ViewBag.Message = "Please enter your Student ID";
                return View(new List<AttendanceRecord>());
            }

            var records = _service.GetAttendanceByStudentId(studentId);
            ViewBag.StudentId = studentId;
            return View(records);
        }
    }
}

