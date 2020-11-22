using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int JerseyNumber { get; set; }
        public string JMBG { get; set; }
        public string PhoneNumber { get; set; }
        public int? TeamId { get; set; }
    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IIdentityService _identityService;

        public CreatePlayerCommandHandler(IAppDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = new AppUser
                    {
                        Email = request.Email,
                        UserName = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PhoneNumber = request.PhoneNumber
                    };

                    await _identityService.CreateUserAsync(user, request.Password, RoleEnum.Player);

                    await _context.SaveChangesAsync(cancellationToken);

                    var player = new Player
                    {
                        
                        JerseyNumber = request.JerseyNumber,
                        JMBG = request.JMBG,
                        TeamId = request.TeamId,
                        UserId = user.Id
                    };

                    _context.Players.Add(player);

                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    return player.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
