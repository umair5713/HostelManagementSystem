using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public User? ValidateUser(string email, string password)
        {
            var user = _repo.GetUser(email, password);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;


        }
        public void RegisterUser(User user)
        {
            var newuser = new User
            {
                Email = user.Email,
                Password = user.Password,
                FkRoleName = "Student "
            };
            _repo.RegisterUser(newuser);
        }
    }
}
