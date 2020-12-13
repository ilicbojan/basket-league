using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MatchPlayers.Commands.AddFoul
{
    public class AddFoulCommand : IRequest
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
    }

    public class AddFoulCommandHandler : IRequestHandler<AddFoulCommand>
    {
        private readonly IAppDbContext _context;

        public AddFoulCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddFoulCommand request, CancellationToken cancellationToken)
        {
            var match = await _context.Matches.FindAsync(request.MatchId);

            if (match == null)
            {
                throw new NotFoundException(nameof(Match), request.MatchId);
            }

            var player = match.MatchPlayers.SingleOrDefault(x => x.PlayerId == request.PlayerId);

            if (player == null)
            {
                throw new NotFoundException(nameof(MatchPlayer), request.PlayerId);
            }

            player.Fouls += 1;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
