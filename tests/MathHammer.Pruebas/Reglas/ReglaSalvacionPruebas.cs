using FluentAssertions;
using MathHammer.Api.Reglas;

namespace MathHammer.Pruebas.Reglas;

public class ReglaSalvacionPruebas
{
    [Theory]
    [InlineData(3, -2, null, 5)]
    [InlineData(4, 0, null, 4)]
    [InlineData(3, -1, null, 4)]
    [InlineData(2, 0, null, 2)]
    [InlineData(3, -3, 4, 4)]
    [InlineData(2, -1, 5, 3)]
    [InlineData(4, -4, null, 7)]
    [InlineData(6, -5, null, 7)]
    public void TiradaSalvacionRequerida_UsaLaMejorOpcionYAcota(int salvacion, int ap, int? invulnerable, int esperada)
    {
        ReglaSalvacion.TiradaSalvacionRequerida(salvacion, ap, invulnerable).Should().Be(esperada);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    public void TiradaSalvacionRequerida_SalvacionInvalida_LanzaExcepcion(int salvacion)
    {
        Action accion = () => ReglaSalvacion.TiradaSalvacionRequerida(salvacion, 0, null);

        accion.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    public void TiradaSalvacionRequerida_InvulnerableInvalida_LanzaExcepcion(int invulnerable)
    {
        Action accion = () => ReglaSalvacion.TiradaSalvacionRequerida(3, 0, invulnerable);

        accion.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void TiradaSalvacionRequerida_ConstanteSinSalvacion_EsSiete()
    {
        ReglaSalvacion.SinSalvacion.Should().Be(7);
    }
}