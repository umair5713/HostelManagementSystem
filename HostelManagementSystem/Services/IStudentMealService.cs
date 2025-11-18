using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IStudentMealService
    {
        void AcceptMeal(int studentId, string date, string mealType);
        List<StudentMeal> GetMealsByStudent(int studentId);
        bool HasAccepted(int studentId, string date, string mealType);
    }
}
