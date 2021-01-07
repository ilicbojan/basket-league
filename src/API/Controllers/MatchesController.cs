using Application.Matches.Commands.CreateMatch;
using Application.Matches.Commands.FinishMatch;
using Application.Matches.Queries.GetH2HMatches;
using Application.Matches.Queries.GetMatch;
using Application.Matches.Queries.GetMatches;
using Application.Matches.Queries.GetMatchStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MatchDto = Application.Matches.Queries.GetMatches.MatchDto;

namespace API.Controllers
{
    public class MatchesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<MatchDto>>> GetMatchesBySeason(int? seasonId, int? teamId, bool isPlayed)
        {
            return await Mediator.Send(new GetMatchesQuery(seasonId, teamId, isPlayed));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatchVm>> Get(int id)
        {
            return await Mediator.Send(new GetMatchQuery { Id = id });
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<MatchStatsVm>> GetMatchStats(int id)
        {
            return await Mediator.Send(new GetMatchStatsQuery { Id = id });
        }

        [HttpGet("{id}/h2h")]
        public async Task<ActionResult<H2HMatchesVm>> GetH2H(int id)
        {
            return await Mediator.Send(new GetH2HMatchesQuery { Id = id });
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
