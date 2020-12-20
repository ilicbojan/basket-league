using Application.Common.Exceptions;
using Application.Seasons.Commands.CreateSeason;
using Application.Seasons.Queries.GetSeasonStandings;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Seasons.Queries
{
    using static Testing;
    using static Helper;

    public class GetSeasonStandingsTests : TestBase
    {
        [Test]
        public void ShouldRequireValidId()
        {
            var query = new GetSeasonStandingsQuery { Id = 1 };

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }


        [Test]
        public async Task ShouldReturnSeasonStandings()
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

            var query = new GetSeasonStandingsQuery { Id = seasonId };

            var result = await SendAsync(query);

            result.Should().BeOfType<SeasonVm>();
            result.Should().NotBeNull();
            result.Id.Should().Be(seasonId);
            result.Name.Should().NotBeNullOrEmpty();
            result.Year.Should().BeGreaterThan(2000);
            result.Teams.Should().BeOfType<List<TeamDto>>();
        }
    }
}
