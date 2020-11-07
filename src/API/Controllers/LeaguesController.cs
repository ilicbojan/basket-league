using Application.Leagues.Commands.CreateLeague;
using Application.Leagues.Queries.Dtos;
using Application.Leagues.Queries.GetLeagues;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class LeaguesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<LeaguesVm>> GetAll()
        {
            return await Mediator.Send(new GetLeaguesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateLeagueCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
