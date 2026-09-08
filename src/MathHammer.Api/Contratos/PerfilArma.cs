namespace MathHammer.Api.Contratos;

/// <summary>
/// Perfil del arma: atributos base y habilidades universales.
/// </summary>
public record PerfilArma(
    int CantidadAtaques,
    int Fuerza,
    int PenetracionArmadura,
    int Danio,
    bool RepetirTiradaHerida,
    HabilidadesArma Habilidades);