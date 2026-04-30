using Microsoft.EntityFrameworkCore;
using HostelManagementSystem.Models;
namespace HostelManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMeal> StudentMeals { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Fee> Fees { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Complaint>().ToTable("tbl_complaints");
        //    modelBuilder.Entity<AttendanceRecord>().ToTable("tbl_attendance");
        //    modelBuilder.Entity<Student>().ToTable("tbl_students");
        //    modelBuilder.Entity<StudentMeal>().ToTable("tbl_student_meals");
        //    modelBuilder.Entity<Menu>().ToTable("tbl_menu");
        //    modelBuilder.Entity<Fee>().ToTable("tbl_fees");
        //    modelBuilder.Entity<User>().ToTable("tbl_user");
        //    modelBuilder.Entity<Role>().ToTable("tbl_role");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>().ToTable("tbl_complaints");
            modelBuilder.Entity<AttendanceRecord>().ToTable("tbl_attendance");
            modelBuilder.Entity<Student>().ToTable("tbl_students");
            modelBuilder.Entity<StudentMeal>().ToTable("tbl_student_meals");
            modelBuilder.Entity<Menu>().ToTable("tbl_menu");
            modelBuilder.Entity<Fee>().ToTable("tbl_fees");
            modelBuilder.Entity<Role>().ToTable("tbl_role");

            // ADD THIS — map User properties to exact column names
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tbl_user");
                entity.HasKey(e => e.id_user);
                entity.Property(e => e.id_user).HasColumnName("id_user");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("user_password");
                entity.Property(e => e.FkRoleName).HasColumnName("fk_role_name");
                entity.Property(e => e.FkStudentId).HasColumnName("fk_student_id");
            });
        }


    }
}
