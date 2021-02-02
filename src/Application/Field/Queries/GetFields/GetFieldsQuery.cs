using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Field.Queries.GetFields
{
    public class GetFieldsQuery : IRequest<FieldsVm>
    {
    }

    public class GetFieldsQueryHandler : IRequestHandler<GetFieldsQuery, FieldsVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetFieldsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FieldsVm> Handle(GetFieldsQuery request, CancellationToken cancellationToken)
        {
            var vm = new FieldsVm();

            vm.Fields = await _context.Fields
                .ProjectTo<FieldDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return vm;
        }
    }
}
