using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Matches.Queries.GetH2HMatches
{
    public class GetH2HMatchesQuery : IRequest<H2HMatchesVm>
    {
        public int Id { get; set; }
    }

    public class GetH2HMatchesQueryHandler : IRequestHandler<GetH2HMatchesQuery, H2HMatchesVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetH2HMatchesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<H2HMatchesVm> Handle(GetH2HMatchesQuery request, CancellationToken cancellationToken)
        {
            var match = await _context.Matches.FindAsync(request.Id);

            if (match == null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }

            var vm = new H2HMatchesVm();

            vm.Matches = await _context.Matches
                .Where(x => ((x.HomeTeamId == match.HomeTeamId && x.AwayTeamId == match.AwayTeamId) 
                    || (x.HomeTeamId == match.AwayTeamId && x.AwayTeamId == match.HomeTeamId))
                    && x.IsPlayed)
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Date).ThenByDescending(x => x.Time)
                .ToListAsync(cancellationToken);

            return vm;
        }
    }
}
