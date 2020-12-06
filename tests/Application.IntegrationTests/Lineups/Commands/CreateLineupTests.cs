using Application.Common.Exceptions;
using Application.Lineups.Commands.CreateLineup;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Lineups.Commands
{
    using static Testing;
    using static Helper;

    public class CreateLineupTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateLineupCommand();

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

            var command = new CreateLineupCommand
            {
                MatchId = 1,
                TeamId = teamId,
                PlayersIds = playersIds
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidTeamId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);

            var command = new CreateLineupCommand
            {
                MatchId = matchId,
                TeamId = 1,
                PlayersIds = playersIds
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidPlayerId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            playersIds.Add(1);

            var command = new CreateLineupCommand
            {
                MatchId = matchId,
                TeamId = teamId,
                PlayersIds = playersIds
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequirePlayerThatIsNotInLineup()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);

            await SendAsync(new CreateLineupCommand
            {
                MatchId = matchId,
                TeamId = teamId,
                PlayersIds = playersIds
            });

            var command = new CreateLineupCommand
            {
                MatchId = matchId,
                TeamId = teamId,
                PlayersIds = playersIds
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateLineup()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);

            var command = new CreateLineupCommand
            {
                MatchId = matchId,
                TeamId = teamId,
                PlayersIds = playersIds
            };

            await SendAsync(command);

            foreach (var playerId in playersIds)
            {

                var matchPlayer = await FindAsync<MatchPlayer>(matchId, playerId);

                matchPlayer.Should().NotBeNull();
                matchPlayer.MatchId.Should().Be(matchId);
                matchPlayer.PlayerId.Should().Be(playerId);
            }
        }
    }
}
