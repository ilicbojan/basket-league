using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        private readonly IAppDbContext _context;

        public CreatePlayerCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email je obavezan")
                .EmailAddress().WithMessage("Email nije u ispravnom formatu")
                .MustAsync(BeUniqueEmail).WithMessage("Email vec postoji");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password je obavezan");

            RuleFor(x => x.FirstName)
                .MaximumLength(50).WithMessage("Ime ne sme biti duze od 50 karaktera")
                .NotEmpty().WithMessage("Ime je obavezno");

            RuleFor(x => x.LastName)
                .MaximumLength(50).WithMessage("Prezime ne sme biti duze od 50 karaktera")
                .NotEmpty().WithMessage("Prezime je obavezno");

            RuleFor(x => x.JerseyNumber)
                .InclusiveBetween(1, 99).WithMessage("Broj na dresu mora biti izmedju 1 i 99")
                .NotEmpty().WithMessage("Broj na dresu je obavezan")
                .MustAsync(BeUniqueJerseyNumber).WithMessage("Izabrani broj na dresu je zauzet");

            RuleFor(x => x.JMBG)
                .Length(13).WithMessage("JBMG mora imati 13 cifara")
                .NotEmpty().WithMessage("JMBG je obavezan")
                .MustAsync(BeUniqueJMBG).WithMessage("JMBG vec postoji");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(40).WithMessage("Broj telefona ne sme biti duzi od 40 karaktera")
                .NotEmpty().WithMessage("Broj telefona je obavezan")
                .MustAsync(BeUniquePhoneNumber).WithMessage("Broj telefona vec postoji");

            RuleFor(x => x.TeamId)
                .MustAsync(TeamExists).WithMessage("Izabrani tim ne postoji");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(x => x.Email != email);
        }

        public async Task<bool> BeUniqueJMBG(string jmbg, CancellationToken cancellationToken)
        {
            return await _context.Players.AllAsync(x => x.JMBG != jmbg);
        }

        public async Task<bool> BeUniqueJerseyNumber(CreatePlayerCommand command, int num, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.FindAsync(command.TeamId);

            return  team.Players.All(x => x.JerseyNumber != num);
        }

        public async Task<bool> BeUniquePhoneNumber(string phone, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(x => x.PhoneNumber != phone);
        }

        public async Task<bool> TeamExists(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return true;
            }

            return await _context.Teams.AnyAsync(x => x.Id == id);
        }
    }
}