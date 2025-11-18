using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        //private List<Menu> menus = new List<Menu>();

        //public void AddMenu(Menu menu)
        //{
        //    // Check if menu for the same date exists
        //    var existing = menus.FirstOrDefault(m => m.Date == menu.Date);
        //    if (existing == null)
        //    {
        //        menus.Add(menu);
        //    }
        //    else
        //    {
        //        // Update existing
        //        existing.Breakfast = menu.Breakfast;
        //        existing.Lunch = menu.Lunch;
        //        existing.Dinner = menu.Dinner;
        //    }
        //}

        //public Menu[] GetAllMenus()
        //{
        //    return menus.ToArray();
        //}

        //public Menu GetMenuByDate(string date)
        //{
        //    return menus.FirstOrDefault(m => m.Date == date);
        //}

        //public void UpdateMenu(Menu menu)
        //{
        //    var existing = menus.FirstOrDefault(m => m.Date == menu.Date);
        //    if (existing != null)
        //    {
        //        existing.Breakfast = menu.Breakfast;
        //        existing.Lunch = menu.Lunch;
        //        existing.Dinner = menu.Dinner;
        //    }
        //}

        //public void DeleteMenu(string date)
        //{
        //    var menu = menus.FirstOrDefault(m => m.Date == date);
        //    if (menu != null)
        //    {
        //        menus.Remove(menu);
        //    }
        //}



        private List<Menu> menus = new List<Menu>();

        // Add menu or update if same date exists (without LINQ)
        public void AddMenu(Menu menu)
        {
            Menu existing = null;

            // Manually search for existing menu by date
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].Date == menu.Date)
                {
                    existing = menus[i];
                    break;
                }
            }

            if (existing == null)
            {
                // Add new menu
                menus.Add(menu);
            }
            else
            {
                // Update existing menu
                existing.Breakfast = menu.Breakfast;
                existing.Lunch = menu.Lunch;
                existing.Dinner = menu.Dinner;
            }
        }

        // Get all menus using arrays
        public Menu[] GetAllMenus()
        {
            Menu[] allMenus = new Menu[menus.Count];
            for (int i = 0; i < menus.Count; i++)
            {
                allMenus[i] = menus[i];
            }
            return allMenus;
        }

        // Get menu by date arrays
        public Menu GetMenuByDate(string date)
        {
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].Date == date)
                {
                    return menus[i];
                }
            }
            return null; // Not found
        }

        // Update menu by date manually
        public void UpdateMenu(Menu menu)
        {
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].Date == menu.Date)
                {
                    menus[i].Breakfast = menu.Breakfast;
                    menus[i].Lunch = menu.Lunch;
                    menus[i].Dinner = menu.Dinner;
                    break;
                }
            }
        }

        // Delete menu by date manually
        public void DeleteMenu(string date)
        {
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].Date == date)
                {
                    // Remove by shifting elements manually
                    for (int j = i; j < menus.Count - 1; j++)
                    {
                        menus[j] = menus[j + 1];
                    }
                    menus.RemoveAt(menus.Count - 1); // Remove last duplicate
                    break;
                }
            }
        }
    }
}
