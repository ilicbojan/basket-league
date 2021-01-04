using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Matches.Queries.GetMatches
{
    public class GetMatchesQuery : IRequest<List<MatchDto>>
    {
        public GetMatchesQuery(int? seasonId, int? teamId, bool isPlayed)
        {
            SeasonId = seasonId;
            TeamId = teamId;
            IsPlayed = isPlayed;
        }

        public int? SeasonId { get; set; }
        public int? TeamId { get; set; }
        public bool IsPlayed { get; set; }
    }

    public class GetMatchesQueryHandler : IRequestHandler<GetMatchesQuery, List<MatchDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetMatchesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MatchDto>> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Match> matches = Enumerable.Empty<Match>().AsQueryable();

            if (request.SeasonId != null)
            {
                var season = await _context.Seasons.FindAsync(request.SeasonId);

                if (season == null)
                {
                    throw new NotFoundException(nameof(Season), request.SeasonId);
                }

                matches = season.Matches
                    .Where(x => x.IsPlayed == request.IsPlayed)
                    .AsQueryable();
            }
            else if (request.TeamId != null)
            {
                var team = await _context.Teams.FindAsync(request.TeamId);

                if (team == null)
                {
                    throw new NotFoundException(nameof(Team), request.TeamId);
                }

                var allMatches = team.HomeMatches
                    .Where(x => x.IsPlayed == request.IsPlayed)
                    .ToList();

                var awayMatches = team.AwayMatches
                    .Where(x => x.IsPlayed == request.IsPlayed);

                allMatches.AddRange(awayMatches);

                matches = allMatches.AsQueryable();
            }

            if (request.IsPlayed)
            {
                matches = matches
                    .OrderByDescending(x => x.Date)
                    .ThenByDescending(x => x.Time);
            }
            else
            {
                matches = matches
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.Time);
            }

            var vm = matches
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .ToList();

            return vm;
        }
    }
}
