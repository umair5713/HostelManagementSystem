using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IBillingService
    {
        void GenerateBill(int studentId, int mealCount, int mealRate);
        Bill GetBill(int studentId);
        Bill[] GetAllBills();
        void MarkAsPaid(int studentId);
    }
}
