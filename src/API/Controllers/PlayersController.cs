using Application.Players.Commands.CreatePlayer;
using Application.Players.Queries.GetPlayers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PlayersController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PlayersVm>> GetAll(int? teamId)
        {
            return await Mediator.Send(new GetPlayersQuery(teamId));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePlayerCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
