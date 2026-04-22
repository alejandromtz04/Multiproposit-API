using ApiLogin.Interfaces;
using ApiLogin.Models.General;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Endpoints.Paradas
{
    public static class ParadasAutorizadasEndpoint
    {
        public static void Map(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/bus").WithTags("Paradas Autorizadas");

            // 1. Agregamos 'async' justo antes de definir los parámetros de la función
            group.MapGet("/", async ([FromServices] IParadaAutorizadaService service, [FromQuery] int? id_parada_autorizada) =>
            {
                if (id_parada_autorizada.HasValue && id_parada_autorizada <= 0)
                {
                    return Results.BadRequest(new { mensaje = "El id_parada_autorizada debe ser un número positivo." });
                }

                try
                {
                    // 2. Agregamos 'await' antes de llamar al servicio
                    var datos = await service.ObtenerParadasAutorizadas(id_parada_autorizada);

                    if (id_parada_autorizada.HasValue && !datos.Any())
                        return Results.NotFound(new { mensaje = $"No se encontró una parada con ID = {id_parada_autorizada}." });

                    return Results.Ok(datos);
                }
                catch (Exception ex)
                {
                    return Results.Problem("Error al consultar la base de datos: " + ex.Message);
                }
            })
            .Produces<List<ParadasAutorizadas>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operacion =>
            {
                operacion.Summary = "Obtener paradas autorizadas";
                return operacion;
            });
        }
    }
}