using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        List<Student> GetStudents();
        void SortByID();
        StudentNode MergeSort(StudentNode h);
        StudentNode Merge(StudentNode left, StudentNode right);
        StudentNode GetMiddle(StudentNode h);
    }
}
