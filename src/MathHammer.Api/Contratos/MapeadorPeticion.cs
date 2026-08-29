using MathHammer.Api.Simulacion;

namespace MathHammer.Api.Contratos;

/// <summary>
/// Convierte la petición de combate en el perfil completo que usa el simulador,
/// incluyendo las habilidades ofensivas y defensivas.
/// </summary>
public static class MapeadorPeticion
{
    /// <summary>
    /// Crea el perfil del simulador a partir de la petición.
    /// </summary>
    public static PerfilCombate MapearPerfil(PeticionCombate peticion)
    {
        return new PerfilCombate
        {
            CantidadAtaques = peticion.Arma.CantidadAtaques,
            ImpactaA = peticion.Atacante.ImpactaA,
            Fuerza = peticion.Arma.Fuerza,
            PenetracionArmadura = peticion.Arma.PenetracionArmadura,
            Danio = peticion.Arma.Danio,
            Resistencia = peticion.Defensor.Resistencia,
            Salvacion = peticion.Defensor.Salvacion,
            SalvacionInvulnerable = peticion.Defensor.SalvacionInvulnerable,
            HeridasPorMiniatura = peticion.Defensor.HeridasPorMiniatura,
            CantidadMiniaturas = peticion.Defensor.CantidadMiniaturas,

            Lanza = peticion.Arma.Habilidades.Lanza,
            GolpesSostenidos = peticion.Arma.Habilidades.GolpesSostenidos,
            GolpesLetales = peticion.Arma.Habilidades.GolpesLetales,
            HeridasDevastadoras = peticion.Arma.Habilidades.HeridasDevastadoras,
            RepetirTiradaHerida = peticion.Arma.RepetirTiradaHerida,
            RepiteParaImpactar = peticion.Atacante.RepiteParaImpactar,
            RepiteUnoParaHerir = peticion.Atacante.RepiteUnoParaHerir,

            SensacionDolor = peticion.Defensor.SensacionDolor,
            ReduccionDanio = peticion.Defensor.ReduccionDanio,
            PenalizacionImpactar = peticion.Defensor.PenalizacionImpactar,
            PenalizacionHerir = peticion.Defensor.PenalizacionHerir,
        };
    }
}