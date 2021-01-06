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

namespace Application.Players.Queries.GetPlayer
{
    public class GetPlayerQuery : IRequest<PlayerVm>
    {
        public int Id { get; set; }
    }

    public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, PlayerVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayerVm> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Players
                .Where(x => x.Id == request.Id)
                .ProjectTo<PlayerVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }

            return vm;
        }
    }
}
