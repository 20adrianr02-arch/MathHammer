namespace MathHammer.Api.Contratos;

/// <summary>
/// Petición de simulación de combate con un único perfil de arma contra una
/// unidad defensora.
/// </summary>
public record PeticionCombate(
    PerfilAtacante Atacante,
    PerfilArma Arma,
    PerfilDefensor Defensor,
    ConfiguracionSimulacion ConfiguracionSimulacion);