using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Data
{
    public static class SeedDatabase
    {
        public static async Task SeedUserAsync(IServiceScope serviceScope)
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

        public static async Task SeedProductsAsync(IServiceScope serviceScope)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

            if (!await dbContext.Categories.AnyAsync() || !await dbContext.Products.AnyAsync())
            {
                var category = new Category
                {
                    Name = "Akcesoria komputerowe"
                };

                await dbContext.Categories.AddAsync(category);

                await dbContext.Products.AddAsync(new Product
                {
                    Category = category,
                    Name = "Mysz DELL",
                    Price = 40
                });

                await dbContext.Products.AddAsync(new Product
                {
                    Category = category,
                    Name = "Klawiatura DELL",
                    Price = 90
                });

                await dbContext.Products.AddAsync(new Product
                {
                    Category = category,
                    Name = "Kabel HDMI",
                    Price = 60
                });

                var category2 = new Category
                {
                    Name = "Monitory"
                };

                await dbContext.Categories.AddAsync(category2);

                await dbContext.Products.AddAsync(new Product
                {
                    Category = category2,
                    Name = "Monitor DELL 23 cale",
                    Price = 600
                });

                await dbContext.Products.AddAsync(new Product
                {
                    Category = category2,
                    Name = "Monitor LG 27 cale",
                    Price = 900
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
