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

        // ADMIN: View all complaints
        public IActionResult Index()
        {
            var complaints = _service.GetAllComplaints();
            return View("~/Views/Complaints/Index.cshtml", complaints);
        }

        // ADMIN: Update status to "Received"
        [HttpPost]
        public IActionResult MarkReceived(int id)
        {
            _service.UpdateComplaintStatus(id, "Received");
            return RedirectToAction("Index");
        }

        // ADMIN: Update status to "In Progress"
        [HttpPost]
        public IActionResult MarkInProgress(int id)
        {
            _service.UpdateComplaintStatus(id, "In Progress");
            return RedirectToAction("Index");
        }

        // ADMIN: Update status to "Done"
        [HttpPost]
        public IActionResult MarkDone(int id)
        {
            _service.UpdateComplaintStatus(id, "Done");
            return RedirectToAction("Index");
        }

        // ADMIN: View complaint details
        public IActionResult Details(int id)
        {
            var complaint = _service.GetComplaintById(id);
            if (complaint == null)
            {
                return NotFound();
            }
            return View("~/Views/Complaints/Details.cshtml", complaint);
        }

        // STUDENT: View submit complaint form (GET)
        [HttpGet]
        public IActionResult Submit()
        {
            return View("~/Views/Complaints/Submit.cshtml");
        }

        // STUDENT: Submit complaint (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Complaint complaint)
        {
            complaint.Time = DateTime.Now;
            complaint.Status = "Pending";

            _service.AddComplaint(complaint);

            TempData["Success"] = "Complaint submitted successfully!";
            return RedirectToAction("Index");
        }
    }
}