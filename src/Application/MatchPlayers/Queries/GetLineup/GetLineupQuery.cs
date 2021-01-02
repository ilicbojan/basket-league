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

namespace Application.MatchPlayers.Queries.GetLineup
{
    public class GetLineupQuery : IRequest<LineupVm>
    {
        public int MatchId { get; set; }
    }

    public class GetLineupQueryHandler : IRequestHandler<GetLineupQuery, LineupVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetLineupQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LineupVm> Handle(GetLineupQuery request, CancellationToken cancellationToken)
        {
            var match = await _context.Matches.FindAsync(request.MatchId);

            if (match == null)
            {
                throw new NotFoundException(nameof(Match), request.MatchId);
            }

            var vm = new LineupVm();

            vm.Players = match.MatchPlayers
                .AsQueryable()
                .ProjectTo<MatchPlayerDto>(_mapper.ConfigurationProvider)
                .ToList();

            return vm;
        }
    }
}
