using Application.Cities.Commands.CreateCity;
using Application.Countries.Commands.CreateCountry;
using Application.Leagues.Queries.GetLeagues;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Leagues.Queries
{
    using static Testing;

    public class GetLeaguesTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllLeagues()
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

            // TODO: Create CreateFieldCommand in Application Project and use SendAsync to get fieldId

            await AddAsync(new League
            {
                Name = "Liga",
                CityId = cityId,
                Seasons = 
                {
                    new Season { Name = "Prolece", Year = 2020, Field = new Field { Name = "Teren 1", Address = "Adresa 1", CityId = cityId } },
                    new Season { Name = "Leto", Year = 2020, Field = new Field { Name = "Teren 2", Address = "Adresa 2", CityId = cityId } },
                    new Season { Name = "Jesen", Year = 2020, Field = new Field { Name = "Teren 3", Address = "Adresa 3", CityId = cityId } }
                }
            });

            var query = new GetLeaguesQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Leagues.Should().HaveCount(1);
            result.Leagues.First().Name.Should().NotBeNullOrEmpty();
            result.Leagues.First().City.Should().NotBeNull();
            result.Leagues.First().Seasons.Should().HaveCount(3);
        }
    }
}
