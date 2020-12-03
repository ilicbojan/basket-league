using Application.Common.Exceptions;
using Application.Countries.Commands.CreateCountry;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Countries.Commands
{
    using static Testing;

    public class CreateCountryTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCountryCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            await SendAsync(new CreateCountryCommand
            {
                Name = "Srbija"
            });

            var command = new CreateCountryCommand
            {
                Name = "Srbija"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateCountry()
        {
            var command = new CreateCountryCommand
            {
                Name = "Srbija"
            };

            var countryId = await SendAsync(command);

            var country = await FindAsync<Country>(countryId);

            country.Should().NotBeNull();
            country.Name.Should().Be(command.Name);
        }
    }
}
