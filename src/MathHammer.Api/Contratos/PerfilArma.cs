namespace MathHammer.Api.Contratos;

/// <summary>
/// Perfil del arma: atributos base (fijos o aleatorios) y habilidades universales.
/// </summary>
public record PerfilArma(
    int CantidadAtaques,
    int Fuerza,
    int PenetracionArmadura,
    int Danio,
    bool RepetirTiradaHerida,
    HabilidadesArma Habilidades,
    DadosAleatorios? AtaquesAleatorios = null,
    DadosAleatorios? DanioAleatorio = null);