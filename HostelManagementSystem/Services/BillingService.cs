using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _repo;

        public BillingService(IBillingRepository repo)
        {
            _repo = repo;
        }

        public void GenerateBill(int studentId, int mealCount, int mealRate)
        {
            int amount = mealCount * mealRate;

            Bill bill = new Bill
            {
                StudentId = studentId,
                TotalMeals = mealCount,
                Amount = amount,
                IsPaid = false
            };

            _repo.SaveBill(bill);
        }

        public Bill GetBill(int studentId)
        {
            return _repo.GetBillByStudent(studentId);
        }

        public Bill[] GetAllBills()
        {
            return _repo.GetAllBills();
        }

        public void MarkAsPaid(int studentId)
        {
            Bill bill = _repo.GetBillByStudent(studentId);
            if (bill != null)
            {
                bill.IsPaid = true;
                _repo.SaveBill(bill);
            }
        }
    }
}
