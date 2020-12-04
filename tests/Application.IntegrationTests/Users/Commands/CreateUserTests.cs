using Application.Common.Exceptions;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Users.Commands
{
    using static Testing;
    using static Helper;

    public class CreateUserTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateUserCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueEmail()
        {
            var roleName = await CreateRoleAsync("player");

            await SendAsync(new CreateUserCommand
            {
                Email = "test@test.com",
                FirstName = "Ime",
                LastName = "Prezime",
                PhoneNumber = "0651234567",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = roleName
            });

            var command = new CreateUserCommand
            {
                Email = "test@test.com",
                FirstName = "Ime",
                LastName = "Prezime",
                PhoneNumber = "0651234568",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = roleName
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniquePhoneNumber()
        {
            var roleName = await CreateRoleAsync("player");

            await SendAsync(new CreateUserCommand
            {
                Email = "test@test.com",
                FirstName = "Ime",
                LastName = "Prezime",
                PhoneNumber = "0651234567",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = roleName
            });

            var command = new CreateUserCommand
            {
                Email = "test@test.com",
                FirstName = "Ime",
                LastName = "Prezime",
                PhoneNumber = "0651234568",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = roleName
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidRoleName()
        {
            var command = new CreateUserCommand
            {
                Email = "test@test.com",
                FirstName = "Ime",
                LastName = "Prezime",
                PhoneNumber = "0651234567",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = "test"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidPasswordConfirmation()
        {
            var roleName = await CreateRoleAsync("player");

            var command = new CreateUserCommand
            {
                Email = "test@test.com",
                FirstName = "Ime",
                LastName = "Prezime",
                PhoneNumber = "0651234567",
                Password = "test12",
                PasswordConfirmation = "asdqwe",
                Role = roleName
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateUser()
        {
            var roleName = await CreateRoleAsync("player");

            var command = new CreateUserCommand
            {
                Email = "test@test.com",
                FirstName = "Ime",
                LastName = "Prezime",
                PhoneNumber = "0651234568",
                Password = "test12",
                PasswordConfirmation = "test12",
                Role = roleName
            };

            var userId = await SendAsync(command);

            var user = await FindAsync<AppUser>(userId);

            user.Should().NotBeNull();
            user.Email.Should().Be(command.Email);
            user.FirstName.Should().Be(command.FirstName);
            user.LastName.Should().Be(command.LastName);
            user.PhoneNumber.Should().Be(command.PhoneNumber);
        }
    }
}
