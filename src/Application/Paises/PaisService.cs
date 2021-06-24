using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CopperConsumption.Application.Common.Exceptions;
using CopperConsumption.Application.Common.Helpers;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CopperConsumption.Application.Paises
{
    public class PaisService
    {
        private readonly ICopperConsumptionDbContext _db;
        private readonly IMapper _mapper;
        public PaisService(
            ICopperConsumptionDbContext db,
            IMapper mapper
            )
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<PaisDto>> GetPaisesAsync()
        {
            return await _db.Paises
                            .AsNoTracking()
                            .ProjectTo<PaisDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public async Task<PaisDto> GetPaisById(int id)
        {
            var pais = await _db.Paises
                                .ProjectTo<PaisDto>(_mapper.ConfigurationProvider)
                                .SingleOrDefaultAsync(p => p.Id == id);

            if (pais == null) throw new NotFoundException("Pais", id);

            return pais;
        }

        public async Task<int> CreatePais(Pais pais)
        {
            pais.Id = 0;
            await ValidationHelper.Validate<Pais>(new PaisValidator(_db), pais);

            _db.Paises.Add(pais);

            await _db.SaveChangesAsync();

            return pais.Id;
        }

        public async Task UpdatePais(Pais pais)
        {
            var _pais = await _db.Paises.FindAsync(pais.Id);
            if (_pais == null) throw new NotFoundException("Pais", pais.Id);

            await ValidationHelper.Validate<Pais>(new PaisValidator(_db), pais);

            _pais.Nombre = pais.Nombre;
            _db.Paises.Update(_pais);

            await _db.SaveChangesAsync();
        }
    }
}
