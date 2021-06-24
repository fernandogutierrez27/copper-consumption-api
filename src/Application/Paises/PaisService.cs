using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CopperConsumption.Application.Common.Exceptions;
using CopperConsumption.Application.Common.Interfaces;
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
        public async Task<List<PaisDto>> GetPaisesAsync() {
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

            if (pais == null) throw new NotFoundException("Pais",id);

            return pais;
        }
    }
}
