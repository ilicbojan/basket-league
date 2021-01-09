using Application.Teams.Commands.CreateTeam;
using Application.Teams.Queries.GetTeam;
using Application.Teams.Queries.GetTeamCurrentStats;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}/current-stats")]
        public async Task<ActionResult<TeamCurrentStatsVm>> GetCurrentStats(int id)
        {
            return await Mediator.Send(new GetTeamCurrentStatsQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTeamCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
