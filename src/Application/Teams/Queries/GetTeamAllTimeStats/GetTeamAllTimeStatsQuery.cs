using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
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

            var seasons = new Dictionary<int, SeasonTeamDto>();
            var vm = new TeamAllTimeStatsVm();

            foreach (var match in team.HomeMatches.Where(x => x.IsPlayed))
            {
                vm.MatchesPlayed++;
                vm.Assists += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Assists);
                vm.Fouls += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Fouls);
                vm.ScoredPoints += match.HomePoints;
                vm.ReceivedPoints += match.AwayPoints;

                if (!seasons.ContainsKey(match.SeasonId))
                {
                    seasons.Add(match.SeasonId, new SeasonTeamDto
                    {
                        Id = match.SeasonId,
                        Name = match.Season.Name,
                        Year = match.Season.Year
                    });
                }

                var season = seasons[match.SeasonId];

                season.MatchesPlayed++;
                season.ScoredPoints += match.HomePoints;
                season.ReceivedPoints += match.AwayPoints;
                season.PointsDiff += match.HomePoints - match.AwayPoints;
                season.Assists += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Assists);
                season.Fouls += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Fouls);
                season.ScoredPointsAvg = Math.Round((double)season.ScoredPoints / season.MatchesPlayed, 2);
                season.ReceivedPointsAvg = Math.Round((double)season.ReceivedPoints / season.MatchesPlayed, 2);
                season.AssistsAvg = Math.Round((double)season.Assists / season.MatchesPlayed, 2);
                season.FoulsAvg = Math.Round((double)season.Fouls / season.MatchesPlayed, 2);

                if (match.HomePoints > match.AwayPoints)
                {
                    vm.Wins++;
                    season.Wins++;
                }
                else
                {
                    vm.Losses++;
                    season.Losses++;
                }
            }

            foreach (var match in team.AwayMatches.Where(x => x.IsPlayed))
            {
                vm.MatchesPlayed++;
                vm.Assists += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Assists);
                vm.Fouls += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Fouls);
                vm.ScoredPoints += match.AwayPoints;
                vm.ReceivedPoints += match.HomePoints;

                if (!seasons.ContainsKey(match.SeasonId))
                {
                    seasons.Add(match.SeasonId, new SeasonTeamDto
                    {
                        Id = match.SeasonId,
                        Name = match.Season.Name,
                        Year = match.Season.Year
                    });
                }

                var season = seasons[match.SeasonId];

                season.MatchesPlayed++;
                season.ScoredPoints += match.AwayPoints;
                season.ReceivedPoints += match.HomePoints;
                season.PointsDiff += match.AwayPoints - match.HomePoints;
                season.Assists += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Assists);
                season.Fouls += match.MatchPlayers.Where(x => x.Player.TeamId == team.Id).Sum(x => x.Fouls);
                season.ScoredPointsAvg = Math.Round((double)season.ScoredPoints / season.MatchesPlayed, 2);
                season.ReceivedPointsAvg = Math.Round((double)season.ReceivedPoints / season.MatchesPlayed, 2);
                season.AssistsAvg = Math.Round((double)season.Assists / season.MatchesPlayed, 2);
                season.FoulsAvg = Math.Round((double)season.Fouls / season.MatchesPlayed, 2);

                if (match.HomePoints < match.AwayPoints)
                {
                    vm.Wins++;
                    season.Wins++;
                }
                else
                {
                    vm.Losses++;
                    season.Losses++;
                }
            }

            vm.PointsDiff = vm.ScoredPoints - vm.ReceivedPoints;
            vm.ScoredPointsAvg = Math.Round((double)vm.ScoredPoints / vm.MatchesPlayed, 2);
            vm.ReceivedPointsAvg = Math.Round((double)vm.ReceivedPoints / vm.MatchesPlayed, 2);
            vm.AssistsAvg = Math.Round((double)vm.Assists / vm.MatchesPlayed, 2);
            vm.FoulsAvg = Math.Round((double)vm.Fouls / vm.MatchesPlayed, 2);
            vm.WinsPercentage = Math.Round((double)vm.Wins / vm.MatchesPlayed * 100, 2);
            vm.LossesPercentage = Math.Round((double)vm.Losses / vm.MatchesPlayed * 100, 2);

            vm.Seasons = seasons.Values
                .OrderByDescending(x => x.Year)
                .ToList();

            return vm;
        }
    }
}
