using CopperConsumption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopperConsumption.Infrastructure.Persistence.Configurations
{
    public class ConsumoConfiguration : IEntityTypeConfiguration<Consumo>
    {
        public void Configure(EntityTypeBuilder<Consumo> builder)
        {
            builder.ToTable("Consumo");

            builder.HasKey(t => new { t.PaisId, t.Año });

            builder.Property(t => t.Cantidad)
                   .IsRequired();

            builder.HasOne(t => t.Pais)
                   .WithMany(t => t.Consumos);
        }
    }
}