using Application.Cities.Queries.GetCities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CitiesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<CitiesVm>> GetAll()
        {
            return await Mediator.Send(new GetCitiesQuery());
        }
    }
}
