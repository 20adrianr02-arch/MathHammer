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

    [Fact]
    public void ObtenerErrores_ConSoloAtaquesAleatorios_NoDevuelveErrores()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Arma = CrearPeticion().Arma with { CantidadAtaques = 0, AtaquesAleatorios = new DadosAleatorios(1, 6, 0) },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().BeEmpty();
    }

    [Fact]
    public void ObtenerErrores_ConSoloDanioAleatorio_NoDevuelveErrores()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Arma = CrearPeticion().Arma with { Danio = 0, DanioAleatorio = new DadosAleatorios(2, 3, 1) },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().BeEmpty();
    }

    [Fact]
    public void ObtenerErrores_ConAtaquesFijoYDados_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Arma = CrearPeticion().Arma with { AtaquesAleatorios = new DadosAleatorios(1, 6, 0) },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*ataquesAleatorios*");
    }

    [Fact]
    public void ObtenerErrores_ConDanioFijoYDados_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Arma = CrearPeticion().Arma with { DanioAleatorio = new DadosAleatorios(1, 6, 0) },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*danioAleatorio*");
    }

    [Fact]
    public void ObtenerErrores_ConAtaquesSinFuente_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Arma = CrearPeticion().Arma with { CantidadAtaques = 0 },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*cantidadAtaques*");
    }

    [Fact]
    public void ObtenerErrores_ConDadosCarasInvalidas_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Arma = CrearPeticion().Arma with { CantidadAtaques = 0, AtaquesAleatorios = new DadosAleatorios(1, 8, 0) },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*caras*");
    }

    [Fact]
    public void ObtenerErrores_ConDadosSinCantidad_DevuelveError()
    {
        PeticionCombate peticion = CrearPeticion() with
        {
            Arma = CrearPeticion().Arma with { CantidadAtaques = 0, AtaquesAleatorios = new DadosAleatorios(0, 6, 0) },
        };

        ValidadorPeticion.ObtenerErrores(peticion).Should().ContainMatch("*cantidadDados*");
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