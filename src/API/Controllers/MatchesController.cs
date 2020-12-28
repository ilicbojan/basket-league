using Application.Matches.Commands.CreateMatch;
using Application.Matches.Commands.FinishMatch;
using Application.Matches.Queries.GetMatch;
using Application.Matches.Queries.GetMatchesBySeason;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class MatchesController : ApiController
    {
        [Route("/api/seasons/{id}/matches")]
        [HttpGet]
        public async Task<ActionResult<List<MatchDto>>> GetMatchesBySeason(int id, bool isPlayed)
        {
            return await Mediator.Send(new GetMatchesBySeasonQuery(id, isPlayed));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatchVm>> Get(int id)
        {
            return await Mediator.Send(new GetMatchQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<List<int>>> Create(CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("{id}/finish")]
        public async Task<ActionResult<Unit>> Finish(int id)
        {
            return await Mediator.Send(new FinishMatchCommand { Id = id });
        }
    }
}
