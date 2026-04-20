using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IMenuRepository
    {
        void AddMenu(Menu menu);
        List<Menu> GetAllMenus();
        Menu? GetMenuByDate(DateTime date);
        Menu? GetById(int menuId);
        void UpdateMenu(Menu menu);
        void DeleteMenu(int menuId);
    }
}
