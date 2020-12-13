using Application.MatchPlayers.Commands.AddPoints;
using Application.MatchPlayers.Commands.CreateLineup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/matches")]
    public class MatchPlayersController : ApiController
    {
        [HttpPost("{matchId}/lineups")]
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
    }
}
