# CopperConsumption API
[![Build Status](https://dev.azure.com/gosoluciones/copper-consumption-api/_apis/build/status/fernandogutierrez27.copper-consumption-api?branchName=main)](https://dev.azure.com/gosoluciones/copper-consumption-api/_build/latest?definitionId=38&branchName=main)

El presente proyecto tiene por objetivo crear un microservicio que exponga un CRUD a modo de ejemplo.

Dicho microservicio expone datos desde una BD alimentada con datos obtenidos desde registros públicos, el registro en particular corresponden al reporte de [Consumo mundial de cobre](https://datos.gob.cl/dataset/consumo-mundial-de-cobre) expuesto por el Ministería de Minería de Chile en el año 2020.

Entre los elementos a implementar en esta PoC tenemos:
- [x] Creación del servicio con tecnología C# (.NET 5)
- [x] Creación de Swagger o Open API 3.0
- [x] Manejo de Errores a través de filtro de excepciones
- [ ] Uso de Docker
- [ ] Despliegue automatizado en Azure
- [ ] Pruebas Unitarias

## Generación de Database
Es posible generar manualmente la base de datos a utilizar, ejecutando el script adjunto en la data/database.sql

## Ejecución desde docker
- En caso de querer compilar el Dockerfile, se debe ejecutar el siguiente comando desde la carpeta `src`:
```bash
docker build -f Api\Dockerfile --force-rm -t <nombre_repositorio>/<nombre_imagen> .
```

Para correr el contenedor se deje ejecutar el siguiente comando:
```

```
TODO: Definir comando de ejecución desde docker

## Despliegue automatizado
TODO: Comentar pipeline de azure (pruebas unitarias y deployment)



