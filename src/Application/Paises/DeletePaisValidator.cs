using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CopperConsumption.Application.Common.Interfaces;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CopperConsumption.Application.Paises
{
    public class DeletePaisValidator : AbstractValidator<Pais>
    {
        private readonly ICopperConsumptionDbContext _context;

        public DeletePaisValidator(ICopperConsumptionDbContext context)
        {
            _context = context;

            RuleFor(p => p.Id)
                .MustAsync(SinConsumos).WithMessage(p => $"Pais ({p.Nombre}) contains consumptions.");
        }

        public async Task<bool> SinConsumos(int id, CancellationToken cancellationToken)
        {
            return await _context.Consumos
                                 .AllAsync(c => c.PaisId != id);
        }
    }
}
