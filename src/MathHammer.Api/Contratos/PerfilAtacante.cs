namespace MathHammer.Api.Contratos;

/// <summary>
/// Perfil del atacante: nombre, habilidad de impacto y repeticiones.
/// </summary>
public record PerfilAtacante(
    string NombreUnidad,
    int ImpactaA,
    bool RepiteParaImpactar,
    bool RepiteUnoParaHerir);