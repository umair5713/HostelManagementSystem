using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IBillingRepository
    {
        void SaveBill(Bill bill);
        Bill[] GetAllBills();
        Bill GetBillByStudent(int studentId);
    }
}
