using AutoMapper;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Domain.Entities;

namespace CopperConsumption.Application.Consumos
{
    public class ConsumoDto : IMapFrom<Consumo>
    {
        public int Año { get; set; }
        public int PaisId { get; set; }
        public string NombrePais { get; set; }
        public decimal Cantidad { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Consumo, ConsumoDto>()
                .ForMember(dest =>
                    dest.NombrePais,
                    opt => opt.MapFrom(src => src.Pais.Nombre)
                );
        }
    }
}
