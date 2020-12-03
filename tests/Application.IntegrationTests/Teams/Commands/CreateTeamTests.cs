using Application.Common.Exceptions;
using Application.Teams.Commands.CreateTeam;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Teams.Commands
{
    using static Testing;

    public class CreateTeamTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTeamCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            await SendAsync(new CreateTeamCommand
            {
                Name = "Tim 1"
            });

            var command = new CreateTeamCommand
            {
                Name = "Tim 1"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTeam()
        {
            var command = new CreateTeamCommand
            {
                Name = "Tim 1"
            };

            var teamId = await SendAsync(command);

            var team = await FindAsync<Team>(teamId);

            team.Should().NotBeNull();
            team.Name.Should().Be(command.Name);
        }
    }
}
