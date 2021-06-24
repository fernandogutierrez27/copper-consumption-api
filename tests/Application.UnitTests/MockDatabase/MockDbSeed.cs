using CopperConsumption.Domain.Entities;
using System.Collections.Generic;

namespace CopperConsumption.Application.UnitTests.MockDatabase
{
    public static class MockDbSeed
    {
        public static List<Pais> CreateListPaises() => new List<Pais>
        {
            new Pais { Id = 1, Nombre = "Alemania", Consumos = new List<Consumo>(){
                new Consumo { PaisId = 1, Año = 2018, Cantidad = 1386.19M },
                new Consumo { PaisId = 1, Año = 2019, Cantidad = 257.81M },
                new Consumo { PaisId = 1, Año = 2020, Cantidad = 98.03M },
            }},
            new Pais { Id = 2, Nombre = "Arabia Saudita", Consumos = new List<Consumo>(){
                new Consumo { PaisId = 2, Año = 2018, Cantidad = 1072.40M },
                new Consumo { PaisId = 2, Año = 2019, Cantidad = 760.25M },
                new Consumo { PaisId = 2, Año = 2020, Cantidad = 573.5M },
            }},
            new Pais { Id = 3, Nombre = "Argentina", Consumos = new List<Consumo>(){
                new Consumo { PaisId = 3, Año = 2018, Cantidad = 234.34M },
                new Consumo { PaisId = 3, Año = 2019, Cantidad = 65.85M },
                new Consumo { PaisId = 3, Año = 2020, Cantidad = 859.12M },
            }},
            new Pais { Id = 4, Nombre = "Australia", Consumos = new List<Consumo>(){
                new Consumo { PaisId = 4, Año = 2018, Cantidad = 121.1M },
                new Consumo { PaisId = 4, Año = 2019, Cantidad = 1276.47M },
                new Consumo { PaisId = 4, Año = 2020, Cantidad = 12.34M },
            }},
            new Pais { Id = 5, Nombre = "Austria", Consumos = new List<Consumo>(){
                new Consumo { PaisId = 5, Año = 2018, Cantidad = 24.2M },
                new Consumo { PaisId = 5, Año = 2019, Cantidad = 34.1M },
                new Consumo { PaisId = 5, Año = 2020, Cantidad = 67.13M },
            }},
            new Pais { Id = 6, Nombre = "Dinamarca", Consumos = new List<Consumo>(){ } }
        };

        public static List<Consumo> CreateListConsumos() => new List<Consumo>
        {
            new Consumo { PaisId = 1, Año = 2018, Cantidad = 1386.19M, Pais = new Pais { Id = 1, Nombre = "Alemania"} },
            new Consumo { PaisId = 2, Año = 2018, Cantidad = 1072.40M, Pais = new Pais { Id = 2, Nombre = "Arabia Saudita"} },
            new Consumo { PaisId = 3, Año = 2018, Cantidad = 234.34M, Pais = new Pais { Id = 3, Nombre = "Argentina"} },
            new Consumo { PaisId = 4, Año = 2018, Cantidad = 121.1M, Pais = new Pais { Id = 4, Nombre = "Australia" } },
            new Consumo { PaisId = 5, Año = 2018, Cantidad = 24.2M, Pais = new Pais { Id = 5, Nombre = "Austria"} },
            new Consumo { PaisId = 1, Año = 2019, Cantidad = 257.81M, Pais = new Pais { Id = 1, Nombre = "Alemania"} },
            new Consumo { PaisId = 2, Año = 2019, Cantidad = 760.25M, Pais = new Pais { Id = 2, Nombre = "Arabia Saudita"} },
            new Consumo { PaisId = 3, Año = 2019, Cantidad = 65.85M, Pais = new Pais { Id = 3, Nombre = "Argentina"} },
            new Consumo { PaisId = 4, Año = 2019, Cantidad = 1276.47M, Pais = new Pais { Id = 4, Nombre = "Australia" } },
            new Consumo { PaisId = 5, Año = 2019, Cantidad = 34.1M, Pais = new Pais { Id = 5, Nombre = "Austria"} },
            new Consumo { PaisId = 1, Año = 2020, Cantidad = 98.03M, Pais = new Pais { Id = 1, Nombre = "Alemania"} },
            new Consumo { PaisId = 2, Año = 2020, Cantidad = 573.5M, Pais = new Pais { Id = 2, Nombre = "Arabia Saudita"} },
            new Consumo { PaisId = 3, Año = 2020, Cantidad = 859.12M, Pais = new Pais { Id = 3, Nombre = "Argentina"} },
            new Consumo { PaisId = 4, Año = 2020, Cantidad = 12.34M, Pais = new Pais { Id = 4, Nombre = "Australia" } },
            new Consumo { PaisId = 5, Año = 2020, Cantidad = 67.13M, Pais = new Pais { Id = 5, Nombre = "Austria"} },
        };
    }
    
}