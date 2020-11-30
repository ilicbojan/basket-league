using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<UserVm>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserVm>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IIdentityService _identityService;

        public LoginUserQueryHandler(IJwtGenerator jwtGenerator, IIdentityService identityService)
        {
            _jwtGenerator = jwtGenerator;
            _identityService = identityService;
        }

        public async Task<UserVm> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.LoginUserAsync(request.Email, request.Password);

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
