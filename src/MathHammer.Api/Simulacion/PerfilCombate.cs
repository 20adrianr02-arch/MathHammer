namespace MathHammer.Api.Simulacion;

/// <summary>
/// Perfil completo de combate: atributos base y habilidades ofensivas y
/// defensivas de la unidad y del arma.
/// </summary>
public record PerfilCombate
{
    public int CantidadAtaques { get; init; }
    public int ImpactaA { get; init; }
    public int Fuerza { get; init; }
    public int PenetracionArmadura { get; init; }
    public int Danio { get; init; }
    public int Resistencia { get; init; }
    public int Salvacion { get; init; }
    public int? SalvacionInvulnerable { get; init; }
    public int HeridasPorMiniatura { get; init; }
    public int CantidadMiniaturas { get; init; }

    // Habilidades ofensivas
    public bool Lanza { get; init; }
    public int GolpesSostenidos { get; init; }
    public bool GolpesLetales { get; init; }
    public bool HeridasDevastadoras { get; init; }
    public bool RepetirTiradaHerida { get; init; }
    public bool RepiteParaImpactar { get; init; }
    public bool RepiteUnoParaHerir { get; init; }

    // Habilidades defensivas
    public int? SensacionDolor { get; init; }
    public bool ReduccionDanio { get; init; }
    public bool PenalizacionImpactar { get; init; }
    public bool PenalizacionHerir { get; init; }
}