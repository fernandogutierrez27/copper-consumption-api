using CopperConsumption.Application.Consumos;
using CopperConsumption.Application.Paises;
using CopperConsumption.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopperConsumption.Api.Controllers
{
    [Route("/api/Consumos")]
    public class ConsumoController : ApiControllerBase
    {
        private readonly ConsumoService _consumoService;
        
        public ConsumoController(ConsumoService consumoService)
        {
            _consumoService = consumoService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ConsumoDto>>> Get()
        {
            return await _consumoService.GetConsumosAsync();
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<List<ConsumoDto>>> GetByYear(int year)
        {
            return await _consumoService.GetByYear(year);
        }
    }
}
