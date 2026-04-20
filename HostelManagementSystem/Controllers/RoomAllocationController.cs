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

        public IActionResult Index()
        {
            var rooms = _service.GetAllRooms();
            ViewBag.AllStudents = _studentService.GetAllStudents();
            return View("~/Views/RoomAllocation/Index.cshtml", rooms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AllocateRoom(int studentId, string roomNo)
        {
            if (string.IsNullOrEmpty(roomNo))
            {
                TempData["Error"] = "Room number cannot be empty.";
                return RedirectToAction("Index");
            }

            try
            {
                _service.AllocateRoom(studentId, roomNo);
                TempData["Success"] = $"Room {roomNo} allocated successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeallocateRoom(int studentId)
        {
            _service.DeallocateRoom(studentId);
            TempData["Success"] = "Room deallocated successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult CheckRoom(string roomNo)
        {
            var student = _service.GetByRoom(roomNo);
            ViewBag.RoomNo = roomNo;
            ViewBag.IsTaken = student != null;
            return View("~/Views/RoomAllocation/CheckRoom.cshtml", student);
        }
    }
}


