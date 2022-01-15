
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;

namespace NNPEFWEB.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IUnitOfWorks unitOfWork;
        public RoleService(RoleManager<Role> roleManager, IUnitOfWorks unitOfWork)
        {
            this.roleManager = roleManager;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Role> GetRoles()
        {
            return roleManager.Roles;
        }

        public IEnumerable<Role> GetActiveRoles()
        {
            return roleManager.Roles; //.Where(x => x.IsActive);
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await roleManager.FindByNameAsync(name);
        }


        public async Task<bool> AddRole(Role role, int[] menus)
        {
            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded && menus != null)
            {
                AddMenuToRole(role.Id, menus);
                return await unitOfWork.Done();
            }

            return false;           
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<Role> GetRoleWithMenuById(int id)
        {
            return await roleManager.Roles.Include(x => x.RoleMenus).FirstOrDefaultAsync(x => x.Id == id);
        }

        private void AddMenuToRole(int roleId, int[] menus)
        {
            if(menus.Length > 0)
            {
                var roleMenus = new List<RoleMenu>();
                foreach (var menu in menus)
                {
                    roleMenus.Add(new RoleMenu
                    {
                        RoleId = roleId,
                        MenuId = menu,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                        IsActive = true
                    });
                }
                unitOfWork.RoleMenus.CreateRange(roleMenus);
            }          
        }

        public async Task<IdentityResult> UpdateRole(Role role)
        {
           return await roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> UpdateRoleInfo(Role role, int[] menus)
        {
            var roleMenus  = unitOfWork.RoleMenus.GetByExpression(x => x.RoleId == role.Id);
            unitOfWork.RoleMenus.RemoveRange(roleMenus);

            if (menus != null)
            {
                AddMenuToRole(role.Id, menus);             
            }
            await unitOfWork.Done();

            return await roleManager.UpdateAsync(role);
        }
    }
}
