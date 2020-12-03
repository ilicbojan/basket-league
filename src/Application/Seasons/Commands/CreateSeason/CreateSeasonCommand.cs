using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int LeagueId { get; set; }
        public int FieldId { get; set; }
    }

    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateSeasonCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {
            var season = new Season
            {
                Name = request.Name,
                Year = request.Year,
                LeagueId = request.LeagueId,
                FieldId = request.FieldId
            };

            _context.Seasons.Add(season);

            await _context.SaveChangesAsync(cancellationToken);

            return season.Id;
        }
    }
}
