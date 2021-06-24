using AutoMapper;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Domain.Entities;

namespace CopperConsumption.Application.Consumos
{
    public class ConsumoCommand : IMapFrom<Consumo>
    {
        public int PaisId { get; set; }
        public int Año { get; set; }
        public decimal Cantidad { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ConsumoCommand, Consumo>();
        }
    }

}
