# CopperConsumption API
[![Build Status](https://dev.azure.com/gosoluciones/copper-consumption-api/_apis/build/status/fernandogutierrez27.copper-consumption-api?branchName=main)](https://dev.azure.com/gosoluciones/copper-consumption-api/_build/latest?definitionId=38&branchName=main)

El presente proyecto tiene por objetivo crear un microservicio que exponga un CRUD a modo de ejemplo.

Dicho microservicio expone datos desde una BD alimentada con datos obtenidos desde registros públicos, el registro en particular corresponden al reporte de [Consumo mundial de cobre](https://datos.gob.cl/dataset/consumo-mundial-de-cobre) expuesto por el Ministería de Minería de Chile en el año 2020.

# Demo
Para ver la ejecución en vivo de la API, junto a su especificación, es posible acceder a la versión [Demo](https://copper-consumption-api.azurewebsites.net/swagger) hospedada en **Azure**.

# Arquitectura de la solución
Entre los elementos a implementar en esta PoC tenemos:
- [x] Creación del servicio con tecnología C# (.NET 5)
- [x] Creación de Swagger o Open API 3.0
- [x] Manejo de Errores a través de filtro de excepciones
- [x] Uso de Docker
- [x] Despliegue automatizado en Azure
- [x] Pruebas Unitarias

La solución se divide en los siguientes componentes, los cuales detallamos a continuación:
## API REST
Se desarrolla proyecto de **API en .NET 5**, considerando una arquitectura de *n-capas*, donde se buscar separar el acceso a la API propiamente tal (**Api**), la capa de negocio donde se realiza todo el procesamiento necesario (**Application**), otra capa con la definición de las entidades del dominio (**Domain**) y finalmente una capa de integración con dependencias externas (**Infratructure**). 

Dicha distribución facilita la ejecución de test unitarios, por ejemplo. En este proyecto se realizan particularmente test unitarios sobre la capa.
 **Application**.

Otro aspecto importante de la API se refiere al manejo de errores, implementando filtros para el manejos de excepciones que posibiliten siempre entregar un mensaje significativo a los consumidores.

Finalmente, la API propiamente tal se documenta a través de la utilización de Swagger, generando la especificación de OpenApi 3.0 correspondiente.

## Contenerización en Docker
La propia solución es contenerizada a través de **Docker**. Procurando su despliegue a través de diferentes tecnologías de orquestación en caso de ser necesario. 

Además, la imagen compilada se encuentra hospedada en nuestro repositorio de [**Docker Hub**](https://hub.docker.com/repository/docker/fgutierrezdocker/copper-consumption-api).

## Integración y Entrega Continua.
La solución ejecuta tareas automatizadas a través de un proceso de **CI/CD**. El flujo comienza cuando se realiza un *push* al código hospedado en **GitHub**, dicha acción desencadena la ejecución de un pipeline proporcionado por **Azure Pipelines**. Dentro de esta instancia se identifican 3 etapas principales:
1. Testing: Se restauran las dependencias, el código es compilado y se ejecutan las **pruebas unitarias**.
2. Build: Una vez aprobada la etapa anterior (validando la ejecución de las pruebas), se procede a compilar la imagen de docker y se realiza la publicación en Docker Hub.
3. Deployment: Finalmente, se realiza el despliegue de la imagen recientemente publicada sobre una instancia de **Azure App Service**. La cual ya se encuentra configurada para conectarse a una BBDD de **Azure SQL**, administrada a través de modalidad PaaS.

# Instalación
## Generación de Database
Es posible generar manualmente la base de datos, ya sea para su utilización local o en la nube, ejecutando el script adjunto en `data/database.sql`

## Ejecución desde docker
- En caso de querer compilar el Dockerfile, se debe ejecutar el siguiente comando desde la carpeta `src`:
```bash
docker build -f Api\Dockerfile --force-rm -t <nombre_repositorio>/<nombre_imagen> .
```
Otra alternativa, es simplemente descargar la imagen desde el repositorio de Docker Hub.
```
docker push fgutierrezdocker/copper-consumption-api
```

Para generar el contenedor a partir de la imagen se deje ejecutar el siguiente comando:
```

```

