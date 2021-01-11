using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Players.Queries.GetPlayerAllTimeStats
{
    public class GetPlayerAllTimeStatsQuery : IRequest<PlayerAllTimeStatsVm>
    {
        public int Id { get; set; }
    }

    public class GetPlayerAllTimeStatsQueryHandler : IRequestHandler<GetPlayerAllTimeStatsQuery, PlayerAllTimeStatsVm>
    {
        private readonly IAppDbContext _context;

        public GetPlayerAllTimeStatsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerAllTimeStatsVm> Handle(GetPlayerAllTimeStatsQuery request, CancellationToken cancellationToken)
        {
            var player = await _context.Players.FindAsync(request.Id);

            if (player == null)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }

            var vm = new PlayerAllTimeStatsVm();

            foreach (var stats in player.PlayerMatches.Where(x => x.Match.IsPlayed))
            {
                vm.MatchesPlayed++;
                vm.Points += stats.Points;
                vm.Assists += stats.Assists;
                vm.Fouls += stats.Fouls;
            }

            vm.PointsAvg = Math.Round((double)vm.Points / vm.MatchesPlayed, 2);
            vm.AssistsAvg = Math.Round((double)vm.Assists / vm.MatchesPlayed, 2);
            vm.FoulsAvg = Math.Round((double)vm.Fouls / vm.MatchesPlayed, 2);

            return vm;
        }
    }
}
