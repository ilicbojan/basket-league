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

            if (!context.Cities.Any())
            {
                context.Cities.Add(new City
                {
                    Name = "Beograd",
                    Country = new Country { Name = "Srbija" }
                });

                await context.SaveChangesAsync();
            }

            if (!context.Seasons.Any())
            {
                var city = await context.Cities.FirstOrDefaultAsync(x => x.Name == "Beograd");

                context.Seasons.Add(new Season
                {
                    Name = "1. Liga Prolece",
                    Year = 2021,
                    League = new League
                    {
                        Name = "1. Liga",
                        CityId = city.Id
                    },
                    Field = new Field
                    {
                        Name = "Teren 1",
                        Address = "Cara Dusana 31",
                        CityId = city.Id
                    }
                });

                await context.SaveChangesAsync();
            }

            if (!context.Teams.Any())
            {
                var season = await context.Seasons.FirstOrDefaultAsync(x => x.Name == "1. Liga Prolece");

                context.Teams.AddRange(
                    new List<Team>
                    {
                        new Team { Name = "Tim1", TeamSeasons = new List<TeamSeason> { new TeamSeason { SeasonId = season.Id } } },
                        new Team { Name = "Tim2", TeamSeasons = new List<TeamSeason> { new TeamSeason { SeasonId = season.Id } } }
                    });

                await context.SaveChangesAsync();
            }

            if (!context.Players.Any())
            {
                var team1 = await context.Teams.FirstOrDefaultAsync(x => x.Name == "Tim1");
                var team2 = await context.Teams.FirstOrDefaultAsync(x => x.Name == "Tim2");

                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Email = "igrac1@test.com", UserName = "igrac1", FirstName = "Igrac1", LastName = "Igrac1", PhoneNumber = "0651234551",
                        Player = new Player { JerseyNumber = 2, JMBG = "123456789120", TeamId = team1.Id }
                    },
                    new AppUser
                    {
                        Email = "igrac2@test.com", UserName = "igrac2", FirstName = "Igrac2", LastName = "Igrac2", PhoneNumber = "0651234552",
                        Player = new Player { JerseyNumber = 3, JMBG = "123456789121", TeamId = team1.Id }
                    },
                    new AppUser
                    {
                        Email = "igrac3@test.com", UserName = "igrac3", FirstName = "Igrac3", LastName = "Igrac3", PhoneNumber = "0651234553",
                        Player = new Player { JerseyNumber = 4, JMBG = "123456789122", TeamId = team1.Id }
                    },
                    new AppUser
                    {
                        Email = "igrac4@test.com", UserName = "igrac4", FirstName = "Igrac4", LastName = "Igrac4", PhoneNumber = "0651234554",
                        Player = new Player { JerseyNumber = 5, JMBG = "123456789123", TeamId = team2.Id }
                    },
                    new AppUser
                    {
                        Email = "igrac5@test.com", UserName = "igrac5", FirstName = "Igrac5", LastName = "Igrac5", PhoneNumber = "0651234555",
                        Player = new Player { JerseyNumber = 6, JMBG = "123456789124", TeamId = team2.Id }
                    },
                    new AppUser
                    {
                        Email = "igrac6@test.com", UserName = "igrac6", FirstName = "Igrac6", LastName = "Igrac6", PhoneNumber = "0651234556",
                        Player = new Player { JerseyNumber = 7, JMBG = "123456789125", TeamId = team2.Id }
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "test12");
                    await userManager.AddToRoleAsync(user, RoleEnum.Player);
                }

                await context.SaveChangesAsync();
            }

            if (!context.Matches.Any())
            {
                var team1 = await context.Teams.FirstOrDefaultAsync(x => x.Name == "Tim1");
                var team2 = await context.Teams.FirstOrDefaultAsync(x => x.Name == "Tim2");
                var season = await context.Seasons.FirstOrDefaultAsync(x => x.Name == "1. Liga Prolece");
                var referee = await userManager.FindByNameAsync("sudija");
                var deleg = await userManager.FindByNameAsync("delegat");

                var match = new Match
                {
                    Date = DateTime.Parse("10/5/2021"),
                    Time = TimeSpan.Parse("18:00:00"),
                    Round = 1,
                    HomeTeamId = team1.Id,
                    AwayTeamId = team2.Id,
                    HomePoints = 0,
                    AwayPoints = 0,
                    SeasonId = season.Id,
                    RefereeId = referee.Id,
                    DelegateId = deleg.Id,
                    MatchPlayers = new List<MatchPlayer>()
                };

                var playersTeam1 = team1.Players.ToList();
                foreach (var player in playersTeam1)
                {
                    match.MatchPlayers.Add(new MatchPlayer { PlayerId = player.Id });
                }

                var playersTeam2 = team2.Players.ToList();
                foreach (var player in playersTeam2)
                {
                    match.MatchPlayers.Add(new MatchPlayer { PlayerId = player.Id });
                }

                context.Matches.Add(match);

                await context.SaveChangesAsync();
            }
        }
    }
}
