using CopperConsumption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopperConsumption.Infrastructure.Persistence.Configurations
{
    public class PaisConfiguration : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais");

            builder.Property(t => t.Nombre)
                .IsRequired();

            builder.HasMany(t => t.Consumos)
                .WithOne(t => t.Pais);
        }
    }
}