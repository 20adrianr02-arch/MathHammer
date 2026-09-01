using System.Diagnostics;
using System.Text.Json;
using System.Threading.RateLimiting;
using MathHammer.Api.Contratos;
using MathHammer.Api.Simulacion;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(opciones =>
{
    opciones.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var origenesPermitidos = builder.Configuration.GetSection("Cors:Origenes").Get<string[]>() ?? [];

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("permitirFrontend", politica =>
        politica.WithOrigins(origenesPermitidos)
                .AllowAnyHeader()
                .AllowAnyMethod());
});

builder.Services.AddHealthChecks();

builder.Services.AddRateLimiter(opciones =>
{
    opciones.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    opciones.AddPolicy("simulacion", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "anónimo",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 20,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
            }));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("permitirFrontend");
app.UseRateLimiter();

app.MapHealthChecks("/health");

app.MapGet("/", () => "MathHammer API");

app.MapPost("/api/combate/simular", (PeticionCombate peticion, ILogger<Program> logger) =>
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
        logger.LogError(excepcion, "Error inesperado al simular el combate.");
        return ResultadosError.Inesperado();
    }
}).RequireRateLimiting("simulacion");

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

    public static IResult Inesperado()
    {
        var problema = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Error inesperado del servidor.",
            Detail = "Se ha producido un error interno. Inténtalo de nuevo.",
        };

        return Results.Json(problema, statusCode: problema.Status);
    }
}