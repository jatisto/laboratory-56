using System.Threading.Tasks;
using Labarotory_56.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Labarotory_56
{
    internal class InitializerRole
    {
        public async Task SeedAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole roleAdmin = new IdentityRole("Admin");
                    await roleManager.CreateAsync(roleAdmin);
                }
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    IdentityRole roleUser = new IdentityRole("User");
                    await roleManager.CreateAsync(roleUser);
                }

                ApplicationUser admin = await userManager.FindByNameAsync("admin");
                if (admin == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@support.com",
                        Name = "admin"
                    };
                    var result = await userManager.CreateAsync(user, "123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }

                }
            }
        }
    }
}