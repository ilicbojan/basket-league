using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seasons.Queries.GetSeasonPlayersStats
{
    public class GetSeasonPlayersStatsQuery : IRequest<List<PlayerDto>>
    {
        public int Id { get; set; }
    }

    public class GetSeasonPlayersStatsQueryHandler : IRequestHandler<GetSeasonPlayersStatsQuery, List<PlayerDto>>
    {
        private readonly IAppDbContext _context;

        public GetSeasonPlayersStatsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlayerDto>> Handle(GetSeasonPlayersStatsQuery request, CancellationToken cancellationToken)
        {
            var season = await _context.Seasons.FindAsync(request.Id);

            if (season == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            var matches = season.Matches
                .Where(x => x.IsPlayed)
                .ToList();
            var playersStats = new Dictionary<int, PlayerDto>();

            foreach (var match in matches)
            {
                foreach (var mp in match.MatchPlayers)
                {
                    if (!playersStats.ContainsKey(mp.PlayerId))
                    {
                        playersStats.Add(mp.PlayerId, new PlayerDto
                        {
                            FirstName = mp.Player.User.FirstName,
                            LastName = mp.Player.User.LastName
                        });
                    }

                    var player = playersStats[mp.PlayerId];

                    player.MatchesPlayed++;
                    player.Points += mp.Points;
                    player.Assists += mp.Assists;
                    player.Fouls += mp.Fouls;
                    player.PointsAvg = Math.Round((double)player.Points / player.MatchesPlayed, 2);
                    player.AssistsAvg = Math.Round((double)player.Assists / player.MatchesPlayed, 2);
                    player.FoulsAvg = Math.Round((double)player.Fouls / player.MatchesPlayed, 2);
                }
            }

            return playersStats.Values
                .OrderByDescending(x => x.PointsAvg)
                .ThenByDescending(x => x.AssistsAvg)
                .ToList();
        }
    }
}
