using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<UserVm>
    {
    }

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserVm>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtGenerator _jwtGenerator;

        public GetCurrentUserQueryHandler(IIdentityService identityService, IJwtGenerator jwtGenerator)
        {
            _identityService = identityService;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserVm> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetCurrentUserAsync();

            var vm = new UserVm
            {
                Token = await _jwtGenerator.CreateToken(user),
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            return vm;
        }
    }
}
