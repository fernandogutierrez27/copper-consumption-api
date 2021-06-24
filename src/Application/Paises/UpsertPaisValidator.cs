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
    public class UpsertPaisValidator : AbstractValidator<Pais>
    {
        private readonly ICopperConsumptionDbContext _context;

        public UpsertPaisValidator(ICopperConsumptionDbContext context)
        {
            _context = context;

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("Nombre is required.")
                .MustAsync(async (pais,nombre, cancellationToken) => await BeUnique(pais.Id, nombre, cancellationToken)).WithMessage(p => $"Pais ({p.Nombre}) already exists.");
        }

        public async Task<bool> BeUnique(int id, string nombre, CancellationToken cancellationToken)
        {
            // Si id es 0 trae todos los paises y luego evalúa que todos sean diferentes del nombre
            // Si id es un número, trae todos los paises que no sean ese número y evalúa que todos sean diferentes del nombre
            return await _context.Paises
                                 .Where(p => id == 0 || p.Id != id)
                                 .AllAsync(p => (p.Nombre != nombre));
        }
    }
}
