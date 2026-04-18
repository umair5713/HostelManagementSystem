using System.ComponentModel.DataAnnotations;

namespace HostelManagementSystem.Models
{
    public class Student
    {
        [Key] 
        public int StudentID { get; set; }
        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; }
        [MaxLength(20)]
        public string RoomNo { get; set; }
        public bool FeeStatus { get; set; } = false;
       
    }
}
