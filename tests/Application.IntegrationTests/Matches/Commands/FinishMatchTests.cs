using Application.Common.Exceptions;
using Application.Matches.Commands.FinishMatch;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Matches.Commands
{
    using static Testing;
    using static Helper;

    public class FinishMatchTests : TestBase
    {
        [Test]
        public void ShouldRequireValidId()
        {
            var query = new FinishMatchCommand { Id = 1 };

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldFinishMatch()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);

            var command = new FinishMatchCommand { Id = matchId };

            await SendAsync(command);

            var match = await FindAsync<Match>(matchId);

            match.Should().NotBeNull();
            match.IsPlayed.Should().BeTrue();
        }
    }
}
