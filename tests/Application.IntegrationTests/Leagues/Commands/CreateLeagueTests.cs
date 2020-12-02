using Application.Cities.Commands.CreateCity;
using Application.Common.Exceptions;
using Application.Countries.Commands.CreateCountry;
using Application.Leagues.Commands.CreateLeague;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Leagues.Commands
{
    using static Testing;

    class CreateLeagueTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateLeagueCommand();

            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            var countryId = await SendAsync(new CreateCountryCommand
            {
                Name = "Srbija"
            });

            var cityId = await SendAsync(new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = countryId
            });

            await SendAsync(new CreateLeagueCommand
            {
                Name = "Prva liga",
                CityId = cityId
            });

            var command = new CreateLeagueCommand
            {
                Name = "Prva liga",
                CityId = cityId
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidCityId()
        {
            var command = new CreateLeagueCommand
            {
                Name = "Prva liga",
                CityId = 1
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateLeague()
        {
            var countryId = await SendAsync(new CreateCountryCommand
            {
                Name = "Srbija"
            });

            var cityId = await SendAsync(new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = countryId
            });

            var command = new CreateLeagueCommand
            {
                Name = "Prva liga",
                CityId = cityId
            };

            var leagueId = await SendAsync(command);

            var league = await FindAsync<League>(leagueId);

            league.Should().NotBeNull();
            league.Name.Should().Be(command.Name);
            league.CityId.Should().Be(command.CityId);
        }
    }
}
