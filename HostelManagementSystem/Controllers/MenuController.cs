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

        
        public IActionResult Index()
        {
            var menus = _menuService.GetMenus();
            return View(menus);
        }

        
        public IActionResult AddMenu()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuService.CreateMenu(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        
        public IActionResult EditMenu(int id)
        {
            var menu = _menuService.GetMenuById(id);
            if (menu == null)
                return NotFound();

            return View(menu);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuService.EditMenu(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMenu(int id)
        {
            _menuService.RemoveMenu(id);
            return RedirectToAction("Index");
        }
    }
}
