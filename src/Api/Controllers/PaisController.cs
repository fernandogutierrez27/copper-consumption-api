using CopperConsumption.Application.Paises;
using CopperConsumption.Domain.Entities;
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

        [HttpPost]
        public async Task<ActionResult> Create(Pais pais)
        {
            int _id = await _paisService.CreatePais(pais);

            return CreatedAtAction("Get", new {id = _id});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Pais pais)
        {
            if (id != pais.Id)
            {
                return BadRequest();
            }

            await _paisService.UpdatePais(pais);

            return NoContent();
        }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     await Mediator.Send(new DeleteTodoListCommand { Id = id });

        //     return NoContent();
        // }
    }
}
