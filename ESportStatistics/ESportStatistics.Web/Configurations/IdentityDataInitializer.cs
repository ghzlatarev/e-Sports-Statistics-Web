using ESportStatistics.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Configurations
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("User"))
            {
                IdentityRole newRole = new IdentityRole() { Name = "User" };
                await roleManager.CreateAsync(newRole);
            }

            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                IdentityRole newRole = new IdentityRole() { Name = "Administrator" };
                await roleManager.CreateAsync(newRole);
            }
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!await userManager.Users.AnyAsync(u => u.UserName == "Administrator"))
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = "Administrator",
                    Email = "admin@gmail.com",
                    CreatedOn = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                };

                if ((await userManager.CreateAsync(newUser, "zaq1@WSX")).Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Administrator");
                }
            }
        }
    }
}
