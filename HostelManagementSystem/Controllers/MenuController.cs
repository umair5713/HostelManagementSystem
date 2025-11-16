using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;


        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        // List all menus
        public ActionResult Index()
        {
            var menus = _menuService.GetMenus();
            return View(menus);
        }


        // Add Menu
        public ActionResult AddMenu()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuService.CreateMenu(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }


        // Edit Menu
        public ActionResult EditMenu(string date)
        {
            var menu = _menuService.GetMenu(date);
            if (menu == null) return NotFound();
            return View(menu);
        }


        [HttpPost]
        public ActionResult EditMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuService.EditMenu(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }


        // Delete Menu
        public ActionResult DeleteMenu(string date)
        {
            _menuService.RemoveMenu(date);
            return RedirectToAction("Index");
        }
    }
}
