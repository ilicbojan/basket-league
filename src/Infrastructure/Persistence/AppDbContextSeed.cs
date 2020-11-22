using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = RoleEnum.Admin},
                    new IdentityRole { Name = RoleEnum.User},
                    new IdentityRole { Name = RoleEnum.Player},
                    new IdentityRole { Name = RoleEnum.Referee},
                    new IdentityRole { Name = RoleEnum.Delegate}
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser { Email = "admin@test.com", UserName = "admin", FirstName = "Admin", LastName = "Admin", PhoneNumber = "0651234567" },
                    new AppUser { Email = "user@test.com", UserName = "user", FirstName = "User", LastName = "User", PhoneNumber = "0651234568" }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "test12");
                }
            }

            if (!context.UserRoles.Any())
            {
                var users = await userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    await userManager.AddToRoleAsync(user, RoleEnum.User);
                }

                var admin = await userManager.FindByNameAsync("admin");

                await userManager.AddToRoleAsync(admin, RoleEnum.Admin);
            }

            if (!context.Leagues.Any())
            {
                context.Leagues.Add(new League
                {
                    Name = "Prva Liga",
                    City = new City
                    {
                        Name = "Beograd",
                        Country = new Country { Name = "Srbija" }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
