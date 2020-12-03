using Application.Teams.Commands.CreateTeam;
using Application.Teams.Queries.GetTeam;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Teams.Queries
{
    using static Testing;

    public class GetTeamTests : TestBase
    {
        [Test]
        public async Task ShouldReturnTeam()
        {
            var teamId = await SendAsync(new CreateTeamCommand { Name = "Tim 1" });

            var query = new GetTeamQuery { Id = teamId };

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Name.Should().NotBeNullOrEmpty();
        }
    }
}
