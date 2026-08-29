namespace MathHammer.Api.Contratos;

/// <summary>
/// Resumen de la ejecución de la simulación.
/// </summary>
public record ResumenSimulacion(
    int IteracionesEjecutadas,
    long DuracionMilisegundos);