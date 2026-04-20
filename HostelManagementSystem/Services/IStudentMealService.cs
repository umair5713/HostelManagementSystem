using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IStudentMealService
    {
        void AcceptMeal(int studentId, DateTime date, string mealType);
        void AcceptAllMeals(int studentId, DateTime date);
        List<StudentMeal> GetMealsByStudent(int studentId);
        bool HasAccepted(int studentId, DateTime date, string mealType);
    }
}
