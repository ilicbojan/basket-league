using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Field.Commands.CreateField
{
    public class CreateFieldCommandValidator : AbstractValidator<CreateFieldCommand>
    {
        private readonly IAppDbContext _context;

        public CreateFieldCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Naziv je obavezan")
              .MaximumLength(50).WithMessage("Naziv ne sme biti duzi od 50 karaktera")
              .MustAsync(BeUniqueName).WithMessage("Izabrani naziv vec postoji");

            RuleFor(x => x.Address)
              .NotEmpty().WithMessage("Adresa je obavezna")
              .MaximumLength(50).WithMessage("Adresa ne sme biti duza od 50 karaktera");

            RuleFor(x => x.CityId)
              .NotEmpty().WithMessage("Grad je obavezan")
              .MustAsync(CityExists).WithMessage("Izabrana drzava ne postoji");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Fields.AllAsync(c => c.Name != name);
        }

        public async Task<bool> CityExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Cities.AnyAsync(c => c.Id == id);
        }
    }
}
