using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopperConsumption.Domain.Entities
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public IList<Consumo> Consumos { get; set; } = new List<Consumo>();
    }
}
