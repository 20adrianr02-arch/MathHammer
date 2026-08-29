using FluentAssertions;
using MathHammer.Api.Contratos;

namespace MathHammer.Pruebas.Contratos;

public class ValidadorPeticionPruebas
{
    [Fact]
    public void ObtenerErrores_ConPeticionValida_NoDevuelveErrores()
    {
        PeticionCombate peticion = CrearPeticion();

        ValidadorPeticion.ObtenerErrores(peticion).Should().BeEmpty();
    }

    [Fact]
    public void ObtenerErrores_ConImpactaAInvalido_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Atacante = CrearPeticion().Atacante with { ImpactaA = 7 },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*impactaA*");
    }

    [Fact]
    public void ObtenerErrores_ConResistenciaFueraDeRango_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Defensor = CrearPeticion().Defensor with { Resistencia = 21 },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*resistencia*");
    }

    [Fact]
    public void ObtenerErrores_ConSalvacionInvalida_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Defensor = CrearPeticion().Defensor with { Salvacion = 7 },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*salvacion*");
    }

    [Fact]
    public void ObtenerErrores_ConSensacionDolorInvalida_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Defensor = CrearPeticion().Defensor with { SensacionDolor = 2 },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*sensacionDolor*");
    }

    [Fact]
    public void ObtenerErrores_ConIteracionesFueraDeRango_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            ConfiguracionSimulacion = CrearPeticion().ConfiguracionSimulacion with { Iteraciones = 0 },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*iteraciones*");
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