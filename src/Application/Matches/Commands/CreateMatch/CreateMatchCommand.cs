using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommand : IRequest<int>
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public int Round { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string RefereeId { get; set; }
        public string DelegateId { get; set; }
        public int SeasonId { get; set; }
    }

    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateMatchCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var date = DateTime.Parse(request.Date);
            var time = TimeSpan.Parse(request.Time);

            var match = new Match
            {
                Date = date,
                Time = time,
                Round = request.Round,
                HomePoints = 0,
                AwayPoints = 0,
                HomeTeamId = request.HomeTeamId,
                AwayTeamId = request.AwayTeamId,
                RefereeId = request.RefereeId,
                DelegateId = request.DelegateId,
                SeasonId = request.SeasonId
            };

            _context.Matches.Add(match);

            await _context.SaveChangesAsync(cancellationToken);

            return match.Id;
        }
    }
}
