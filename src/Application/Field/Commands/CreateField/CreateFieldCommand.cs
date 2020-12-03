using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Field.Commands.CreateField
{
    public class CreateFieldCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
    }

    public class CreateFieldCommandHandler : IRequestHandler<CreateFieldCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateFieldCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFieldCommand request, CancellationToken cancellationToken)
        {
            var field = new Domain.Entities.Field
            {
                Name = request.Name,
                Address = request.Address,
                CityId = request.CityId,
            };

            _context.Fields.Add(field);

            await _context.SaveChangesAsync(cancellationToken);

            return field.Id;
        }
    }
}
