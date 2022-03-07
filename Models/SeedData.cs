using SmartCityApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartCityApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SmartCityAppContext(serviceProvider.GetRequiredService<DbContextOptions<SmartCityAppContext>>()))
            {
                if (!context.Pages.Any())
                {

                    context.Pages.AddRange(
                        new Page
                        {
                            Title = "Home",
                            Slug = "home",
                            Content = "home page",
                            Sorting = 0
                        },
                        new Page
                        {
                            Title = "About Us",
                            Slug = "about-us",
                            Content = "about us page",
                            Sorting = 100
                        },
                        new Page
                        {
                            Title = "Services",
                            Slug = "services",
                            Content = "services page",
                            Sorting = 100
                        },
                        new Page
                        {
                            Title = "Contact",
                            Slug = "contact",
                            Content = "contact page",
                            Sorting = 100
                        }
                    );
                }
                var roleStore = new RoleStore<IdentityRole>(context);
                var myRole = roleStore.Roles.AsNoTracking().FirstOrDefault(r => r.Name == "admin");
                if (myRole == null)
                {
                    var role = new IdentityRole("admin");
                    roleStore.CreateAsync(role);
                }
                var userStore = new UserStore<AppUser>(context);
                var adminUser = userStore.Users.AsNoTracking().FirstOrDefault(u => u.Email == "admin@gmail.com");
                if(adminUser == null)
                {
                    var admin = new AppUser();
                    admin.Email = "admin@gmail.com";
                    admin.UserName = "admin@gmail.com";
                    var password = new PasswordHasher<AppUser>();
                    var hashed = password.HashPassword(admin, "Pass123!");
                    admin.PasswordHash = hashed;
                    admin.EmailConfirmed = true;
                    admin.SecurityStamp = Guid.NewGuid().ToString("D");
                    var res = userStore.CreateAsync(admin);
                    UserManager<AppUser> _userManager = serviceProvider.GetService<UserManager<AppUser>>();
                    _userManager.AddToRoleAsync(admin, "admin");


                }

                context.SaveChanges();
            }
        }
    }
}
