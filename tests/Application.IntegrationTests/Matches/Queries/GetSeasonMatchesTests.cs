using Application.Common.Exceptions;
using Application.Matches.Queries.GetMatchesBySeason;
using Application.Seasons.Commands.CreateSeason;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Matches.Queries
{
    using static Testing;
    using static Helper;

    public class GetMatchesBySeasonTests : TestBase
    {
        [Test]
        public void ShouldRequireValidId()
        {
            var query = new GetMatchesBySeasonQuery(1, true);

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

            var query = new GetMatchesBySeasonQuery(seasonId, true);

            var result = await SendAsync(query);

            result.Should().BeOfType<List<MatchDto>>();
            result.Should().NotBeNull();
        }
    }
}
