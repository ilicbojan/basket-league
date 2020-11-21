using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        private readonly IAppDbContext _context;

        public CreateTeamCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Naziv tima je obavezan")
                .MaximumLength(50).WithMessage("Naziv tima ne sme biti duzi od 50 karaktera")
                .MustAsync(BeUniqueName).WithMessage("Izabrani naziv vec postoji");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Teams.AllAsync(x => x.Name != name);
        }
    }
}
