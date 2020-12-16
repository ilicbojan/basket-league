using Application.Common.Exceptions;
using Application.Matches.Queries.GetMatch;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Matches.Queries
{
    using static Testing;
    using static Helper;

    public class GetMatchTests : TestBase
    {
        [Test]
        public void ShouldRequireValidId()
        {
            var query = new GetMatchQuery { Id = 1 };

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldReturnMatch()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);

            var query = new GetMatchQuery { Id = matchId };

            var result = await SendAsync(query);

            result.Should().NotBeNull();
        }
    }
}
