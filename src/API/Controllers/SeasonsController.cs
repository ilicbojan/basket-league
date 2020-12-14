using Application.Seasons.Commands.CreateSeason;
using Application.Seasons.Queries.GetSeasonResults;
using Application.Seasons.Queries.GetSeasons;
using Application.Seasons.Queries.GetSeasonStandings;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}/results")]
        public async Task<ActionResult<ResultsVm>> GetResults(int id)
        {
            return await Mediator.Send(new GetSeasonResultsQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSeasonCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
