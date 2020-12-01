using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Leagues.Queries.GetLeagues
{
    public class GetLeaguesQuery : IRequest<LeaguesVm>
    {
    }

    public class GetLeaguesQueryHandler : IRequestHandler<GetLeaguesQuery, LeaguesVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetLeaguesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LeaguesVm> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
        {
            LeaguesVm vm = new LeaguesVm();

            vm.Leagues = await _context.Leagues
                .ProjectTo<LeagueDto>(_mapper.ConfigurationProvider)
                .OrderBy(l => l.Name)
                .ToListAsync(cancellationToken);

            return vm;
        }
    }
}
