using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class RoomAllocationController : Controller
    {
        private readonly IRoomAllocationService _service;
        private readonly IStudentService _studentService;

        public RoomAllocationController(
            IRoomAllocationService service,
            IStudentService studentService)
        {
            _service = service;
            _studentService = studentService;
        }

        public IActionResult QueueList()
        {
            var allStudents = _studentService.GetAllStudents();
            var waiting = _service.GetQueue();

            ViewBag.AllStudents = allStudents;
            return View("~/Views/RoomAllocation/QueueList.cshtml", waiting);
        }

        [HttpPost]
        public IActionResult AddToQueue(Student student)
        {
            if (student == null)
            {
                TempData["Error"] = "Invalid student information.";
                return RedirectToAction("QueueList");
            }

            _service.AddToQueue(student);
            return RedirectToAction("QueueList");
        }

        public IActionResult AllocateRoom()
        {
            var student = _service.AllocateRoom();

            if (student == null)
            {
                TempData["Message"] = "No students in queue.";
                return View("~/Views/RoomAllocation/Allocated.cshtml", null);
            }

            return View("~/Views/RoomAllocation/Allocated.cshtml", student);
        }
    }
}
