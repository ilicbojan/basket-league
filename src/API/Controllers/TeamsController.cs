﻿using Application.Teams.Commands.CreateTeam;
using Application.Teams.Queries.GetTeam;
using Application.Teams.Queries.GetTeamAllTimeStats;
using Application.Teams.Queries.GetTeamCurrentStats;
using Application.Teams.Queries.GetTeams;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class TeamsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<TeamsVm>> GetAll()
        {
            return await Mediator.Send(new GetTeamsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamVm>> Get(int id)
        {
            return await Mediator.Send(new GetTeamQuery { Id = id });
        }

        [HttpGet("{id}/current-stats")]
        public async Task<ActionResult<TeamCurrentStatsVm>> GetCurrentStats(int id)
        {
            return await Mediator.Send(new GetTeamCurrentStatsQuery { Id = id });
        }

        [HttpGet("{id}/all-time-stats")]
        public async Task<ActionResult<TeamAllTimeStatsVm>> GetAllTimeStats(int id)
        {
            return await Mediator.Send(new GetTeamAllTimeStatsQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTeamCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
