using MathHammer.Api.Simulacion;

namespace MathHammer.Api.Contratos;

/// <summary>
/// Respuesta de la simulación de combate: métricas del panel de resultados y
/// resumen de la ejecución.
/// </summary>
public record ResultadoCombate(
    ResultadoMetricas Metricas,
    ResumenSimulacion Resumen);