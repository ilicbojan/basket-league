using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teams.Queries.GetTeamCurrentStats
{
    public class GetTeamCurrentStatsQuery : IRequest<TeamCurrentStatsVm>
    {
        public int Id { get; set; }
    }

    public class GetTeamCurrentStatsQueryHandler : IRequestHandler<GetTeamCurrentStatsQuery, TeamCurrentStatsVm>
    {
        private readonly IAppDbContext _context;

        public GetTeamCurrentStatsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TeamCurrentStatsVm> Handle(GetTeamCurrentStatsQuery request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.FindAsync(request.Id);

            if (team == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }

            var currentSeason = team.TeamSeasons.SingleOrDefault(x => x.Season.IsCurrent).Season;

            if (currentSeason == null)
            {
                throw new Exception("There is no current season");
            }

            var matches = currentSeason.Matches
                .Where(x => (x.HomeTeamId == team.Id || x.AwayTeamId == team.Id) && x.IsPlayed)
                .ToList();

            var vm = new TeamCurrentStatsVm();

            foreach (var match in matches)
            {
                vm.MatchesPlayed++;
                vm.Assists += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Assists);
                vm.Fouls += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Fouls);

                if (match.HomeTeamId == team.Id)
                {
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
                else if (match.AwayTeamId == team.Id)
                {
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
            }

            vm.PointsDiff = vm.ScoredPoints - vm.ReceivedPoints;
            vm.ScoredPointsAvg = Math.Round((double)vm.ScoredPoints / vm.MatchesPlayed, 2);
            vm.ReceivedPointsAvg = Math.Round((double)vm.ReceivedPoints / vm.MatchesPlayed, 2);
            vm.AssistsAvg = Math.Round((double)vm.Assists / vm.MatchesPlayed, 2);
            vm.FoulsAvg = Math.Round((double)vm.Fouls / vm.MatchesPlayed, 2);
            vm.WinsPercentage = Math.Round((double)vm.Wins / vm.MatchesPlayed * 100, 2);
            vm.LossesPercantage = Math.Round((double)vm.Losses / vm.MatchesPlayed * 100, 2);

            return vm;
        }
    }
}
