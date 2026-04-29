using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        // ADD
        public void AddMenu(Menu menu)
        {
            _context.Database.ExecuteSqlRaw(
                @"INSERT INTO tbl_menu (Date, Breakfast, Lunch, Dinner)
              VALUES     ({0}, {1}, {2}, {3})",
                menu.Date,
                menu.Breakfast,
                menu.Lunch,
                menu.Dinner
            );
        }

        // GET ALL
        public List<Menu> GetAllMenus()
        {
            return _context.Menus
                      .FromSqlRaw("SELECT MenuID, Date, Breakfast, Lunch, Dinner FROM tbl_menu ORDER BY Date DESC")
                      .ToList();
        }

        // GET BY DATE
        public Menu? GetMenuByDate(DateTime date)
        {
            return _context.Menus
                      .FromSqlRaw("SELECT MenuID, Date, Breakfast, Lunch, Dinner FROM tbl_menu WHERE CAST(Date AS DATE) = CAST({0} AS DATE)", date)
                      .FirstOrDefault();
        }

        // GET BY ID
        public Menu? GetById(int menuId)
        {
            return _context.Menus
                      .FromSqlRaw("SELECT MenuID, Date, Breakfast, Lunch, Dinner FROM tbl_menu WHERE MenuID = {0}", menuId)
                      .FirstOrDefault();
        }

        // UPDATE
        public void UpdateMenu(Menu menu)
        {
            _context.Database.ExecuteSqlRaw(
                @"UPDATE tbl_menu 
              SET Date = {0}, Breakfast = {1}, Lunch = {2}, Dinner = {3}
              WHERE MenuID = {4}",
                menu.Date,
                menu.Breakfast,
                menu.Lunch,
                menu.Dinner,
                menu.MenuID
            );
        }

        // DELETE
        public void DeleteMenu(int menuId)
        {
            _context.Database.ExecuteSqlRaw(
                "DELETE FROM tbl_menu WHERE MenuID = {0}",
                menuId
            );
        }
    }
}
