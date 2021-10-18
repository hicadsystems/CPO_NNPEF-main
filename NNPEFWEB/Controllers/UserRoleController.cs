using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNPEFWEB.Enum;
using NNPEFWEB.Models;

namespace NNPEFWEB.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
      //  [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
           var userRoles = new List<UserRolesViewModel>();
            foreach(ApplicationUser user in users)
            {
                var vm = new UserRolesViewModel();
                vm.UserId = user.Id;
                vm.FirstName = user.FirstName;
                vm.LastName = user.LastName;
                vm.Email = user.Email;
                vm.Rank = user.Rank;
                vm.Command = user.Command;
                vm.Ship = user.ship;
                vm.Appointment = user.Appointment;
                vm.Roles = await GetUserRoles(user);
                userRoles.Add(vm);
            }
            return View(userRoles);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User With {userId} Not Fund";
                return View("NotFund");
            }
            ViewBag.userName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach(var Role in _roleManager.Roles)
            {
                var userRoles = new ManageUserRolesViewModel
                {
                    RoleId = Role.Id,
                    RoleName = Role.Name
                };
                if(await _userManager.IsInRoleAsync(user, Role.Name))
                {
                    userRoles.Selected = true;
                }
                else
                {
                    userRoles.Selected = false;
                }
                model.Add(userRoles);
            }
            return View(model);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot Remove Existing User Role");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot Add Selected Roles to User");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
