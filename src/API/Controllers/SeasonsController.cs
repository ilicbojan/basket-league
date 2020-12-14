using Application.Seasons.Commands.CreateSeason;
using Application.Seasons.Queries.GetSeasonMatches;
using Application.Seasons.Queries.GetSeasons;
using Application.Seasons.Queries.GetSeasonStandings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class SeasonsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SeasonsVm>> GetAll()
        {
            return await Mediator.Send(new GetSeasonsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonVm>> GetStandings(int id)
        {
            return await Mediator.Send(new GetSeasonStandingsQuery { Id = id });
        }

        [HttpGet("{id}/matches")]
        public async Task<ActionResult<List<MatchDto>>> GetMatches(int id, bool isPlayed)
        {
            return await Mediator.Send(new GetSeasonMatchesQuery(id, isPlayed));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSeasonCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
