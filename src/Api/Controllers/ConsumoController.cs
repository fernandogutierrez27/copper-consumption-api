using CopperConsumption.Application.Consumos;
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
    [Route("/api/Consumos")]
    public class ConsumoController : ApiControllerBase
    {
        private readonly ConsumoService _consumoService;

        public ConsumoController(ConsumoService consumoService)
        {
            _consumoService = consumoService;
        }

        /// <summary>
        /// Obtiene un listado de todos los consumos para todos los países y todos los años
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Consumos
        ///
        /// </remarks>
        /// <returns>El listado de consumos registrados en el sistema</returns>
        /// <response code="200">Success: Retorna un arreglo con el listado de todos los consumos registrados.</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<List<ConsumoDto>>> Get()
        {
            return await _consumoService.GetConsumosAsync();
        }

        /// <summary>
        /// Obtiene un listado de todos los consumos para determinado año
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Consumos/{year}
        ///
        /// </remarks>
        /// <param name="year">Año a consultar.</param>
        /// <returns>El listado de consumos registrados en el sistema para determinado año.</returns>
        /// <response code="200">Success: Retorna un arreglo con el listado de los consumos registrados para dicho periodo.</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{year}")]
        public async Task<ActionResult<List<ConsumoDto>>> GetByYear(int year)
        {
            return await _consumoService.GetByYear(year);
        }
    }
}
