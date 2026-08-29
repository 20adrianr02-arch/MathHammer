using FluentAssertions;
using MathHammer.Api.Reglas;

namespace MathHammer.Pruebas.Reglas;

public class DistribucionBinomialPruebas
{
    [Fact]
    public void ExitosEsperados_EsCantidadPorProbabilidad()
    {
        DistribucionBinomial.ExitosEsperados(20, 0.5).Should().Be(10.0);
        DistribucionBinomial.ExitosEsperados(8, 1.0 / 6.0).Should().BeApproximately(8.0 / 6.0, 1e-9);
    }

    [Fact]
    public void ProbabilidadAlMenos_TodosLosExitos_IgualaAExacta()
    {
        double exacta = DistribucionBinomial.ProbabilidadExacta(10, 10, 1.0 / 3.0);

        DistribucionBinomial.ProbabilidadAlMenos(10, 10, 1.0 / 3.0).Should().BeApproximately(exacta, 1e-9);
    }

    [Fact]
    public void ProbabilidadAlMenos_CeroExitos_DevuelveCienPorCiento()
    {
        DistribucionBinomial.ProbabilidadAlMenos(5, 0, 0.4).Should().Be(1.0);
    }

    [Fact]
    public void ProbabilidadAlMenos_MasExitosQueDados_DevuelveCero()
    {
        DistribucionBinomial.ProbabilidadAlMenos(3, 4, 0.5).Should().Be(0.0);
    }

    [Fact]
    public void ProbabilidadAlMenos_CasoConocido_DosOMasDeCincoConMitad()
    {
        // Con 5 dados y probabilidad 0.5, P(X >= 2) es 0.8125.
        DistribucionBinomial.ProbabilidadAlMenos(5, 2, 0.5).Should().BeApproximately(0.8125, 1e-9);
    }

    [Fact]
    public void CoeficienteBinomial_ValoresConocidos()
    {
        DistribucionBinomial.CoeficienteBinomial(5, 2).Should().Be(10.0);
        DistribucionBinomial.CoeficienteBinomial(5, 0).Should().Be(1.0);
        DistribucionBinomial.CoeficienteBinomial(5, 5).Should().Be(1.0);
        DistribucionBinomial.CoeficienteBinomial(5, 6).Should().Be(0.0);
    }

    [Fact]
    public void ProbabilidadExacta_LaSumaDeTodosLosResultados_EsUno()
    {
        double suma = 0.0;
        for (int exitos = 0; exitos <= 6; exitos++)
        {
            suma += DistribucionBinomial.ProbabilidadExacta(6, exitos, 0.3);
        }

        suma.Should().BeApproximately(1.0, 1e-9);
    }
}
