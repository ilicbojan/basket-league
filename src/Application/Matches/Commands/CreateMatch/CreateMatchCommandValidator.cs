using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        private readonly IAppDbContext _context;
        private readonly IIdentityService _identityService;

        public CreateMatchCommandValidator(IAppDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Datum je obavezan");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Vreme je obavezno");

            RuleFor(x => x.Round)
                .NotEmpty().WithMessage("Runda je obavezna");

            RuleFor(x => x.HomeTeamId)
                .NotEmpty().WithMessage("Domaci tim je obavezan")
                .NotEqual(x => x.AwayTeamId).WithMessage("Domaci i gostujuci tim moraju biti razliciti")
                .MustAsync(TeamExists).WithMessage("Izabrani domaci tim ne postoji ili nije u izabranoj sezoni");

            RuleFor(x => x.AwayTeamId)
                .NotEmpty().WithMessage("Gostujuci tim je obavezan")
                .MustAsync(TeamExists).WithMessage("Izabrani gostujuci tim ne postoji ili nije u izabranoj sezoni");

            RuleFor(x => x.RefereeId)
                .NotEmpty().WithMessage("Sudija je obavezan")
                .MustAsync(UserExists).WithMessage("Izabrani sudija ne postoji")
                .MustAsync(IsReferee).WithMessage("Izabrani korisnik nije sudija");

            RuleFor(x => x.DelegateId)
                .NotEmpty().WithMessage("Delegat je obavezan")
                .MustAsync(UserExists).WithMessage("Izabrani delegat ne postoji")
                .MustAsync(IsDelegate).WithMessage("Izabrani korisnik nije delegat");

            RuleFor(x => x.SeasonId)
                .NotEmpty().WithMessage("Sezona je obavezna")
                .MustAsync(SeasonExists).WithMessage("Izabrana sezona ne postoji");
        }

        public async Task<bool> TeamExists(CreateMatchCommand command, int id, CancellationToken cancellationToken)
        {
            return await _context.Teams.AnyAsync(x => x.Id == id && x.TeamSeasons.Any(t => t.SeasonId == command.SeasonId));
        }

        public async Task<bool> UserExists(string id, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> SeasonExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Seasons.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsReferee(string id, CancellationToken cancellationToken)
        {
            return await _identityService.IsUserInRoleAsync(id, RoleEnum.Referee);
        }

        public async Task<bool> IsDelegate(string id, CancellationToken cancellationToken)
        {
            return await _identityService.IsUserInRoleAsync(id, RoleEnum.Delegate);
        }
    }
}
