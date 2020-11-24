using Application.Lineups.Commands.CreateLineup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/matches")]
    public class LineupsController : ApiController
    {
        [HttpPost("{matchId}/lineups")]
        public async Task<ActionResult<Unit>> Create(int matchId, CreateLineupCommand command)
        {
            command.MatchId = matchId;

            return await Mediator.Send(command);
        }
    }
}
