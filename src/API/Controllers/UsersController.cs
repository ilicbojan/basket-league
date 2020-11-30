using Application.Users.Commands.CreateUser;
using Application.Users.Queries.LoginUser;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserVm>> Login(LoginUserQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
