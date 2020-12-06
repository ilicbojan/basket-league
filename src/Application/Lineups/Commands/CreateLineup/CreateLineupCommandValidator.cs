using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Lineups.Commands.CreateLineup
{
    public class CreateLineupCommandValidator : AbstractValidator<CreateLineupCommand>
    {
        private readonly IAppDbContext _context;

        public CreateLineupCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.MatchId)
                .NotEmpty().WithMessage("Utakmica je obavezna")
                .MustAsync(MatchExists).WithMessage("Izabrana utakmica ne postoji");

            RuleFor(x => x.TeamId)
                .NotEmpty().WithMessage("Tim je obavezan")
                .MustAsync(TeamExists).WithMessage("Izabrani tim ne postoji");

            RuleFor(x => x.PlayersIds)
                .NotEmpty().WithMessage("Igraci su obavezni");

            RuleFor(x => x.PlayersIds.Count)
                .GreaterThanOrEqualTo(3).WithMessage("Utakmicu ne moze igrati manje od 3 igraca");

            RuleForEach(x => x.PlayersIds)
                .MustAsync(PlayerExists).WithMessage("Izabrani igrac ne postoji")
                .MustAsync(IsPlayerInLineup).WithMessage("Izabrani igrac se vec nalazi u postavi");
        }

        public async Task<bool> MatchExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Matches.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> TeamExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Teams.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> PlayerExists(CreateLineupCommand command, int id, CancellationToken cancellationToken)
        {
            return await _context.Players.AnyAsync(x => x.Id == id && x.TeamId == command.TeamId);
        }

        public async Task<bool> IsPlayerInLineup(CreateLineupCommand command, int id, CancellationToken cancellationToken)
        {
            var match = await _context.Matches.FindAsync(command.MatchId);

            if (match == null)
            {
                return true;
            }

            return match.MatchPlayers.All(x => x.PlayerId != id);
        }
    }
}
