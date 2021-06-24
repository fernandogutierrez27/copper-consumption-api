using AutoMapper;
using CopperConsumption.Application.Common.Mappings;
using CopperConsumption.Domain.Entities;

namespace CopperConsumption.Application.Paises
{
    public class PaisDto : IMapFrom<Pais>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pais, PaisDto>();
        }
    }
}
