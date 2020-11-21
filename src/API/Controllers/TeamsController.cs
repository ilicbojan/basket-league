using Application.Teams.Commands.CreateTeam;
using Application.Teams.Queries.GetTeam;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class TeamsController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamVm>> Get(int id)
        {
            return await Mediator.Send(new GetTeamQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTeamCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
