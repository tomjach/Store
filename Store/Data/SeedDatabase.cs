using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Data
{
    public static class SeedDatabase
    {
        public static async Task SeedAsync(IServiceScope serviceScope)
        {            
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = new IdentityUser { UserName = "admin@test.pl" };
            var password = "Start123!";
            if (await userManager.FindByNameAsync(user.UserName) == null)
            {
                var result = await userManager.CreateAsync(user, password);
            }
            
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var admin = new IdentityRole { Name = "Admin" };
            if (!await roleManager.RoleExistsAsync(admin.Name))
            {
                var result = await roleManager.CreateAsync(admin);
            }
            
            await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), admin.Name);
        }
    }
}
