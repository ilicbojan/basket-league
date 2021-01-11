using Application.Players.Commands.CreatePlayer;
using Application.Players.Queries.GetPlayer;
using Application.Players.Queries.GetPlayerAllTimeStats;
using Application.Players.Queries.GetPlayerCurrentStats;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerVm>> Get(int id)
        {
            return await Mediator.Send(new GetPlayerQuery { Id = id });
        }

        [HttpGet("{id}/current-stats")]
        public async Task<ActionResult<PlayerCurrentStatsVm>> GetCurrentStats(int id)
        {
            return await Mediator.Send(new GetPlayerCurrentStatsQuery { Id = id });
        }

        [HttpGet("{id}/all-time-stats")]
        public async Task<ActionResult<PlayerAllTimeStatsVm>> GetAllTimeStats(int id)
        {
            return await Mediator.Send(new GetPlayerAllTimeStatsQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePlayerCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
