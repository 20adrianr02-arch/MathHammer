using FluentAssertions;
using MathHammer.Api.Reglas;

namespace MathHammer.Pruebas.Reglas;

public class ReglaHeridaPruebas
{
    [Theory]
    [InlineData(8, 4, 2)]
    [InlineData(10, 5, 2)]
    [InlineData(4, 2, 2)]
    [InlineData(5, 4, 3)]
    [InlineData(6, 4, 3)]
    [InlineData(4, 4, 4)]
    [InlineData(3, 4, 5)]
    [InlineData(4, 5, 5)]
    [InlineData(2, 4, 6)]
    [InlineData(3, 6, 6)]
    public void TiradaHeridaRequerida_SegunComparacion_DevuelveTiradaCorrecta(int fuerza, int resistencia, int esperada)
    {
        ReglaHerida.TiradaHeridaRequerida(fuerza, resistencia).Should().Be(esperada);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void TiradaHeridaRequerida_FuerzaInvalida_LanzaExcepcion(int fuerza)
    {
        Action accion = () => ReglaHerida.TiradaHeridaRequerida(fuerza, 4);

        accion.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TiradaHeridaRequerida_ResistenciaInvalida_LanzaExcepcion(int resistencia)
    {
        Action accion = () => ReglaHerida.TiradaHeridaRequerida(5, resistencia);

        accion.Should().Throw<ArgumentOutOfRangeException>();
    }
}