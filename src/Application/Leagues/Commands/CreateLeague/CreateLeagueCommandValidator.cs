using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Leagues.Commands.CreateLeague
{
    public class CreateLeagueCommandValidator : AbstractValidator<CreateLeagueCommand>
    {
        private readonly IAppDbContext _context;

        public CreateLeagueCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Naziv lige je obavezan")
                .MaximumLength(50).WithMessage("Naziv lige ne sme biti duzi od 50 karaktera")
                .MustAsync(BeUniqueName).WithMessage("Izabrani naziv vec postoji");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("Grad je obavezan")
                .MustAsync(CityExists).WithMessage("Izabrani grad ne postoji");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Leagues.AllAsync(x => x.Name != name);
        }

        public async Task<bool> CityExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Cities.AnyAsync(c => c.Id == id);
        }
    }
}
