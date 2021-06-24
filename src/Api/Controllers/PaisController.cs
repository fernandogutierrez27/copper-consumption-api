using CopperConsumption.Application.Consumos;
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
        private readonly ConsumoService _consumoService;
        public PaisController(
            PaisService paisService,
            ConsumoService consumoService)
        {
            _paisService = paisService;
            _consumoService = consumoService;
        }

        /// <summary>
        /// Obtiene un listado de todos los países
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PaisDto>>> Get()
        {
            return await _paisService.GetPaisesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaisDto>> GetById(int id)
        {
            return await _paisService.GetPaisById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pais pais)
        {
            int _id = await _paisService.CreatePais(pais);

            return Created("", new { id = _id });
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _paisService.DeletePais(id);

            return NoContent();
        }

        #region Consumos

        [HttpGet("{id}/Consumos")]
        public async Task<ActionResult<List<ConsumoDto>>> GetConsumosByPais(int id)
        {
            return await _consumoService.GetByPais(id);
        }

        [HttpGet("{id}/Consumos/{year}")]
        public async Task<ActionResult<ConsumoDto>> GetConsumoByPaisAndYear(int id, int year)
        {
            return await _consumoService.GetByPaisAndYear(id, year);
        }

        [HttpPost("{id}/Consumos/{year}")]
        public async Task<ActionResult> CreateConsumo(int id, int year, ConsumoCommand consumo)
        {
            if (id != consumo.PaisId || year != consumo.Año)
            {
                return BadRequest();
            }

            Consumo _consumo = await _consumoService.CreateConsumo(consumo);

            return Created("", new { PaisId = _consumo.PaisId, Año = _consumo.Año });
        }

        [HttpPut("{id}/Consumos/{year}")]
        public async Task<ActionResult> UpdateConsumo(int id, int year, ConsumoCommand consumo)
        {
            if (id != consumo.PaisId || year != consumo.Año)
            {
                return BadRequest();
            }

            await _consumoService.UpdateConsumo(consumo);

            return NoContent();
        }

        [HttpDelete("{id}/Consumos/{year}")]
        public async Task<ActionResult> DeleteConsumo(int id, int year)
        {
            await _consumoService.DeleteConsumo(id, year);

            return NoContent();
        }

        #endregion Consumos
    }
}
