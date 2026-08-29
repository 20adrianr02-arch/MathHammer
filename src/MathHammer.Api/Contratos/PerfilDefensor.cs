namespace MathHammer.Api.Contratos;

/// <summary>
/// Perfil del defensor: atributos base y habilidades defensivas.
/// </summary>
public record PerfilDefensor(
    string NombreUnidad,
    int Resistencia,
    int Salvacion,
    int? SalvacionInvulnerable,
    int? SensacionDolor,
    bool ReduccionDanio,
    bool PenalizacionImpactar,
    bool PenalizacionHerir,
    int HeridasPorMiniatura,
    int CantidadMiniaturas);