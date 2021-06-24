using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CopperConsumption.Application.Consumos
{
    public class CreateConsumoValidator : AbstractValidator<Consumo>
    {
        private readonly ICopperConsumptionDbContext _context;

        public CreateConsumoValidator(ICopperConsumptionDbContext context)
        {
            _context = context;

            RuleFor(c => c.Cantidad)
                .NotEmpty().WithMessage("Cantidad es requerido.")
                .MustAsync(async (Consumo,cantidad, cancellationToken) => await BeUnique(Consumo.PaisId, Consumo.Año, cancellationToken)).WithMessage(c => $"El Consumo (PaisId:{c.PaisId}/Año:{c.Año}) ya existe.");
        }

        public async Task<bool> BeUnique(int paisId, int año, CancellationToken cancellationToken)
        {
            // Si id es 0 trae todos los Consumoes y luego evalúa que todos sean diferentes del nombre
            // Si id es un número, trae todos los Consumoes que no sean ese número y evalúa que todos sean diferentes del nombre
            return await _context.Consumos
                                 .AllAsync(c => c.PaisId != paisId || c.Año != año);
        }
    }
}
