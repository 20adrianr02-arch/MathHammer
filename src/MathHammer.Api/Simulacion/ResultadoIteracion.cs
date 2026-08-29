namespace MathHammer.Api.Simulacion;

/// <summary>
/// Resultado de una iteración de combate: heridas aplicadas, miniaturas
/// destruidas y los conteos de impactos, heridas y salvaciones logrados.
/// </summary>
public record ResultadoIteracion(
    int HeridasInfligidas,
    int MiniaturasDestruidas,
    int ImpactosLogrados,
    int HeridasLogradas,
    int SalvacionesLogradas);