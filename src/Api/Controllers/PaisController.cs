using CopperConsumption.Application.Paises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopperConsumption.Api.Controllers
{
    public class PaisController : ApiControllerBase
    {
        private readonly PaisService _paisService;
        public PaisController(PaisService paisService)
        {
            _paisService = paisService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<PaisDto>>> Get()
        {
            return await _paisService.GetPaisesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaisDto>> Get(int id)
        {
            return await _paisService.GetPaisById(id);
        }

        // [HttpPost]
        // public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
        // {
        //     return await Mediator.Send(command);
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
        // {
        //     if (id != command.Id)
        //     {
        //         return BadRequest();
        //     }

        //     await Mediator.Send(command);

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     await Mediator.Send(new DeleteTodoListCommand { Id = id });

        //     return NoContent();
        // }
    }
}
