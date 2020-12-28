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

namespace Application.Matches.Queries.GetMatchesBySeason
{
    public class GetMatchesBySeasonQuery : IRequest<List<MatchDto>>
    {
        public GetMatchesBySeasonQuery(int id, bool isPlayed)
        {
            Id = id;
            IsPlayed = isPlayed;
        }

        public int Id { get; set; }
        public bool IsPlayed { get; set; }
    }

    public class GetMatchesBySeasonQueryHandler : IRequestHandler<GetMatchesBySeasonQuery, List<MatchDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetMatchesBySeasonQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MatchDto>> Handle(GetMatchesBySeasonQuery request, CancellationToken cancellationToken)
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
