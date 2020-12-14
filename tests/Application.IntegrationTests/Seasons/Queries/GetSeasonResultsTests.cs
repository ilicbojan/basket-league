using Application.Common.Exceptions;
using Application.Seasons.Commands.CreateSeason;
using Application.Seasons.Queries.GetSeasonResults;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Seasons.Queries
{
    using static Testing;
    using static Helper;

    public class GetSeasonResultsTests : TestBase
    {
        [Test]
        public void ShouldRequireValidId()
        {
            var query = new GetSeasonResultsQuery { Id = 1 };

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldReturnSeasonResults()
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

            var query = new GetSeasonResultsQuery { Id = seasonId };

            var result = await SendAsync(query);

            result.Should().BeOfType<ResultsVm>();
            result.Should().NotBeNull();
            result.Matches.Should().BeOfType<List<MatchDto>>();
        }
    }
}
