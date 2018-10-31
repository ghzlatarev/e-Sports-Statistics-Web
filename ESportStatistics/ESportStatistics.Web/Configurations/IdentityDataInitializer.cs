using ESportStatistics.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Configurations
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole newRole = new IdentityRole() { Name = "User" };
                roleManager.CreateAsync(newRole);
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole newRole = new IdentityRole() { Name = "Administrator" };
                roleManager.CreateAsync(newRole);
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.AnyAsync(u => u.UserName == "Administrator").Result)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = "Administrator",
                    Email = "admin@gmail.com",
                    CreatedOn = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                };

                if (userManager.CreateAsync(newUser, "zaq1@WSX").Result.Succeeded)
                {
                    userManager.AddToRoleAsync(newUser, "Administrator");
                }
            }
        }
    }
}
