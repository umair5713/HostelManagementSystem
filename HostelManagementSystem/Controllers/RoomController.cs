using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class RoomController : Controller
    {
        // Shared queue for the system (same as your original logic)
        private static readonly RoomQueue _queue = new RoomQueue();

        public IActionResult QueueList()
        {
            // Get all students from StudentController (static list)
            var allStudents = StudentController.students.GetStudents();
            var waitingStudents = _queue.get_queue();

            ViewBag.AllStudents = allStudents;

            return View("~/Views/RoomAllocation/QueueList.cshtml", waitingStudents);
        }

        [HttpPost]
        public IActionResult AddToQueue(Student student)
        {
            if (student == null)
            {
                TempData["Error"] = "Invalid student information.";
                return RedirectToAction("QueueList");
            }

            _queue.enqueue(student);
            return RedirectToAction("QueueList");
        }

        public IActionResult AllocateRoom()
        {
            var student = _queue.dequeue();

            // Queue empty
            if (student == null)
            {
                TempData["Message"] = "No students in queue.";
                return View("~/Views/RoomAllocation/Allocated.cshtml", null);
            }

            // Student allocated
            return View("~/Views/RoomAllocation/Allocated.cshtml", student);
        }
    }
}
