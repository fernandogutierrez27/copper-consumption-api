using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CopperConsumption.Application.Common.Exceptions;
using CopperConsumption.Application.Common.Helpers;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CopperConsumption.Application.Consumos
{
    public class ConsumoService
    {
        private readonly ICopperConsumptionDbContext _db;
        private readonly IMapper _mapper;
        public ConsumoService(
            ICopperConsumptionDbContext db,
            IMapper mapper
            )
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<ConsumoDto>> GetConsumosAsync()
        {
            return await _db.Consumos
                            .Include(c => c.Pais)
                            .AsNoTracking()
                            .ProjectTo<ConsumoDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public async Task<ConsumoDto> GetByPaisAndYear(int paisId, int year)
        {
            var consumo = await _db.Consumos
                                    .Include(c => c.Pais)
                                    .Where(c => c.Año == year && c.PaisId == paisId)
                                    .AsNoTracking()
                                    .ProjectTo<ConsumoDto>(_mapper.ConfigurationProvider)
                                    .SingleOrDefaultAsync();

            if (consumo == null) throw new NotFoundException("Pais/Año", $"{paisId}/{year}");

            return consumo;
        }

        public async Task<List<ConsumoDto>> GetByYear(int year)
        {
            return await _db.Consumos
                            .Include(c => c.Pais)
                            .Where(c => c.Año == year)
                            .AsNoTracking()
                            .ProjectTo<ConsumoDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public async Task<List<ConsumoDto>> GetByPais(int paisId)
        {
            var pais = await _db.Paises.FindAsync(paisId);

            if (pais == null) throw new NotFoundException("Pais", paisId);

            return await _db.Consumos
                            .Include(c => c.Pais)
                            .Where(c => c.PaisId == paisId)
                            .AsNoTracking()
                            .ProjectTo<ConsumoDto>(_mapper.ConfigurationProvider)
                            .OrderBy(c => c.Año)
                            .ToListAsync();
        }

        public async Task<Consumo> CreateConsumo(ConsumoCommand consumo)
        {
            //Consumo _consumo = _mapper.Map<Consumo>(consumo);
            Consumo _consumo = new Consumo
            {
                PaisId = consumo.PaisId,
                Año = consumo.Año,
                Cantidad = consumo.Cantidad
            };

            await ValidationHelper.Validate<Consumo>(new CreateConsumoValidator(_db), _consumo);

            _db.Consumos.Add(_consumo);

            await _db.SaveChangesAsync();

            return _consumo;
        }

        public async Task UpdateConsumo(ConsumoCommand consumo)
        {
            var _consumo = await _db.Consumos.SingleOrDefaultAsync(c => c.Año == consumo.Año && c.PaisId == consumo.PaisId);
            if (_consumo == null) throw new NotFoundException("Consumo", $"PaisId:{consumo.PaisId}/Año:{consumo.Año}");

            _consumo.Cantidad = consumo.Cantidad;

            await ValidationHelper.Validate<Consumo>(new UpdateConsumoValidator(_db), _consumo);

            _db.Consumos.Update(_consumo);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteConsumo(int paisId, int year)
        {
            var _consumo = await _db.Consumos.SingleOrDefaultAsync(c => c.Año == year && c.PaisId == paisId);
            if (_consumo == null) throw new NotFoundException("Consumo", $"PaisId:{paisId}/Año:{year}");

            _db.Consumos.Remove(_consumo);

            await _db.SaveChangesAsync();
        }
    }
}
