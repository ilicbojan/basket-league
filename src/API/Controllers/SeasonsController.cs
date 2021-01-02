using Application.Seasons.Commands.CreateSeason;
using Application.Seasons.Queries.GetSeason;
using Application.Seasons.Queries.GetSeasonPlayersStats;
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
        public async Task<ActionResult<List<SeasonDto>>> GetAll()
        {
            return await Mediator.Send(new GetSeasonsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonVm>> Get(int id)
        {
            return await Mediator.Send(new GetSeasonQuery { Id = id });
        }

        [HttpGet("{id}/standings")]
        public async Task<ActionResult<SeasonStandingsVm>> GetStandings(int id)
        {
            return await Mediator.Send(new GetSeasonStandingsQuery { Id = id });
        }

        [HttpGet("{id}/stats-players")]
        public async Task<ActionResult<PlayersStatsVm>> GetPlayersStats(int id)
        {
            return await Mediator.Send(new GetSeasonPlayersStatsQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSeasonCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
