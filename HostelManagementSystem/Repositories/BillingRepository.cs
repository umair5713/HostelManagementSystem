using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private List<Bill> bills = new List<Bill>();


        public void SaveBill(Bill bill)
        {
            var existing = bills.FirstOrDefault(b => b.StudentId == bill.StudentId);
            if (existing == null) bills.Add(bill);
            else
            {
                existing.TotalMeals = bill.TotalMeals;
                existing.Amount = bill.Amount;
                existing.IsPaid = bill.IsPaid;
            }
        }


        public Bill[] GetAllBills() => bills.ToArray();


        public Bill GetBillByStudent(int studentId) => bills.FirstOrDefault(b => b.StudentId == studentId);
    }
}
