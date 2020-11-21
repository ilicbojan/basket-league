using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teams.Queries.GetTeam
{
    public class GetTeamQuery : IRequest<TeamVm>
    {
        public int Id { get; set; }
    }

    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, TeamVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamVm> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Teams
                .Where(x => x.Id == request.Id)
                .ProjectTo<TeamVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return vm;
        }
    }
}
