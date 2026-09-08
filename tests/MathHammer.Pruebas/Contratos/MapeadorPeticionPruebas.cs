using FluentAssertions;
using MathHammer.Api.Contratos;
using MathHammer.Api.Simulacion;

namespace MathHammer.Pruebas.Contratos;

public class MapeadorPeticionPruebas
{
    [Fact]
    public void MapearPerfil_ConPeticionValida_CreaElPerfilCorrecto()
    {
        PeticionCombate peticion = CrearPeticion();

        PerfilCombate perfil = MapeadorPeticion.MapearPerfil(peticion);

        perfil.CantidadAtaques.Should().Be(8);
        perfil.ImpactaA.Should().Be(3);
        perfil.Fuerza.Should().Be(5);
        perfil.PenetracionArmadura.Should().Be(-2);
        perfil.Danio.Should().Be(1);
        perfil.Resistencia.Should().Be(4);
        perfil.Salvacion.Should().Be(3);
        perfil.SalvacionInvulnerable.Should().BeNull();
        perfil.HeridasPorMiniatura.Should().Be(2);
        perfil.CantidadMiniaturas.Should().Be(5);
    }

    [Fact]
    public void MapearPerfil_ConHabilidades_LasIncluye()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Atacante = CrearPeticion().Atacante with { RepiteParaImpactar = true, RepiteUnoParaHerir = true },
            Arma = CrearPeticion().Arma with
            {
                RepetirTiradaHerida = true,
                Habilidades = new HabilidadesArma(true, 2, true, true),
            },
            Defensor = CrearPeticion().Defensor with
            {
                SensacionDolor = 6,
                ReduccionDanio = true,
                PenalizacionImpactar = true,
                PenalizacionHerir = true,
            },
        };

        PerfilCombate perfil = MapeadorPeticion.MapearPerfil(peticion);

        perfil.RepiteParaImpactar.Should().BeTrue();
        perfil.RepiteUnoParaHerir.Should().BeTrue();
        perfil.RepetirTiradaHerida.Should().BeTrue();
        perfil.Lanza.Should().BeTrue();
        perfil.GolpesSostenidos.Should().Be(2);
        perfil.GolpesLetales.Should().BeTrue();
        perfil.HeridasDevastadoras.Should().BeTrue();
        perfil.SensacionDolor.Should().Be(6);
        perfil.ReduccionDanio.Should().BeTrue();
        perfil.PenalizacionImpactar.Should().BeTrue();
        perfil.PenalizacionHerir.Should().BeTrue();
    }

    private static PeticionCombate CrearPeticion()
    {
        return new PeticionCombate(
            new PerfilAtacante("Escuadra intercesora", 3, false, false),
            new PerfilArma(8, 5, -2, 1, false, new HabilidadesArma(false, 0, false, false)),
            new PerfilDefensor("Guerreros Necrones", 4, 3, null, null, false, false, false, 2, 5),
            new ConfiguracionSimulacion(10000, null));
    }
}