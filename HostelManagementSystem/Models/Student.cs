using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(12)]
        [RegularExpression(@"^03[0-9]{9}$",
            ErrorMessage = "Phone must start with 03 followed by 9 digits e.g. 03001234567")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNIC is required")]
        [MaxLength(15)]
        [RegularExpression(@"^\d{5}-\d{7}-\d{1}$",
            ErrorMessage = "CNIC must follow pattern 42101-1234567-1")]
        public string CNIC { get; set; } = string.Empty;

        [Required(ErrorMessage = "Semester is required")]
        [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8")]
        public int Semester { get; set; } = 1;

        [MaxLength(20)]
        public string RoomNo { get; set; } = string.Empty;

        public bool FeeStatus { get; set; } = false;
    }
}
