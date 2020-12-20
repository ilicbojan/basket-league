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

namespace Application.Seasons.Queries.GetSeasons
{
    public class GetSeasonsQuery : IRequest<List<SeasonDto>>
    {
    }

    public class GetSeasonsQueryHandler : IRequestHandler<GetSeasonsQuery, List<SeasonDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SeasonDto>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
        {
            var vm = new List<SeasonDto>();

            vm = await _context.Seasons
                .ProjectTo<SeasonDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Year)
                .ToListAsync(cancellationToken);

            return vm;
        }
    }
}
