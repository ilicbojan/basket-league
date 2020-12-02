using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seasons.Queries.GetSeasonStandings
{
    public class GetSeasonStandingsQuery : IRequest
    {
        public int Id { get; set; }
    }

    public class GetSeasonStandingsQueryHandler : IRequestHandler<GetSeasonStandingsQuery>
    {
        private readonly IAppDbContext _context;

        public GetSeasonStandingsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(GetSeasonStandingsQuery request, CancellationToken cancellationToken)
        {
            var season = await _context.Seasons.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (season == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            return Unit.Value;
        }
    }
}
