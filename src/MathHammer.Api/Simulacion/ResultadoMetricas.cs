namespace MathHammer.Api.Simulacion;

/// <summary>
/// Resultado de una simulación con las métricas mostradas en el panel de
/// resultados: medios secuenciales analíticos y métricas derivadas del Monte
/// Carlo (daño, miniaturas, probabilidad de aniquilación y percentiles).
/// </summary>
public record ResultadoMetricas(
    double ImpactosEsperados,
    double HeridasEsperadas,
    double SalvacionesEnemigo,
    double ProbabilidadMatarUnidad,
    double MiniaturasEliminadas,
    double DanioMedioEsperado,
    double Percentil25,
    double Percentil75);