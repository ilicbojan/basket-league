using Application.Seasons.Commands.CreateSeason;
using Application.Seasons.Queries.Dtos;
using Application.Seasons.Queries.GetSeasons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSeasonCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
