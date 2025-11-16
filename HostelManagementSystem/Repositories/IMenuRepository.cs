using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IMenuRepository
    {
        void AddMenu(Menu menu);
        Menu[] GetAllMenus();
        Menu GetMenuByDate(string date);
        void UpdateMenu(Menu menu);
        void DeleteMenu(string date);
    }
}
