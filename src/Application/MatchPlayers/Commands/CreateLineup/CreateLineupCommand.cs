using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MatchPlayers.Commands.CreateLineup
{
    public class CreateLineupCommand : IRequest
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public List<int> PlayersIds { get; set; } = new List<int>();
    }

    public class CreateLineupCommandHandler : IRequestHandler<CreateLineupCommand>
    {
        private readonly IAppDbContext _context;

        public CreateLineupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateLineupCommand request, CancellationToken cancellationToken)
        {
            // TODO: Can't create if match already have lineups

            var lineup = new List<MatchPlayer>();

            foreach (var id in request.PlayersIds)
            {
                var matchPlayer = new MatchPlayer
                {
                    MatchId = request.MatchId,
                    PlayerId = id,
                    Points = 0,
                    Assists = 0,
                    Fouls = 0
                };

                lineup.Add(matchPlayer);
            }

            _context.MatchPlayers.AddRange(lineup);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
