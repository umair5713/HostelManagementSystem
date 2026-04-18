using System.ComponentModel.DataAnnotations;

namespace HostelManagementSystem.Models
{
    public class AttendanceRecord
    {
        [Key]
        public int AttendanceId { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; }=string.Empty;
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
