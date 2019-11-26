using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Store.Data;
using Store.Helpers;
using Store.Services;

namespace Store
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            
            using(var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await dbContext.Database.MigrateAsync();


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


                //var signInManager = serviceScope.ServiceProvider.GetRequiredService<SignInManager<IdentityUser>>();

                //var options = serviceScope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();
                //var usersSevice = new UsersService(userManager, signInManager, options);

                //var result = await signInManager.PasswordSignInAsync(user.UserName, password, false, false);

                ////var loginResult = await usersSevice.LoginAsync(user.UserName, password);

                //Console.WriteLine("");
                ////Console.WriteLine(loginResult.Token);
            }
            
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
