using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IUserService
    {
        User? ValidateUser(string email, string password);
        void RegisterUser(User user);
    }
}
