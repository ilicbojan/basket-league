using Application.Seasons.Queries.GetSeasons;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Seasons.Queries
{
    using static Testing;
    using static Helper;

    public class GetSeasonsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllSeasons()
        {
            var cityId = await CreateCity();
            var leageId = await CreateLeague(cityId);
            var fieldId = await CreateField(cityId);

            await AddAsync(new Season
            {
                Name = "Sezona 1",
                Year = 2020,
                FieldId = fieldId,
                LeagueId = leageId
            });

            var query = new GetSeasonsQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().Name.Should().NotBeNullOrEmpty();
            result.First().Year.Should().BePositive();
            result.First().League.Should().NotBeNull();
        }
    }
}
