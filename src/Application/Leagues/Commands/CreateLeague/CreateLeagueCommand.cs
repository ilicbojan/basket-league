using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Leagues.Commands.CreateLeague
{
    public class CreateLeagueCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int CityId { get; set; }
    }

    public class CreateLeagueCommandHandler : IRequestHandler<CreateLeagueCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateLeagueCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
        {
            var league = new League
            {
                Name = request.Name,
                CityId = request.CityId
            };

            _context.Leagues.Add(league);

            await _context.SaveChangesAsync(cancellationToken);

            return league.Id;
        }
    }
}
