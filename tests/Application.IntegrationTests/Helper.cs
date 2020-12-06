using Application.Cities.Commands.CreateCity;
using Application.Countries.Commands.CreateCountry;
using Application.Field.Commands.CreateField;
using Application.Leagues.Commands.CreateLeague;
using Application.Matches.Commands.CreateMatch;
using Application.Players.Commands.CreatePlayer;
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

        public async static Task<int> CreateTeam(string name)
        {
            var teamId = await SendAsync(new CreateTeamCommand
            {
                Name = name,

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

        public async static Task<int> CreateMatch(int teamId, int seasonId)
        {
            var awayTeamId = await CreateTeam(seasonId);
            var refereeId = await CreateReferee();
            var delegateId = await CreateDelegate();

            var matchId = await SendAsync(new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamId,
                AwayTeamId = awayTeamId,
                RefereeId = refereeId,
                DelegateId = delegateId,
                SeasonId = seasonId
            });

            return matchId[0];
        }

        public async static Task<List<int>> CreatePlayers(int teamId)
        {
            await CreateRoleAsync("player");

            var id1 = await SendAsync(new CreatePlayerCommand
            {
                Email = "test@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891235",
                PhoneNumber = "0651234568",
                TeamId = teamId
            });

            var id2 = await SendAsync(new CreatePlayerCommand
            {
                Email = "igrac2@test.com",
                Password = "test12",
                FirstName = "igrac2",
                LastName = "igrac2",
                JerseyNumber = 2,
                JMBG = "1234567891232",
                PhoneNumber = "0651234562",
                TeamId = teamId
            });

            var id3 = await SendAsync(new CreatePlayerCommand
            {
                Email = "igrac3@test.com",
                Password = "test12",
                FirstName = "igrac3",
                LastName = "igrac3",
                JerseyNumber = 3,
                JMBG = "1234567891233",
                PhoneNumber = "0651234563",
                TeamId = teamId
            });

            var id4 = await SendAsync(new CreatePlayerCommand
            {
                Email = "igrac4@test.com",
                Password = "test12",
                FirstName = "igrac4",
                LastName = "igrac4",
                JerseyNumber = 4,
                JMBG = "1234567891234",
                PhoneNumber = "0651234564",
                TeamId = teamId
            });

            return new List<int> { id1, id2, id3, id4 };
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
