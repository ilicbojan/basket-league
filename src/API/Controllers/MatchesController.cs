using Application.Matches.Commands.CreateMatch;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class MatchesController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<List<int>>> Create(CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
