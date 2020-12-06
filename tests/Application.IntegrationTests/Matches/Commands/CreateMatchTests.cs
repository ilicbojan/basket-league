using Application.Common.Exceptions;
using Application.Matches.Commands.CreateMatch;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Matches.Commands
{
    using static Testing;
    using static Helper;

    public class CreateMatchTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateMatchCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireDifferentTeams()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam(seasonId);
            var refereeId = await CreateReferee();
            var delegateId = await CreateDelegate();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamId,
                AwayTeamId = teamId,
                RefereeId = refereeId,
                DelegateId = delegateId,
                SeasonId = seasonId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidHomeTeamId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam(seasonId);
            var refereeId = await CreateReferee();
            var delegateId = await CreateDelegate();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = 45,
                AwayTeamId = teamId,
                RefereeId = refereeId,
                DelegateId = delegateId,
                SeasonId = seasonId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidAwayTeamId()
        {
            var seasonId = await CreateSeason();
            var teamId = await CreateTeam(seasonId);
            var refereeId = await CreateReferee();
            var delegateId = await CreateDelegate();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamId,
                AwayTeamId = 45,
                RefereeId = refereeId,
                DelegateId = delegateId,
                SeasonId = seasonId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidRefereeId()
        {
            var seasonId = await CreateSeason();
            var teamHomeId = await CreateTeam("Tim 1", seasonId);
            var teamAwayId = await CreateTeam("Tim 2", seasonId);
            var delegateId = await CreateDelegate();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamHomeId,
                AwayTeamId = teamAwayId,
                RefereeId = "383738f9-d9ba-48d2-bb44-920e5b10ae51",
                DelegateId = delegateId,
                SeasonId = seasonId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidDelegateId()
        {
            var seasonId = await CreateSeason();
            var teamHomeId = await CreateTeam("Tim 1", seasonId);
            var teamAwayId = await CreateTeam("Tim 2", seasonId);
            var refereeId = await CreateReferee();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamHomeId,
                AwayTeamId = teamAwayId,
                RefereeId = refereeId,
                DelegateId = "383738f9-d9ba-48d2-bb44-920e5b10ae51",
                SeasonId = seasonId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidSeasonId()
        {
            var seasonId = await CreateSeason();
            var teamHomeId = await CreateTeam("Tim 1", seasonId);
            var teamAwayId = await CreateTeam("Tim 2", seasonId);
            var refereeId = await CreateReferee();
            var delegateId = await CreateDelegate();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamHomeId,
                AwayTeamId = teamAwayId,
                RefereeId = refereeId,
                DelegateId = delegateId,
                SeasonId = 4
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidRefereeRole()
        {
            var seasonId = await CreateSeason();
            var teamHomeId = await CreateTeam("Tim 1", seasonId);
            var teamAwayId = await CreateTeam("Tim 2", seasonId);
            var delegateId = await CreateDelegate();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamHomeId,
                AwayTeamId = teamAwayId,
                RefereeId = delegateId,
                DelegateId = delegateId,
                SeasonId = seasonId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidDelegateRole()
        {
            var seasonId = await CreateSeason();
            var teamHomeId = await CreateTeam("Tim 1", seasonId);
            var teamAwayId = await CreateTeam("Tim 2", seasonId);
            var refereeId = await CreateReferee();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamHomeId,
                AwayTeamId = teamAwayId,
                RefereeId = refereeId,
                DelegateId = refereeId,
                SeasonId = seasonId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMatch()
        {
            var seasonId = await CreateSeason();
            var teamHomeId = await CreateTeam("Tim 1", seasonId);
            var teamAwayId = await CreateTeam("Tim 2", seasonId);
            var refereeId = await CreateReferee();
            var delegateId = await CreateDelegate();

            var command = new CreateMatchCommand
            {
                Date = "12/12/2020",
                Time = "18:00:00",
                Round = 1,
                HomeTeamId = teamHomeId,
                AwayTeamId = teamAwayId,
                RefereeId = refereeId,
                DelegateId = delegateId,
                SeasonId = seasonId
            };

            var ids = await SendAsync(command);

            var match = await FindAsync<Match>(ids[0]);

            match.Should().NotBeNull();
            match.Date.Should().Be(DateTime.Parse(command.Date));
            match.Time.Should().Be(TimeSpan.Parse(command.Time));
            match.Round.Should().Be(command.Round);
            match.HomeTeamId.Should().Be(command.HomeTeamId);
            match.AwayTeamId.Should().Be(command.AwayTeamId);
            match.RefereeId.Should().Be(command.RefereeId);
            match.DelegateId.Should().Be(command.DelegateId);
            match.SeasonId.Should().Be(command.SeasonId);
            match.HomePoints.Should().Be(0);
            match.AwayPoints.Should().Be(0);
            ids.Count.Should().Be(3);
        }
    }
}
