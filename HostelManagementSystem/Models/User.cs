//using System.ComponentModel.DataAnnotations.Schema;

//namespace HostelManagementSystem.Models
//{
//   [Table("tbl_user")]
//   public class User
//   {
//        [Column("id_user")]
//        public int Id { get; set; }
//        [Column("email")]
//        public string Email { get; set; } = string.Empty;
//        [Column("user_password")]
//        public string Password { get; set; } = string.Empty;
//        [Column("fk_role_name")]

//        public string FkRoleName { get; set; } = string.Empty;

//        public int? FkStudentId { get; set; }
//    }

//}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagementSystem.Models
{
    [Table("tbl_user")]
    public class User
    {
        [Key]
        [Column("id_user")]
        public int id_user { get; set; }

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("user_password")]
        public string Password { get; set; } = string.Empty;

        [Column("fk_role_name")]
        public string FkRoleName { get; set; } = string.Empty;

        [Column("fk_student_id")]
        public int? FkStudentId { get; set; }
    }
}

