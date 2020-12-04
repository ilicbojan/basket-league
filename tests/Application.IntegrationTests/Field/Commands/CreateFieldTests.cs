using Application.Cities.Commands.CreateCity;
using Application.Common.Exceptions;
using Application.Countries.Commands.CreateCountry;
using Application.Field.Commands.CreateField;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Field.Commands
{
    using static Testing;

    public class CreateFieldTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateFieldCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            var cityId = await CreateCity();

            await SendAsync(new CreateFieldCommand
            {
                Name = "Teren 1",
                Address = "Adresa 1",
                CityId = cityId
            });

            var command = new CreateFieldCommand
            {
                Name = "Teren 1",
                Address = "Adresa 1",
                CityId = cityId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidCityId()
        {
            var command = new CreateFieldCommand
            {
                Name = "Teren 1",
                Address = "Adresa 1",
                CityId = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateField()
        {
            var cityId = await CreateCity();

            var command = new CreateFieldCommand
            {
                Name = "Teren 1",
                Address = "Adresa 1",
                CityId = cityId
            };

            var fieldId = await SendAsync(command);

            var field = await FindAsync<Domain.Entities.Field>(fieldId);

            field.Should().NotBeNull();
            field.Name.Should().Be(command.Name);
            field.Address.Should().Be(command.Address);
            field.CityId.Should().Be(command.CityId);
        }

        private async Task<int> CreateCity()
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

            return cityId;
        }
    }
}
