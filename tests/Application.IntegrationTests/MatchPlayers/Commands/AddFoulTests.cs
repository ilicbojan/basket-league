using Application.Common.Exceptions;
using Application.MatchPlayers.Commands.AddFoul;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.MatchPlayers.Commands
{
    using static Testing;
    using static Helper;

    public class AddFoulTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new AddFoulCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidMatchId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            await CreateLineup(matchId, teamId, playersIds);

            var command = new AddFoulCommand
            {
                MatchId = 1,
                PlayerId = playersIds[0],
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireValidPlayerId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            await CreateLineup(matchId, teamId, playersIds);

            var command = new AddFoulCommand
            {
                MatchId = matchId,
                PlayerId = 1,
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldAddFoul()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            await CreateLineup(matchId, teamId, playersIds);

            var command = new AddFoulCommand
            {
                MatchId = matchId,
                PlayerId = playersIds[0],
            };

            await SendAsync(command);

            var player = await FindAsync<MatchPlayer>(matchId, playersIds[0]);
            player.Should().NotBeNull();
            player.Fouls.Should().Be(1);
        }
    }
}
