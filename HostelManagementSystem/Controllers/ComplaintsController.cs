using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly IComplaintService _service;

        public ComplaintsController(IComplaintService service)
        {
            _service = service;
        }

        public IActionResult Queue()
        {
            var complaints = _service.GetAllComplaints();
            return View("~/Views/Complaints/Queue.cshtml", complaints);
        }
        [HttpGet]
        public IActionResult Submit()
        {
            return View("~/Views/Complaints/Submit.cshtml");
        }

        [HttpPost]
        public IActionResult Submit(Complaint complaint)
        {
            if (complaint == null || string.IsNullOrEmpty(complaint.StudentName))
            {
                TempData["Error"] = "Invalid complaint";
                return RedirectToAction("Queue");
            }

            _service.SubmitComplaint(complaint);

            // Directly show updated queue instead of redirect
            var complaints = _service.GetAllComplaints();
            return View("~/Views/Complaints/Queue.cshtml", complaints);
        }
        [HttpGet]
        public IActionResult Process()
        {
            return View("~/Views/Complaints/Processed.cshtml");
        }
        
        [HttpPost]
        public IActionResult ProcessNext()
        {
            var complaint = _service.ProcessComplaint();

            if (complaint == null)
            {
                TempData["Message"] = "No complaints in queue";
                return RedirectToAction("Queue");
            }

            return View("~/Views/Complaints/Processed.cshtml", complaint);
        }
    }
}
