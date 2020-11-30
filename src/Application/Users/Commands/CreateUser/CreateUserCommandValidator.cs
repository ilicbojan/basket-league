using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IAppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUserCommandValidator(IAppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email je obavezan")
                .EmailAddress().WithMessage("Email nije u ispravnom formatu")
                .MustAsync(BeUniqueEmail).WithMessage("Email vec postoji");

            RuleFor(x => x.FirstName)
                .MaximumLength(50).WithMessage("Ime ne sme biti duze od 50 karaktera")
                .NotEmpty().WithMessage("Ime je obavezno");

            RuleFor(x => x.LastName)
                .MaximumLength(50).WithMessage("Prezime ne sme biti duze od 50 karaktera")
                .NotEmpty().WithMessage("Prezime je obavezno");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(40).WithMessage("Broj telefona ne sme biti duzi od 40 karaktera")
                .NotEmpty().WithMessage("Broj telefona je obavezan")
                .MustAsync(BeUniquePhoneNumber).WithMessage("Broj telefona vec postoji");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password je obavezan")
                .MinimumLength(6).WithMessage("Password mora da bude duzi od 6 karaktera");

            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty().WithMessage("Potvrda passworda je obavezna")
                .Equal(x => x.Password).WithMessage("Potvrda passworda je razlicita od passworda");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Uloga je obavezna")
                .MustAsync(RoleExists).WithMessage("Izabrana uloga ne postoji");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(x => x.Email != email);
        }

        public async Task<bool> BeUniquePhoneNumber(string phone, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(x => x.PhoneNumber != phone);
        }

        public async Task<bool> RoleExists(string name, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.AnyAsync(x => x.Name == name);
        }
    }
}
