using Application.Field.Commands.CreateField;
using Application.Field.Queries.GetFields;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class FieldsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<FieldsVm>> GetAll()
        {
            return await Mediator.Send(new GetFieldsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateFieldCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
