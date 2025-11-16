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

        public Menu[] GetMenus()
        {
            return _repo.GetAllMenus();
        }

        public Menu GetMenu(string date)
        {
            return _repo.GetMenuByDate(date);
        }

        public void EditMenu(Menu menu)
        {
            _repo.UpdateMenu(menu);
        }

        public void RemoveMenu(string date)
        {
            _repo.DeleteMenu(date);
        }
    }
}
