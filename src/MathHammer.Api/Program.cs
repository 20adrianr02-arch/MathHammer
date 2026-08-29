using System.Diagnostics;
using System.Text.Json;
using MathHammer.Api.Contratos;
using MathHammer.Api.Simulacion;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(opciones =>
{
    opciones.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("permitirFrontendLocal", politica =>
        politica.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("permitirFrontendLocal");

app.MapGet("/", () => "MathHammer API");

app.MapPost("/api/combate/simular", (PeticionCombate peticion) =>
{
    IReadOnlyList<string> errores = ValidadorPeticion.ObtenerErrores(peticion);
    if (errores.Count > 0)
    {
        return ResultadosError.Validacion(errores);
    }

    try
    {
        PerfilCombate perfil = MapeadorPeticion.MapearPerfil(peticion);

        var cronometro = Stopwatch.StartNew();
        ResultadoIteracion[] resultados = SimuladorCombate.Simular(
            perfil,
            peticion.ConfiguracionSimulacion.Iteraciones,
            peticion.ConfiguracionSimulacion.SemillaAleatoria);
        cronometro.Stop();

        ResultadoMetricas metricas = CalculadoraMetricas.Calcular(perfil, resultados);
        var resumen = new ResumenSimulacion(resultados.Length, cronometro.ElapsedMilliseconds);

        return Results.Ok(new ResultadoCombate(metricas, resumen));
    }
    catch (Exception excepcion)
    {
        return ResultadosError.Inesperado(excepcion);
    }
});

app.Run();

/// <summary>
/// Construye respuestas de error conforme a RFC 9457 (Problem Details).
/// </summary>
public static class ResultadosError
{
    public static IResult Validacion(IReadOnlyList<string> errores)
    {
        var problema = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "La petición no cumple las reglas de validación.",
            Detail = string.Join(" ", errores),
        };

        return Results.Json(problema, statusCode: problema.Status);
    }

    public static IResult Inesperado(Exception excepcion)
    {
        var problema = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Error inesperado del servidor.",
            Detail = excepcion.Message,
        };

        return Results.Json(problema, statusCode: problema.Status);
    }
}