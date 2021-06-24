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
    public class UpdateConsumoValidator : AbstractValidator<Consumo>
    {
        private readonly ICopperConsumptionDbContext _context;

        public UpdateConsumoValidator(ICopperConsumptionDbContext context)
        {
            _context = context;

            RuleFor(c => c.Cantidad)
                .NotEmpty().WithMessage("Cantidad es requerido.");
        }
    }
}
