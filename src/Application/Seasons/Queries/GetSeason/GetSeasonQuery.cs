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

namespace Application.Seasons.Queries.GetSeason
{
    public class GetSeasonQuery : IRequest<SeasonVm>
    {
        public int Id { get; set; }
    }

    public class GetSeasonQueryHandler : IRequestHandler<GetSeasonQuery, SeasonVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeasonVm> Handle(GetSeasonQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Seasons
                .Where(x => x.Id == request.Id)
                .ProjectTo<SeasonVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            return vm;
        }
    }
}
