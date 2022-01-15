using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Extention;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModels.Menus;
using System.Threading.Tasks;

namespace NNPEFWEB.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IUserService userService;
        private readonly IMenuService menuService;
        private readonly IMenuGroupService menuGroupService;
        public MenuController(IUserService userService, IMenuService menuService, IMenuGroupService menuGroupService) : base(userService)
        {
            this.menuGroupService = menuGroupService;
            this.menuService = menuService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var devices = new MenusViewModel
            {
                Menus = menuService.GetMenus(),
                MenuGroups = menuGroupService.GetMenuGroupss()
            };
            return View(devices);
        }


        // GET: Menu/Details/5
        [Route("menu/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            var menu = await menuService.GetMenuById(id);
            return Json(new
            {
                Menu = menu
            }.ToResponse(null, true)); 
        }

        // GET: Menu/Details/5
        [Route("menu/update")]
        public async Task<ActionResult> Update(MenuViewModel newMenu)
        {
            var menu = new Menu
            {
                Id = newMenu.Id,
                Name = newMenu.Name,
                Code = newMenu.Code,
                Description = newMenu.Description,
                Controller = newMenu.Controller,
                Action = newMenu.Action,
                IsActive = true
            };

            if (newMenu.MenuGroup.HasValue)
            {
                menu.MenuGroupId = newMenu.MenuGroup;
            }
            else {
                menu.MenuGroupId = null;
            }
            var updated = await menuService.UpdateMenu(menu);

            if (updated)
            {
                TempData["SuccessMessage"] = "Successfully updataed menu";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Unable to updataed menu ";
            return RedirectToAction(nameof(Index));
        }

        // GET: Menu/Create
        [Route("/menu/togglestatus/{id:int}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {

            var menu = await menuService.GetMenuById(id);

            menu.IsActive = !menu.IsActive;

            var result =  await menuService.UpdateMenu(menu);

            if (!result)
            {
                TempData["ErrorMessage"] = "Unable to change menu status";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = $"Status Change Successful";
            return RedirectToAction(nameof(Index));
        }

    }
}