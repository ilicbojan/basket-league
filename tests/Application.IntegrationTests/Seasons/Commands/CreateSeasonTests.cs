using Application.Cities.Commands.CreateCity;
using Application.Common.Exceptions;
using Application.Countries.Commands.CreateCountry;
using Application.Field.Commands.CreateField;
using Application.Leagues.Commands.CreateLeague;
using Application.Seasons.Commands.CreateSeason;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Seasons.Commands
{
    using static Testing;
    using static Helper;

    public class CreateSeasonTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateSeasonCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            var cityId = await CreateCity();
            var leagueId = await CreateLeague(cityId);
            var fieldId = await CreateField(cityId);

            await SendAsync(new CreateSeasonCommand
            {
                Name = "Leto",
                Year = 2020,
                LeagueId = leagueId,
                FieldId = fieldId
            });

            var command = new CreateSeasonCommand
            {
                Name = "Leto",
                Year = 2020,
                LeagueId = leagueId,
                FieldId = fieldId
            };

            FluentActions.Invoking(() => 
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidLeagueId()
        {
            var cityId = await CreateCity();
            var fieldId = await CreateField(cityId);

            var command = new CreateSeasonCommand
            {
                Name = "Leto",
                Year = 2020,
                LeagueId = 1,
                FieldId = fieldId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidFieldId()
        {
            var cityId = await CreateCity();
            var leagueId = await CreateLeague(cityId);

            var command = new CreateSeasonCommand
            {
                Name = "Leto",
                Year = 2020,
                LeagueId = leagueId,
                FieldId = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateSeason()
        {
            var cityId = await CreateCity();
            var leagueId = await CreateLeague(cityId);
            var fieldId = await CreateField(cityId);

            var command = new CreateSeasonCommand
            {
                Name = "Leto",
                Year = 2020,
                LeagueId = leagueId,
                FieldId = fieldId
            };

            var seasonId = await SendAsync(command);

            var season = await FindAsync<Season>(seasonId);

            season.Should().NotBeNull();
            season.Name.Should().Be(command.Name);
            season.Year.Should().Be(command.Year);
            season.LeagueId.Should().Be(command.LeagueId);
            season.FieldId.Should().Be(command.FieldId);
        }
    }
}
