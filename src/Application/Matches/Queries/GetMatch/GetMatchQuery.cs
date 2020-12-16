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

namespace Application.Matches.Queries.GetMatch
{
    public class GetMatchQuery : IRequest<MatchVm>
    {
        public int Id { get; set; }
    }

    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, MatchVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetMatchQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchVm> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Matches
                .Where(x => x.Id == request.Id)
                .ProjectTo<MatchVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }

            return vm;
        }
    }
}
