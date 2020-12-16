using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seasons.Queries.GetSeasonStandings
{
    public class GetSeasonStandingsQuery : IRequest<SeasonVm>
    {
        public int Id { get; set; }
    }

    public class GetSeasonStandingsQueryHandler : IRequestHandler<GetSeasonStandingsQuery, SeasonVm>
    {
        private readonly IAppDbContext _context;

        public GetSeasonStandingsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SeasonVm> Handle(GetSeasonStandingsQuery request, CancellationToken cancellationToken)
        {
            var season = await _context.Seasons.FindAsync(request.Id);

            if (season == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            var vm = new SeasonVm
            {
                Id = season.Id,
                Name = season.Name,
                Year = season.Year
            };

            var teams = new Dictionary<int, TeamDto>();

            foreach (var ts in season.TeamSeasons.ToList())
            {
                var team = new TeamDto { Id = ts.TeamId, Name = ts.Team.Name };
                teams.Add(team.Id, team);
            }

            var matches = season.Matches
                .Where(x => x.IsPlayed)
                .ToList();

            foreach (var match in matches)
            {
                var homeTeam = teams[match.HomeTeamId];
                var awayTeam = teams[match.AwayTeamId];

                homeTeam.MatchesPlayed++;
                homeTeam.ScoredPoints += match.HomePoints;
                homeTeam.ReceivedPoints += match.AwayPoints;
                homeTeam.PointsDiff += match.HomePoints - match.AwayPoints;
                homeTeam.Points++;

                awayTeam.MatchesPlayed++;
                awayTeam.ScoredPoints += match.AwayPoints;
                awayTeam.ReceivedPoints += match.HomePoints;
                awayTeam.PointsDiff += match.AwayPoints - match.HomePoints;
                awayTeam.Points++;

                if (match.HomePoints > match.AwayPoints)
                {
                    homeTeam.Wins++;
                    homeTeam.Points++;
                    awayTeam.Losses++;
                }
                else
                {
                    homeTeam.Losses++;
                    awayTeam.Wins++;
                    awayTeam.Points++;
                }
            }

            vm.Standings = teams.Values
                .OrderByDescending(x => x.Points)
                .ToList();

            return vm;
        }
    }
}
