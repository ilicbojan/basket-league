using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Players.Queries.GetPlayerCurrentStats
{
    public class GetPlayerCurrentStatsQuery : IRequest<PlayerCurrentStatsVm>
    {
        public int Id { get; set; }
    }

    public class GetPlayerCurrentStatsQueryHandler : IRequestHandler<GetPlayerCurrentStatsQuery, PlayerCurrentStatsVm>
    {
        private readonly IAppDbContext _context;

        public GetPlayerCurrentStatsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerCurrentStatsVm> Handle(GetPlayerCurrentStatsQuery request, CancellationToken cancellationToken)
        {
            var player = await _context.Players.FindAsync(request.Id);

            if (player == null)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }

            var playerMatches = player.PlayerMatches.Where(x => x.Match.Season.IsCurrent && x.Match.IsPlayed);

            var vm = new PlayerCurrentStatsVm();

            foreach (var stats in playerMatches)
            {
                vm.Points += stats.Points;
                vm.Assists += stats.Assists;
                vm.Fouls += stats.Fouls;

                var team = stats.Match.HomeTeamId == player.TeamId
                    ? stats.Match.AwayTeam
                    : stats.Match.HomeTeam;

                var matchPlayer = new MatchPlayerDto
                {
                    Points = stats.Points,
                    Assists = stats.Assists,
                    Fouls = stats.Fouls,
                    Team = new TeamDto { Id = team.Id, Name = team.Name }
                };

                vm.Matches.Add(matchPlayer);
            }

            vm.MatchesPlayed = vm.Matches.Count;
            vm.PointsAvg = Math.Round((double)vm.Points / vm.MatchesPlayed, 2);
            vm.AssistsAvg = Math.Round((double)vm.Assists / vm.MatchesPlayed, 2);
            vm.FoulsAvg = Math.Round((double)vm.Fouls / vm.MatchesPlayed, 2);

            return vm;
        }
    }
}
