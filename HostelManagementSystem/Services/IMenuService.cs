using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IMenuService
    {
        void CreateMenu(Menu menu);
        Menu[] GetMenus();
        Menu GetMenu(string date);
        void EditMenu(Menu menu);
        void RemoveMenu(string date);
    }
}
