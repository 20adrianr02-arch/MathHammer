namespace MathHammer.Api.Contratos;

/// <summary>
/// Configuración de la simulación Monte Carlo.
/// </summary>
public record ConfiguracionSimulacion(
    int Iteraciones,
    int? SemillaAleatoria);