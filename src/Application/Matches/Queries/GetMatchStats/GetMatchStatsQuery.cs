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

namespace Application.Matches.Queries.GetMatchStats
{
    public class GetMatchStatsQuery : IRequest<MatchStatsVm>
    {
        public int Id { get; set; }
    }

    public class GetMatchStatsQueryHadnler : IRequestHandler<GetMatchStatsQuery, MatchStatsVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetMatchStatsQueryHadnler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchStatsVm> Handle(GetMatchStatsQuery request, CancellationToken cancellationToken)
        {
            var vm= await _context.Matches
                .Where(x => x.Id == request.Id)
                .ProjectTo<MatchStatsVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }

            return vm;
        }
    }
}
