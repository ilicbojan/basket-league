using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teams.Queries.GetTeams
{
    public class GetTeamsQuery : IRequest<TeamsVm>
    {
    }

    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, TeamsVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamsVm> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var vm = new TeamsVm();

            vm.Teams = await _context.Teams
                .ProjectTo<TeamDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return vm;
        }
    }
}
