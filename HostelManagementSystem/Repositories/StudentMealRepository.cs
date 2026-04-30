using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    public class StudentMealRepository : IStudentMealRepository
    {
        private readonly AppDbContext _context;

        public StudentMealRepository(AppDbContext context)
        {
            _context = context;
        }

        //        // ACCEPT MEAL
        //        public void AcceptMeal(StudentMeal meal)
        //        {
        //            if (!HasAccepted(meal.StudentID, meal.Date, meal.MealType))
        //            {
        //                // DEBUG — remove after fixing
        //                Console.WriteLine($"Inserting StudentID: {meal.StudentID}, Date: {meal.Date}, MealType: {meal.MealType}");

        //                _context.Database.ExecuteSqlRaw(
        //                    @"INSERT INTO tbl_student_meals (StudentID, Date, MealType, Time)
        //              VALUES ({0}, {1}, {2}, {3})",
        //                    meal.StudentID,
        //                    meal.Date,
        //                    meal.MealType,
        //                    DateTime.Now
        //                );
        //            }
        //        }

        //        // GET MEALS BY STUDENT
        //        public List<StudentMeal> GetMealsByStudent(int studentId)
        //        {
        //            return _context.StudentMeals
        //                      .FromSqlRaw("SELECT MealID, StudentID, Date, MealType, Time FROM tbl_student_meals WHERE StudentID = {0} ORDER BY Date DESC", studentId)
        //                      .ToList();
        //        }

        //        // HAS ACCEPTED
        //        public bool HasAccepted(int studentId, DateTime date, string mealType)
        //        {
        //            var result = _context.StudentMeals
        //                            .FromSqlRaw(@"SELECT MealID, StudentID, Date, MealType, Time 
        //                                      FROM tbl_student_meals 
        //                                      WHERE StudentID = {0} AND CAST(Date AS DATE) = CAST({1} AS DATE) AND MealType = {2}",
        //                                          studentId, date, mealType)
        //                            .FirstOrDefault();
        //            return result != null;
        //        }

        public void AcceptMeal(StudentMeal meal)
        {
            // Check if record already exists for this meal
            var existing = _context.StudentMeals
                .FromSqlRaw(
                    @"SELECT MealID, StudentID, Date, MealType, Time, Status 
              FROM tbl_student_meals 
              WHERE StudentID = {0} 
              AND CAST(Date AS DATE) = CAST({1} AS DATE) 
              AND MealType = {2}",
                    meal.StudentID, meal.Date, meal.MealType)
                .FirstOrDefault();

            if (existing != null)
            {
                // Update existing record status
                _context.Database.ExecuteSqlRaw(
                    @"UPDATE tbl_student_meals 
              SET Status = {0} 
              WHERE StudentID = {1} 
              AND CAST(Date AS DATE) = CAST({2} AS DATE) 
              AND MealType = {3}",
                    meal.Status,
                    meal.StudentID,
                    meal.Date,
                    meal.MealType
                );
            }
            else
            {
                // Insert new record
                _context.Database.ExecuteSqlRaw(
                    @"INSERT INTO tbl_student_meals (StudentID, Date, MealType, Time, Status)
              VALUES ({0}, {1}, {2}, {3}, {4})",
                    meal.StudentID,
                    meal.Date,
                    meal.MealType,
                    DateTime.Now,
                    meal.Status
                );
            }
        }

        public List<StudentMeal> GetMealsByStudent(int studentId)
        {
            return _context.StudentMeals
                .FromSqlRaw(
                    @"SELECT MealID, StudentID, Date, MealType, Time, Status 
              FROM tbl_student_meals 
              WHERE StudentID = {0} 
              ORDER BY Date DESC",
                    studentId)
                .ToList();
        }

        public bool HasAccepted(int studentId, DateTime date, string mealType)
        {
            var result = _context.StudentMeals
                .FromSqlRaw(
                    @"SELECT MealID, StudentID, Date, MealType, Time, Status 
              FROM tbl_student_meals 
              WHERE StudentID = {0} 
              AND CAST(Date AS DATE) = CAST({1} AS DATE) 
              AND MealType = {2}
              AND Status = 'Accepted'",
                    studentId, date, mealType)
                .FirstOrDefault();
            return result != null;
        }

        public bool HasDeclined(int studentId, DateTime date, string mealType)
        {
            var result = _context.StudentMeals
                .FromSqlRaw(
                    @"SELECT MealID, StudentID, Date, MealType, Time, Status 
              FROM tbl_student_meals 
              WHERE StudentID = {0} 
              AND CAST(Date AS DATE) = CAST({1} AS DATE) 
              AND MealType = {2}
              AND Status = 'Declined'",
                    studentId, date, mealType)
                .FirstOrDefault();
            return result != null;
        }
    }
}


