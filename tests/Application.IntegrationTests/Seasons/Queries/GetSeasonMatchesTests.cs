using Application.Common.Exceptions;
using Application.Seasons.Commands.CreateSeason;
using Application.Seasons.Queries.GetSeasonMatches;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Seasons.Queries
{
    using static Testing;
    using static Helper;

    public class GetSeasonMatchesTests : TestBase
    {
        [Test]
        public void ShouldRequireValidId()
        {
            var query = new GetSeasonMatchesQuery(1, true);

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldReturnSeasonMatches()
        {
            var cityId = await CreateCity();
            var leageId = await CreateLeague(cityId);
            var fieldId = await CreateField(cityId);

            var seasonId = await SendAsync(new CreateSeasonCommand
            {
                Name = "Sezona 1",
                Year = 2020,
                FieldId = fieldId,
                LeagueId = leageId
            });

            var query = new GetSeasonMatchesQuery(seasonId, true);

            var result = await SendAsync(query);

            result.Should().BeOfType<List<MatchDto>>();
            result.Should().NotBeNull();
        }
    }
}
