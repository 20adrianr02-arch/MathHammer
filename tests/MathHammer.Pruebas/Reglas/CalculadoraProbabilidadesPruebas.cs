using FluentAssertions;
using MathHammer.Api.Reglas;

namespace MathHammer.Pruebas.Reglas;

public class CalculadoraProbabilidadesPruebas
{
    [Theory]
    [InlineData(2, 5.0 / 6.0)]
    [InlineData(3, 4.0 / 6.0)]
    [InlineData(4, 3.0 / 6.0)]
    [InlineData(5, 2.0 / 6.0)]
    [InlineData(6, 1.0 / 6.0)]
    public void ProbabilidadExito_ConObjetivoEntreDosYSeis_DevuelveLaFraccion(int objetivo, double esperada)
    {
        CalculadoraProbabilidades.ProbabilidadExito(objetivo).Should().BeApproximately(esperada, 1e-9);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-3)]
    public void ProbabilidadExito_ConObjetivoMenorOIgualAUno_DevuelveCienPorCiento(int objetivo)
    {
        CalculadoraProbabilidades.ProbabilidadExito(objetivo).Should().Be(1.0);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(20)]
    public void ProbabilidadExito_ConObjetivoMayorOIgualASiete_DevuelveCero(int objetivo)
    {
        CalculadoraProbabilidades.ProbabilidadExito(objetivo).Should().Be(0.0);
    }

    [Fact]
    public void ProbabilidadFallo_EsComplementariaAlExito()
    {
        double exito = CalculadoraProbabilidades.ProbabilidadExito(4);
        double fallo = CalculadoraProbabilidades.ProbabilidadFallo(4);

        (exito + fallo).Should().BeApproximately(1.0, 1e-9);
    }
}
