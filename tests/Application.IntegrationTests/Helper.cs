using Application.Cities.Commands.CreateCity;
using Application.Countries.Commands.CreateCountry;
using Application.Field.Commands.CreateField;
using Application.Leagues.Commands.CreateLeague;
using Application.Seasons.Commands.CreateSeason;
using Application.Teams.Commands.CreateTeam;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    using static Testing;

    public class Helper
    {
        public async static Task<int> CreateCountry()
        {
            var countryId = await SendAsync(new CreateCountryCommand
            {
                Name = "Srbija"
            });

            return countryId;
        }

        public async static Task<int> CreateCity()
        {
            var countryId = await CreateCountry();

            var cityId = await SendAsync(new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = countryId
            });

            return cityId;
        }

        public async static Task<int> CreateField(int? cityId)
        {
            if (cityId == null)
            {
                cityId = await CreateCity();
            }

            var fieldId = await SendAsync(new CreateFieldCommand
            {
                Name = "Teren 1",
                Address = "Adresa terena",
                CityId = (int)cityId
            });

            return fieldId;
        }

        public async static Task<int> CreateLeague(int? cityId)
        {
            if (cityId == null)
            {
                cityId = await CreateCity();
            }

            var leagueId = await SendAsync(new CreateLeagueCommand
            {
                Name = "Prva liga",
                CityId = (int)cityId
            });

            return leagueId;
        }

        public async static Task<int> CreateSeason()
        {
            var cityId = await CreateCity();
            var fieldId = await CreateField(cityId);
            var leagueId = await CreateLeague(cityId);

            var seasonId = await SendAsync(new CreateSeasonCommand
            {
                Name = "Prva liga",
                Year = 2020,
                FieldId = fieldId,
                LeagueId = leagueId
            });

            return seasonId;
        }

        public async static Task<int> CreateTeam()
        {
            var teamId = await SendAsync(new CreateTeamCommand
            {
                Name = "Tim 1"
            });

            return teamId;
        }

        public async static Task<int> CreateTeam(int seasonId)
        {
            var teamId = await SendAsync(new CreateTeamCommand
            {
                Name = "Tim 1"
            });

            await AddAsync(new TeamSeason
            {
                TeamId = teamId,
                SeasonId = seasonId
            });

            return teamId;
        }

        public async static Task<int> CreateTeam(string name, int seasonId)
        {
            var teamId = await SendAsync(new CreateTeamCommand
            {
                Name = name,
                
            });

            await AddAsync(new TeamSeason
            {
                TeamId = teamId,
                SeasonId = seasonId
            });

            return teamId;
        }

        public async static Task<string> CreateReferee()
        {
            var roleName = await CreateRoleAsync("referee");

            var refereeId = await SendAsync(new CreateUserCommand
            {
                Email = "referee@test.com",
                FirstName = "Ref",
                LastName = "Referee",
                PhoneNumber = "0651234560",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = roleName
            });

            return refereeId;
        }

        public async static Task<string> CreateDelegate()
        {
            var roleName = await CreateRoleAsync("delegate");

            var delegateId = await SendAsync(new CreateUserCommand
            {
                Email = "delegate@test.com",
                FirstName = "Del",
                LastName = "Delegate",
                PhoneNumber = "0651234561",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = roleName
            });

            return delegateId;
        }
    }
}
