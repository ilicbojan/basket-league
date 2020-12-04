using Application.Common.Exceptions;
using Application.Players.Commands.CreatePlayer;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Players.Commands
{
    using static Testing;
    using static Helper;

    public class CreatePlayerTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreatePlayerCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueEmail()
        {
            await CreateRoleAsync("player");
            var teamId = await CreateTeam();

            await SendAsync(new CreatePlayerCommand
            {
                Email = "test@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891234",
                PhoneNumber = "0651234567",
                TeamId = teamId
            });

            var command = new CreatePlayerCommand
            {
                Email = "test@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 6,
                JMBG = "1234567891235",
                PhoneNumber = "0651234568",
                TeamId = teamId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueJMBG()
        {
            await CreateRoleAsync("player");
            var teamId = await CreateTeam();

            await SendAsync(new CreatePlayerCommand
            {
                Email = "test@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891234",
                PhoneNumber = "0651234567",
                TeamId = teamId
            });

            var command = new CreatePlayerCommand
            {
                Email = "test1@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 6,
                JMBG = "1234567891234",
                PhoneNumber = "0651234568",
                TeamId = teamId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniquePhoneNumber()
        {
            await CreateRoleAsync("player");
            var teamId = await CreateTeam();

            await SendAsync(new CreatePlayerCommand
            {
                Email = "test@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891234",
                PhoneNumber = "0651234567",
                TeamId = teamId
            });

            var command = new CreatePlayerCommand
            {
                Email = "test1@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 6,
                JMBG = "1234567891235",
                PhoneNumber = "0651234567",
                TeamId = teamId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueJerseyNumber()
        {
            await CreateRoleAsync("player");
            var teamId = await CreateTeam();

            await SendAsync(new CreatePlayerCommand
            {
                Email = "test@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891234",
                PhoneNumber = "0651234567",
                TeamId = teamId
            });

            var command = new CreatePlayerCommand
            {
                Email = "test1@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891235",
                PhoneNumber = "0651234568",
                TeamId = teamId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidTeamId()
        {
            await CreateRoleAsync("player");

            var command = new CreatePlayerCommand
            {
                Email = "test1@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891235",
                PhoneNumber = "0651234568",
                TeamId = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreatePlayer()
        {
            await CreateRoleAsync("player");
            var teamId = await CreateTeam();

            var command = new CreatePlayerCommand
            {
                Email = "test@test.com",
                Password = "test12",
                FirstName = "Ime",
                LastName = "Prezime",
                JerseyNumber = 5,
                JMBG = "1234567891235",
                PhoneNumber = "0651234568",
                TeamId = teamId
            };

            var playerId = await SendAsync(command);

            var player = await FindAsync<Player>(playerId);
            var user = await FindAsync<AppUser>(player.UserId);

            player.Should().NotBeNull();
            player.JerseyNumber.Should().Be(command.JerseyNumber);
            player.JMBG.Should().Be(command.JMBG);
            player.TeamId.Should().Be(command.TeamId);
            player.UserId.Should().NotBeNull();

            user.Email.Should().Be(command.Email);
            user.FirstName.Should().Be(command.FirstName);
            user.LastName.Should().Be(command.LastName);
            user.PhoneNumber.Should().Be(command.PhoneNumber);
        }
    }
}
