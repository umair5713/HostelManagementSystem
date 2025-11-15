//using HostelManagementSystem.Repositories;
//using Microsoft.AspNetCore.Mvc;

//namespace HostelManagementSystem.Controllers
//{
//    private readonly IAttendance_Repository _attendance;
//    public class Attendance_Controller()
//    {
//        //public static AttendanceStack attendance = new AttendanceStack();


//        public IActionResult Index()
//        {
//            var records = _attendance.GetAttendanceList();
//            return View(records);
//        }

//        [HttpPost]
//        public IActionResult Mark(string studentName)
//        {
//            AttendanceRecord record = new AttendanceRecord
//            {
//                StudentName = studentName,
//                Time = DateTime.Now
//            };

//            _attendance.Push(record);
//            return RedirectToAction("Index");
//        }

//        public IActionResult Undo()
//        {
//            _attendance.Pop();
//            return RedirectToAction("Index");

//        }
//    }
//}

using HostelManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HostelManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendance_Repository _attendanceRepository;

        // Constructor injection for the repository
        public AttendanceController(IAttendance_Repository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        // GET: /Attendance/
        public IActionResult Index()
        {
            var records = _attendanceRepository.GetAttendanceList();
            return View(records);
        }

        // POST: /Attendance/Mark
        [HttpPost]
        public IActionResult Mark(string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName))
            {
                TempData["Error"] = "Student name cannot be empty.";
                return RedirectToAction("Index");
            }

            var record = new AttendanceRecord
            {
                StudentName = studentName,
                Time = DateTime.Now
            };

            _attendanceRepository.Push(record);

            return RedirectToAction("Index");
        }

        // POST: /Attendance/Undo
        [HttpPost]
        public IActionResult Undo()
        {
            _attendanceRepository.Pop();
            return RedirectToAction("Index");
        }
    }
}
