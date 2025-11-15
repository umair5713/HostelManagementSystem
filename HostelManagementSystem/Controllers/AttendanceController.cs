using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        public static AttendanceStack attendance = new AttendanceStack();

        public IActionResult Index()
        {
            var records = attendance.GetAttendanceList();
            return View(records);
        }

        [HttpPost]
        public IActionResult Mark(string studentName)
        {
            AttendanceRecord record = new AttendanceRecord
            {
                StudentName = studentName,
                Time = DateTime.Now
            };

            attendance.Push(record);
            return RedirectToAction("Index");
        }

        public IActionResult Undo()
        {
            attendance.Pop();
            return RedirectToAction("Index");
        }
    }
}
