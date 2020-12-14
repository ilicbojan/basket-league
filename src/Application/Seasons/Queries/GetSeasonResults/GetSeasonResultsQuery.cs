using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seasons.Queries.GetSeasonResults
{
    public class GetSeasonResultsQuery : IRequest<ResultsVm>
    {
        public int Id { get; set; }
    }

    public class GetSeasonResultsQueryHandler : IRequestHandler<GetSeasonResultsQuery, ResultsVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonResultsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultsVm> Handle(GetSeasonResultsQuery request, CancellationToken cancellationToken)
        {
            var season = await _context.Seasons.FindAsync(request.Id);

            if (season == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            var vm = new ResultsVm();

            vm.Matches = season.Matches
                .AsQueryable()
                .Where(x => x.IsPlayed)
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Time)
                .ToList();

            return vm;
        }
    }
}
