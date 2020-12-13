using Application.Common.Exceptions;
using Application.MatchPlayers.Commands.AddPoints;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.MatchPlayers.Commands
{
    using static Testing;
    using static Helper;

    public class AddPointsTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new AddPointsCommand();

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

            var command = new AddPointsCommand
            {
                MatchId = 1,
                ScorePlayerId = playersIds[0],
                Points = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireValidScorePlayerId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            await CreateLineup(matchId, teamId, playersIds);

            var command = new AddPointsCommand
            {
                MatchId = matchId,
                ScorePlayerId = 1,
                Points = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireValidAssistPlayerId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            await CreateLineup(matchId, teamId, playersIds);

            var command = new AddPointsCommand
            {
                MatchId = matchId,
                ScorePlayerId = playersIds[0],
                AssistPlayerId = 0,
                Points = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequirePointsToBe1or2()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            await CreateLineup(matchId, teamId, playersIds);

            var command = new AddPointsCommand
            {
                MatchId = matchId,
                ScorePlayerId = playersIds[0],
                AssistPlayerId = playersIds[1],
                Points = 3
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldAddPoints()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam("Tim 2", seasonId);
            var matchId = await CreateMatch(teamId, seasonId);
            var playersIds = await CreatePlayers(teamId);
            await CreateLineup(matchId, teamId, playersIds);

            var command = new AddPointsCommand
            {
                MatchId = matchId,
                ScorePlayerId = playersIds[0],
                AssistPlayerId = playersIds[1],
                Points = 2
            };

            await SendAsync(command);

            var match = await FindAsync<Match>(matchId);

            match.Should().NotBeNull();
            match.HomePoints.Should().Be(2);

            var scorePlayer = await FindAsync<MatchPlayer>(matchId, playersIds[0]);
            scorePlayer.Should().NotBeNull();
            scorePlayer.Points.Should().Be(2);

            var assistPlayer = await FindAsync<MatchPlayer>(matchId, playersIds[1]);
            assistPlayer.Should().NotBeNull();
            assistPlayer.Assists.Should().Be(1);
        }
    }
}
