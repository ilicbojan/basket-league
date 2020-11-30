﻿using Application.Users.Commands.CreateUser;
using Application.Users.Queries.CurrentUser;
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
        public async Task<ActionResult<Application.Users.Queries.LoginUser.UserVm>> Login(LoginUserQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("current")]
        public async Task<ActionResult<Application.Users.Queries.CurrentUser.UserVm>> Current()
        {
            return await Mediator.Send(new CurrentUserQuery());
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
