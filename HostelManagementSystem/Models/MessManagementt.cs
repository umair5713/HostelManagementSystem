namespace HostelManagementSystem.Models
{
    public class MessManagementt
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int MealsTaken { get; set; }
        public decimal MealRate { get; set; }

        // Calculated property
        public decimal TotalBill => MealsTaken * MealRate;
    }
}
