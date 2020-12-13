using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MatchPlayers.Commands.AddPoints
{
    public class AddPointsCommand : IRequest
    {
        public int MatchId { get; set; }
        public int ScorePlayerId { get; set; }
        public int Points { get; set; }
        public int? AssistPlayerId { get; set; }
    }

    public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand>
    {
        private readonly IAppDbContext _context;

        public AddPointsCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddPointsCommand request, CancellationToken cancellationToken)
        {
            var match = await _context.Matches.FindAsync(request.MatchId);

            if (match == null)
            {
                throw new NotFoundException(nameof(Match), request.MatchId);
            }

            var scorePlayer = match.MatchPlayers.SingleOrDefault(x => x.PlayerId == request.ScorePlayerId);

            if (scorePlayer == null)
            {
                throw new NotFoundException(nameof(MatchPlayer), request.ScorePlayerId);
            }

            scorePlayer.Points += request.Points;

            if (request.AssistPlayerId != null)
            {
                var assistPlayer = match.MatchPlayers.SingleOrDefault(x => x.PlayerId == request.AssistPlayerId);

                if (assistPlayer == null)
                {
                    throw new NotFoundException(nameof(MatchPlayer), request.AssistPlayerId);
                }

                if (scorePlayer.Player.TeamId != assistPlayer.Player.TeamId)
                {
                    throw new Exception("Igraci moraju biti iz istog tima");
                }

                assistPlayer.Assists += 1;
            }

            if (scorePlayer.Player.TeamId == match.HomeTeamId)
            {
                match.HomePoints += request.Points;
            }
            else
            {
                match.AwayPoints += request.Points;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
