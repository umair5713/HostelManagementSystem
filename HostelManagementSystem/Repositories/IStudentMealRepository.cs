using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IStudentMealRepository
    {
        void AcceptMeal(StudentMeal meal);
        List<StudentMeal> GetMealsByStudent(int studentId);
        bool HasAccepted(int studentId, DateTime date, string mealType);

        bool HasDeclined(int studentId, DateTime date, string mealType);
    }
}
