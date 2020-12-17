using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Matches.Commands.FinishMatch
{
    public class FinishMatchCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class FinishMatchCommandHandler : IRequestHandler<FinishMatchCommand>
    {
        private readonly IAppDbContext _context;

        public FinishMatchCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _context.Matches.FindAsync(request.Id);

            if (match == null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }

            match.IsPlayed = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
