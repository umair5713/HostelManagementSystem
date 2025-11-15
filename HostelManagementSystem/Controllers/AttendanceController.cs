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
        public IActionResult Mark(string studentName)
        {
            _service.MarkAttendance(studentName);
            return RedirectToAction("Index");
        }

        public IActionResult Undo()
        {
            _service.UndoAttendance();
            return RedirectToAction("Index");
        }
    }
}
