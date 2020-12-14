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

namespace Application.Seasons.Queries.GetSeasonMatches
{
    public class GetSeasonMatchesQuery : IRequest<List<MatchDto>>
    {
        public GetSeasonMatchesQuery(int id, bool isPlayed)
        {
            Id = id;
            IsPlayed = isPlayed;
        }

        public int Id { get; set; }
        public bool IsPlayed { get; set; }
    }

    public class GetSeasonMatchesQueryHandler : IRequestHandler<GetSeasonMatchesQuery, List<MatchDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonMatchesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MatchDto>> Handle(GetSeasonMatchesQuery request, CancellationToken cancellationToken)
        {
            var season = await _context.Seasons.FindAsync(request.Id);

            if (season == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            var matches = season.Matches
                .Where(x => x.IsPlayed == request.IsPlayed)
                .AsQueryable()
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider);

            if (request.IsPlayed)
            {
                matches = matches.OrderByDescending(x => x.Date)
                    .ThenByDescending(x => x.Time);
            }
            else
            {
                matches = matches.OrderBy(x => x.Date)
                    .ThenBy(x => x.Time);
            }

            var vm = matches.ToList();

            return vm;
        }
    }
}
