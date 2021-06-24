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
    public class PaisValidator : AbstractValidator<Pais>
    {
        private readonly ICopperConsumptionDbContext _context;

        public PaisValidator(ICopperConsumptionDbContext context)
        {
            _context = context;

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("Nombre is required.")
                .MustAsync(BeUnique).WithMessage(p => $"Pais ({p.Nombre}) already exists.");
        }

        public async Task<bool> BeUnique(string nombre, CancellationToken cancellationToken)
        {
            return await _context.Paises
                                 .AllAsync(p => p.Nombre != nombre);
        }
    }
}
