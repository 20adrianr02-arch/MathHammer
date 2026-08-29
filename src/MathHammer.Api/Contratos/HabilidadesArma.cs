namespace MathHammer.Api.Contratos;

/// <summary>
/// Habilidades universales del arma. En el módulo 5 se aceptan pero todavía no
/// se aplican en la simulación; se activarán en el módulo 6.
/// </summary>
public record HabilidadesArma(
    bool Lanza,
    int GolpesSostenidos,
    bool GolpesLetales,
    bool HeridasDevastadoras);