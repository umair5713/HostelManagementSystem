using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        //private List<Bill> bills = new List<Bill>();


        //public void SaveBill(Bill bill)
        //{
        //    var existing = bills.FirstOrDefault(b => b.StudentId == bill.StudentId);
        //    if (existing == null) bills.Add(bill);
        //    else
        //    {
        //        existing.TotalMeals = bill.TotalMeals;
        //        existing.Amount = bill.Amount;
        //        existing.IsPaid = bill.IsPaid;
        //    }
        //}


        //public Bill[] GetAllBills() => bills.ToArray();


        //public Bill GetBillByStudent(int studentId) => bills.FirstOrDefault(b => b.StudentId == studentId);
        // List<Bill> data structure to store bills
        private List<Bill> bills = new List<Bill>();

        // Save or update a bill without using LINQ or default methods like FirstOrDefault
        public void SaveBill(Bill bill)
        {
            Bill existing = null;

            // Manual search through the list
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].StudentId == bill.StudentId)
                {
                    existing = bills[i];
                    break;
                }
            }

            if (existing == null)
            {
                // Add new bill
                bills.Add(bill);
            }
            else
            {
                // Update existing bill
                existing.TotalMeals = bill.TotalMeals;
                existing.Amount = bill.Amount;
                existing.IsPaid = bill.IsPaid;
            }
        }

        // Get all bills without using ToArray()
        public Bill[] GetAllBills()
        {
            Bill[] allBills = new Bill[bills.Count];
            for (int i = 0; i < bills.Count; i++)
            {
                allBills[i] = bills[i];
            }
            return allBills;
        }

        // Get a bill by StudentId manually
        public Bill GetBillByStudent(int studentId)
        {
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].StudentId == studentId)
                {
                    return bills[i];
                }
            }
            return null; // Not found
        }
    }
}
