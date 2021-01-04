using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Players.Queries.GetPlayers
{
    public class GetPlayersQuery : IRequest<PlayersVm>
    {
        public GetPlayersQuery(int? teamId)
        {
            TeamId = teamId;
        }

        public int? TeamId { get; set; }
    }

    public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, PlayersVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayersQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayersVm> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Player> players = Enumerable.Empty<Player>().AsQueryable();

            if (request.TeamId != null)
            {
                var team = await _context.Teams.FindAsync(request.TeamId);

                if (team == null)
                {
                    throw new NotFoundException(nameof(Team), request.TeamId);
                }

                players = team.Players.AsQueryable();
            }

            var vm = new PlayersVm();


            vm.Players = players
                .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                .ToList();

            return vm;
        }
    }
}
