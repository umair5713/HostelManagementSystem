using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

public class StudentFeeController : Controller
{
    private readonly IFeeService _service;

    public StudentFeeController(IFeeService service)
    {
        _service = service;
    }
    [HttpGet]
    public IActionResult Pay(int id)
    {
        var fee = _service.GetAllFees().FirstOrDefault(f => f.FeeID == id);
        return View(fee);
    }
    [HttpPost]
    [HttpPost]
    public IActionResult ConfirmPay(int id)
    {
        _service.PayFee(id);
        return RedirectToAction("MyFees");
    }
    public IActionResult MyFees()
    {
        var studentId = HttpContext.Session.GetInt32("StudentID");

        var fees = _service.GetAllFees()
            .Where(f => f.StudentID == studentId)
            .ToList();

        return View(fees);
    }
}