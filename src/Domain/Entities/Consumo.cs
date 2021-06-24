using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopperConsumption.Domain.Entities
{
    public class Consumo
    {
        public int Año { get; set; }
        public int PaisId { get; set; }
        public decimal Cantidad { get; set; }

        public Pais Pais { get; set; }
    }
}
