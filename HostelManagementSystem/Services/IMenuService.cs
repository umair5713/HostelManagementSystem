using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IMenuService
    {
        void CreateMenu(Menu menu);
        List<Menu> GetMenus();
        Menu? GetMenu(DateTime date);
        Menu? GetMenuById(int menuId);
        void EditMenu(Menu menu);
        void RemoveMenu(int menuId);
    }
}
