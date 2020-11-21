using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateTeamCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team
            {
                Name = request.Name
            };

            _context.Teams.Add(team);

            await _context.SaveChangesAsync(cancellationToken);

            return team.Id;
        }
    }
}
