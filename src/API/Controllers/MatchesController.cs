﻿using Application.Matches.Commands.CreateMatch;
using Application.Matches.Queries.GetMatch;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class MatchesController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchVm>> Get(int id)
        {
            return await Mediator.Send(new GetMatchQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<List<int>>> Create(CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
