using HostelManagementSystem.Repositories;
using HostelManagementSystem.Models;
namespace HostelManagementSystem.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;

        public MenuService(IMenuRepository repo)
        {
            _repo = repo;
        }

        public void CreateMenu(Menu menu)
        {
            _repo.AddMenu(menu);
        }

        public List<Menu> GetMenus()
        {
            return _repo.GetAllMenus();
        }

        public Menu? GetMenu(DateTime date)
        {
            return _repo.GetMenuByDate(date.Date);
        }

        public Menu? GetMenuById(int menuId)
        {
            return _repo.GetById(menuId);
        }

        public void EditMenu(Menu menu)
        {
            _repo.UpdateMenu(menu);
        }

        public void RemoveMenu(int menuId)
        {
            _repo.DeleteMenu(menuId);
        }
    }
}
