using Microsoft.AspNetCore.Identity;
using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Secertariat.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.CDR.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.HOD.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.DO.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Operator.ToString()));
        }
        public static async Task SuperAdminAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            var defaultuser = new ApplicationUser
            {
                UserName = "SupperAdmin",
                FirstName = "Kenneth",
                LastName = "Ilekhaze",
                Email = "kboyyo.kenneth@gmail.com",
                EmailConfirmed = true,
               // PhoneNumber = "07066439348",
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultuser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultuser, "Password07066$&");
                    await userManager.AddToRoleAsync(defaultuser, Enum.Roles.Secertariat.ToString());
                    await userManager.AddToRoleAsync(defaultuser, Enum.Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultuser, Enum.Roles.CDR.ToString());
                    await userManager.AddToRoleAsync(defaultuser, Enum.Roles.HOD.ToString());
                    await userManager.AddToRoleAsync(defaultuser, Enum.Roles.DO.ToString());
                    await userManager.AddToRoleAsync(defaultuser, Enum.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultuser, Enum.Roles.Operator.ToString());
                }
            }
        }

    }
}
