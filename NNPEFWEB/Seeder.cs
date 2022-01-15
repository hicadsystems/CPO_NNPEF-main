using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using System;
using System.Collections.Generic;
using System.IO;


namespace NNPEFWEB
{
    public class Seeder
    {
        private static IServiceProvider _provider;
        
        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager,   IConfiguration config, IUnitOfWorks unitOfWork)
        //public static void SeedData(this IApplicationBuilder builder, UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration config, IUnitOfWork unitOfWork)
        {

                //SeedRoles(roleManager);
                //SeedMenuGroup(unitOfWork);
                //SeedMenus(unitOfWork);
                //SeedUsers(userManager, config, unitOfWork);

        }

  


        //public static void SeedRoles(RoleManager<Role> roleManager)
        //{
        //     if (!roleManager.RoleExistsAsync("Administrator").Result)
        //    {
        //        var roles = new List<Role>
        //        {
        //            new Role {Name = "Administrator", IsActive=true, CreatedOn= DateTime.Now, UpdatedOn = DateTime.Now, Description = "Perform system administrative activities on the APP" },
        //            new Role {Name = "Field Officer", Description = "Performs Field Activities on the client portal e.g. verification and enrollment activities",IsActive=true, CreatedOn= DateTime.Now, UpdatedOn = DateTime.Now},
        //             new Role {Name = "Supervisor", Description = "Performs non-administrative, Supervisory roles on the APP ",IsActive=true, CreatedOn= DateTime.Now, UpdatedOn = DateTime.Now}


        //        };

        //        foreach (var role in roles)
        //        {
        //            var result = roleManager.CreateAsync(role).Result;
        //        }
        //    }
        //}

        //public static void SeedMenuGroup(IUnitOfWorks unitOfWork)
        //{
        //    var menuGroup = unitOfWork.MenuGroups.Find(1).Result;
        //    if (menuGroup == null)
        //    {
        //        unitOfWork.MenuGroups.Create(new MenuGroup { Name = "Admin", Description = "Administrative Menus", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
        //        unitOfWork.MenuGroups.Create(new MenuGroup { Name = "Reports", Description = "Report Menus", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
        //        unitOfWork.Done().Wait();               
        //    }
        //}

        //public static void SeedMenus(IUnitOfWorks unitOfWork)
        //{
        //    var menu = unitOfWork.Menus.Find(1).Result;
        //    if (menu == null)
        //    {
        //        var menus = new List<Menu>
        //        {
        //            new Menu{Name= "Access Control", Controller="Home", Action="AccessControl", Description="Create, read, update, delete activities on roles, menus and devices",
        //                CreatedOn =DateTime.Now, UpdatedOn=DateTime.Now, MenuGroupId = 1},
        //            new Menu{Name= "User Management", Controller="User", Action="Index", Description="Create, read, update, delete activities on user",
        //                CreatedOn =DateTime.Now, UpdatedOn=DateTime.Now, MenuGroupId = 1},
        //            new Menu{Name= "Summary Report", Controller="Report", Action="Index", Description="Show summary report or overview of all employees",
        //                CreatedOn =DateTime.Now, UpdatedOn=DateTime.Now, MenuGroupId = 2},
        //            new Menu{Name= "Status Report", Controller="Report", Action="Status", Description="Show report of biometric process",
        //                CreatedOn =DateTime.Now, UpdatedOn=DateTime.Now, MenuGroupId = 2},
        //            new Menu{Name= "Period Management", Controller="period", Action="Index", Description="Create, read, update, delete activities associated with biometric excercise periods",
        //                CreatedOn =DateTime.Now, UpdatedOn=DateTime.Now, MenuGroupId = 1},
        //        };

        //        unitOfWork.Menus.CreateRange(menus);
        //        unitOfWork.Done().Wait();

        //        var roleMenus = new List<RoleMenu>
        //        {
        //            new RoleMenu{MenuId = 1, RoleId = 1, CreatedOn = DateTime.Now, IsActive = true, UpdatedOn = DateTime.Now},
        //            new RoleMenu{MenuId = 2, RoleId = 1, CreatedOn = DateTime.Now, IsActive = true, UpdatedOn = DateTime.Now},
        //            new RoleMenu{MenuId = 3, RoleId = 1, CreatedOn = DateTime.Now, IsActive = true, UpdatedOn = DateTime.Now}
        //        };

        //        unitOfWork.RoleMenus.CreateRange(roleMenus);
        //        unitOfWork.Done().Wait();
        //    }
        //}

        //public static void SeedUsers(UserManager<User> userManager, IConfiguration config, IUnitOfWorks unitOfWork)
        //{
        //    if (userManager.FindByEmailAsync("hicad@hicad.com").Result == null)
        //    {
        //        var user = new User
        //        {
        //            UserName = "hicad@hicad.com",
        //            FirstName = "Hicad",
        //            LastName = "Hicad1",
        //            Email = "hicad@hicad.com",
        //            CreatedOn = DateTime.Now,
        //            UpdatedOn = DateTime.Now,
        //            IsActive = true                  
        //        };

        //        var result = userManager.CreateAsync(user, "password").Result;
        //        if (result.Succeeded)
        //        {
        //            unitOfWork.UserRoles.Create(new UserRole
        //            {
        //                IsActive = true,
        //                CreatedOn = DateTime.Now,
        //                UpdatedOn = DateTime.Now,
        //                UserId = user.Id,
        //                RoleId = 1,

        //            });
        //            unitOfWork.Done().Wait();
        //        }
                
        //    }
        //}


    }

}
