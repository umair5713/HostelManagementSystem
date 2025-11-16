namespace HostelManagementSystem.Models
{
    public class Bill
    {
        public int StudentId { get; set; }
        public int TotalMeals { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}
