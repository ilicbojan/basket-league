using Application.Leagues.Queries.GetLeagues;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Leagues.Queries
{
    using static Testing;

    public class GetLeaguesTests
    {
        [Test]
        public async Task ShouldReturnAllLeagues()
        {
            // Arrange
            await AddAsync(new League
            {
                Name = "Liga",
                City = new City { Name = "Beograd", Country = new Country { Name = "Srbija" } }
            });

            var query = new GetLeaguesQuery();

            // Act
            var result = await SendAsync(query);

            // Assert
            result.Leagues.Should().HaveCount(1);
        }
    }
}
