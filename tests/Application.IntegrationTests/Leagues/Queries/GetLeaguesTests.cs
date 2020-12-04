using Application.Countries.Commands.CreateCountry;
using Application.Field.Commands.CreateField;
using Application.Leagues.Queries.GetLeagues;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Leagues.Queries
{
    using static Testing;
    using static Helper;

    public class GetLeaguesTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllLeagues()
        {
            var cityId = await CreateCity();
            var fieldId = await CreateField(cityId);

            await AddAsync(new League
            {
                Name = "Liga",
                CityId = cityId,
                Seasons = 
                {
                    new Season { Name = "Prolece", Year = 2020, FieldId = fieldId },
                    new Season { Name = "Leto", Year = 2020, FieldId = fieldId },
                    new Season { Name = "Jesen", Year = 2020, FieldId = fieldId }
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
