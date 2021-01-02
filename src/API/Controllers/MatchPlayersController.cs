using Application.MatchPlayers.Commands.AddFoul;
using Application.MatchPlayers.Commands.AddPoints;
using Application.MatchPlayers.Commands.CreateLineup;
using Application.MatchPlayers.Queries.GetLineup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/matches")]
    public class MatchPlayersController : ApiController
    {
        [HttpGet("{matchId}/lineup")]
        public async Task<ActionResult<LineupVm>> GetLineup(int matchId)
        {
            return await Mediator.Send(new GetLineupQuery { MatchId = matchId });
        }

        [HttpPost("{matchId}/lineup")]
        public async Task<ActionResult<Unit>> CreateLineup(int matchId, CreateLineupCommand command)
        {
            command.MatchId = matchId;

            return await Mediator.Send(command);
        }

        [HttpPatch("{matchId}/points")]
        public async Task<ActionResult<Unit>> AddPoints(int matchId, AddPointsCommand command)
        {
            command.MatchId = matchId;

            return await Mediator.Send(command);
        }

        [HttpPatch("{matchId}/fouls")]
        public async Task<ActionResult<Unit>> AddFoul(int matchId, AddFoulCommand command)
        {
            command.MatchId = matchId;

            return await Mediator.Send(command);
        }
    }
}
