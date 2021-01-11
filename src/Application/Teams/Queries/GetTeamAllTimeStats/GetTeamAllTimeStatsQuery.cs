using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teams.Queries.GetTeamAllTimeStats
{
    public class GetTeamAllTimeStatsQuery : IRequest<TeamAllTimeStatsVm>
    {
        public int Id { get; set; }
    }

    public class GetTeamAllTimeStatsQueryHandler : IRequestHandler<GetTeamAllTimeStatsQuery, TeamAllTimeStatsVm>
    {
        private readonly IAppDbContext _context;

        public GetTeamAllTimeStatsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TeamAllTimeStatsVm> Handle(GetTeamAllTimeStatsQuery request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.FindAsync(request.Id);

            if (team == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }

            var vm = new TeamAllTimeStatsVm();

            foreach (var match in team.HomeMatches.Where(x => x.IsPlayed))
            {
                vm.MatchesPlayed++;
                vm.Assists += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Assists);
                vm.Fouls += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Fouls);
                vm.ScoredPoints += match.HomePoints;
                vm.ReceivedPoints += match.AwayPoints;

                if (match.HomePoints > match.AwayPoints)
                {
                    vm.Wins++;
                }
                else
                {
                    vm.Losses++;
                }
            }

            foreach (var match in team.AwayMatches.Where(x => x.IsPlayed))
            {
                vm.MatchesPlayed++;
                vm.Assists += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Assists);
                vm.Fouls += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Fouls);
                vm.ScoredPoints += match.AwayPoints;
                vm.ReceivedPoints += match.HomePoints;

                if (match.HomePoints < match.AwayPoints)
                {
                    vm.Wins++;
                }
                else
                {
                    vm.Losses++;
                }
            }

            vm.PointsDiff = vm.ScoredPoints - vm.ReceivedPoints;
            vm.ScoredPointsAvg = Math.Round((double)vm.ScoredPoints / vm.MatchesPlayed, 2);
            vm.ReceivedPointsAvg = Math.Round((double)vm.ReceivedPoints / vm.MatchesPlayed, 2);
            vm.AssistsAvg = Math.Round((double)vm.Assists / vm.MatchesPlayed, 2);
            vm.FoulsAvg = Math.Round((double)vm.Fouls / vm.MatchesPlayed, 2);
            vm.WinsPercentage = Math.Round((double)vm.Wins / vm.MatchesPlayed * 100, 2);
            vm.LossesPercentage = Math.Round((double)vm.Losses / vm.MatchesPlayed * 100, 2);

            return vm;
        }
    }
}
