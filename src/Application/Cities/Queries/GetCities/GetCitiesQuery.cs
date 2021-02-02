

using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cities.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<CitiesVm>
    {
    }

    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, CitiesVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitiesVm> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var vm = new CitiesVm();

            vm.Cities = await _context.Cities
                .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return vm;
        }
    }
}
