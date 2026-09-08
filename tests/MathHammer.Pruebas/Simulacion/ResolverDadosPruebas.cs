using FluentAssertions;
using MathHammer.Api.Contratos;
using MathHammer.Api.Simulacion;

namespace MathHammer.Pruebas.Simulacion;

public class ResolverDadosPruebas
{
    [Fact]
    public void Resolver_D6MasUno_ConvergeALaMedia()
    {
        var generador = new GeneradorAleatorio(101);
        var dados = new DadosAleatorios(1, 6, 1);

        double media = Promediar(100000, generador, dados);

        media.Should().BeApproximately(4.5, 0.1);
    }

    [Fact]
    public void Resolver_DosD3_ConvergeALaMedia()
    {
        var generador = new GeneradorAleatorio(202);
        var dados = new DadosAleatorios(2, 3, 0);

        double media = Promediar(100000, generador, dados);

        media.Should().BeApproximately(4.0, 0.1);
    }

    [Fact]
    public void Resolver_ConModificadorNegativo_RespetaElMinimoDeUno()
    {
        var generador = new GeneradorAleatorio(303);
        var dados = new DadosAleatorios(1, 6, -3);

        for (int indice = 0; indice < 10000; indice++)
        {
            ResolverDados.Resolver(dados, generador).Should().BeGreaterThanOrEqualTo(1);
        }
    }

    [Fact]
    public void Resolver_MismaSemilla_ProduceMismosResultados()
    {
        var dados = new DadosAleatorios(2, 6, 1);

        int primera = ResolverDados.Resolver(dados, new GeneradorAleatorio(404));
        int segunda = ResolverDados.Resolver(dados, new GeneradorAleatorio(404));

        primera.Should().Be(segunda);
    }

    private static double Promediar(int muestras, GeneradorAleatorio generador, DadosAleatorios dados)
    {
        double total = 0.0;
        for (int indice = 0; indice < muestras; indice++)
        {
            total += ResolverDados.Resolver(dados, generador);
        }

        return total / muestras;
    }
}
