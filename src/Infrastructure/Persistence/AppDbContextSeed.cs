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
                    new AppUser { Email = "sudija@test.com", UserName = "sudija", FirstName = "Sudija", LastName = "Sudija", PhoneNumber = "0651234568" },
                    new AppUser { Email = "delegat@test.com", UserName = "delegat", FirstName = "Delegat", LastName = "Delegat", PhoneNumber = "0651234566" }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "test12");
                }
            }

            if (!context.UserRoles.Any())
            {
                var admin = await userManager.FindByNameAsync("admin");
                var referee = await userManager.FindByNameAsync("sudija");
                var deleg = await userManager.FindByNameAsync("delegat");

                await userManager.AddToRoleAsync(admin, RoleEnum.Admin);
                await userManager.AddToRoleAsync(referee, RoleEnum.Referee);
                await userManager.AddToRoleAsync(deleg, RoleEnum.Delegate);
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
