using Application.Cities.Commands.CreateCity;
using Application.Common.Exceptions;
using Application.Countries.Commands.CreateCountry;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Cities.Commands
{
    using static Testing;

    public class CreateCityTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCityCommand();

            FluentActions.Invoking(() => 
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            var countryId = await SendAsync(new CreateCountryCommand
            {
                Name = "Srbija"
            });

            await SendAsync(new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = countryId
            });

            var command = new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = countryId
            };

            FluentActions.Invoking(() => 
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidCountryId()
        {
            var command = new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = 1
            };

            FluentActions.Invoking(() => 
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateCity()
        {
            var countryId = await SendAsync(new CreateCountryCommand
            {
                Name = "Srbija"
            });

            var command = new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = countryId
            };

            var cityId = await SendAsync(command);

            var city = await FindAsync<City>(cityId);

            city.Should().NotBeNull();
            city.Name.Should().Be(command.Name);
            city.CountryId.Should().Be(command.CountryId);
        }
    }
}
