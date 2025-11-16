using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private List<Menu> menus = new List<Menu>();

        public void AddMenu(Menu menu)
        {
            // Check if menu for the same date exists
            var existing = menus.FirstOrDefault(m => m.Date == menu.Date);
            if (existing == null)
            {
                menus.Add(menu);
            }
            else
            {
                // Update existing
                existing.Breakfast = menu.Breakfast;
                existing.Lunch = menu.Lunch;
                existing.Dinner = menu.Dinner;
            }
        }

        public Menu[] GetAllMenus()
        {
            return menus.ToArray();
        }

        public Menu GetMenuByDate(string date)
        {
            return menus.FirstOrDefault(m => m.Date == date);
        }

        public void UpdateMenu(Menu menu)
        {
            var existing = menus.FirstOrDefault(m => m.Date == menu.Date);
            if (existing != null)
            {
                existing.Breakfast = menu.Breakfast;
                existing.Lunch = menu.Lunch;
                existing.Dinner = menu.Dinner;
            }
        }

        public void DeleteMenu(string date)
        {
            var menu = menus.FirstOrDefault(m => m.Date == date);
            if (menu != null)
            {
                menus.Remove(menu);
            }
        }
    }
}
