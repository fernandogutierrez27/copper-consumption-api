﻿using CopperConsumption.Application.Consumos;
using CopperConsumption.Application.Paises;
using CopperConsumption.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopperConsumption.Api.Controllers
{
    [Produces("application/json")]
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

        /// <summary>
        /// Crea un País
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Pais
        ///     {
        ///        "nombre": "Chile"
        ///     }
        ///
        /// </remarks>
        /// <param name="pais">Objeto que representa el país a crear.</param>
        /// <returns>El Id del País recientemente Creado</returns>
        /// <response code="201">Success: Retorna un objeto con el Id del País recientemente Creado.</response>
        /// <response code="400">Error: El nombre del país es inválido o ya existe.</response> 
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<ActionResult> Create(Pais pais)
        {
            int _id = await _paisService.CreatePais(pais);

            return Created("", new { id = _id });
        }

        /// <summary>
        /// Actualiza un País existente
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Pais/{id}
        ///     {
        ///        "id": 22,
        ///        "nombre": "Chile"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Identificador del País a actualizar.</param>
        /// <param name="pais">Objeto que representa el país a actualizar.</param>
        /// <response code="204">Success: No Content - Actualización correcta</response>
        /// <response code="400">Error: El nombre del país es inválido o ya existe, ó el identificador del país no corresponde con la URI.</response> 
        /// <response code="404">Error: No se encuentra un país asociado al identificador.</response> 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
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

        /// <summary>
        /// Elimina un País existente
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Pais/{id}
        ///
        /// </remarks>
        /// <param name="id">Identificador del país a eliminar.</param>
        /// <response code="204">Success: No Content.</response>
        /// <response code="400">Error: El país contiene consumos asociados, no es posible eliminar.</response> 
        /// <response code="404">Error: No se encuentra un país asociado al identificador.</response> 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Crear un registro de consumo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Pais/{id}/Consumos/{year}
        ///     {
        ///        "paisId": 22,
        ///        "año": 2020
        ///        "cantidad": 132.4
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Identificador del País asociado al consumo.</param>
        /// <param name="year">Año del consumo.</param>
        /// <param name="consumo">Objeto que representa el consumo a crear.</param>
        /// <response code="201">Success: Se retorna el identificador del país y del año asociados al consumo creado.</response>
        /// <response code="400">Error: Ya existe un consumo para este país y periodo, ó el valor de cantidad es inválido.</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
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

        /// <summary>
        /// Actualiza un registro de consumo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Pais/{id}/Consumos/{year}
        ///     {
        ///        "paisId": 22,
        ///        "año": 2020
        ///        "cantidad": 132.4
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Identificador del País asociado al consumo.</param>
        /// <param name="year">Año del consumo.</param>
        /// <param name="consumo">Objeto que representa el consumo a actualizar.</param>
        /// <response code="204">Success: No Content - Actualización correcta.</response>
        /// <response code="400">El valor de cantidad es inválido o los valores ingresados para el identificador de país o año no corresponden con la URI.</response> 
        /// <response code="404">Error: No se encuentra un consumo asociado a dicho país y periodo.</response> 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
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

        /// <summary>
        /// Elimina un registro de consumo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Pais/{id}/Consumos/{year}
        ///
        /// </remarks>
        /// <param name="id">Identificador del País asociado al consumo.</param>
        /// <param name="year">Año del consumo.</param>
        /// <response code="204">Success: No Content - Eliminación correcta.</response>
        /// <response code="400">Los valores ingresados para el identificador de país o año no corresponden con la URI.</response> 
        /// <response code="404">Error: No se encuentra un consumo asociado a dicho país y periodo.</response> 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}/Consumos/{year}")]
        public async Task<ActionResult> DeleteConsumo(int id, int year)
        {
            await _consumoService.DeleteConsumo(id, year);

            return NoContent();
        }

        #endregion Consumos
    }
}
