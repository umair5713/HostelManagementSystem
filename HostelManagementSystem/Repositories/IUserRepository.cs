using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IUserRepository
    {
        User? GetUser(string email, string password);
        void RegisterUser(User user);
    }
}
