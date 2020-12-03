using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
    {
        private readonly IAppDbContext _context;

        public CreateSeasonCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Naziv sezone je obavezan")
                .MaximumLength(50).WithMessage("Naziv sezone ne sme biti duzi od 50 karaktera")
                .MustAsync(BeUniqueName).WithMessage("Izabrani naziv vec postoji");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Godina odrzavanja sezone je obavezna")
                .InclusiveBetween(2000, 3000).WithMessage("Godina mora biti izmedju 2000 i 3000");

            RuleFor(x => x.LeagueId)
                .NotEmpty().WithMessage("Liga je obavezna")
                .MustAsync(LeagueExists).WithMessage("Izabrana liga ne postoji");

            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage("Teren je obavezan")
                .MustAsync(FieldExists).WithMessage("Izabrani teren ne postoji");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Seasons.AllAsync(x => x.Name != name);
        }

        public async Task<bool> LeagueExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Leagues.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> FieldExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Fields.AnyAsync(x => x.Id == id);
        }
    }
}
