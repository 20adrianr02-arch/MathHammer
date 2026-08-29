using FluentAssertions;
using MathHammer.Api.Simulacion;

namespace MathHammer.Pruebas.Simulacion;

public class GeneradorAleatorioPruebas
{
    [Fact]
    public void MismaSemilla_GeneraLaMismaSecuencia()
    {
        var primerGenerador = new GeneradorAleatorio(42);
        var segundoGenerador = new GeneradorAleatorio(42);

        int[] primeraSecuencia = LanzarSecuencia(primerGenerador, 20);
        int[] segundaSecuencia = LanzarSecuencia(segundoGenerador, 20);

        primeraSecuencia.Should().Equal(segundaSecuencia);
    }

    [Fact]
    public void LanzarD6_SiempreDevuelveEntreUnoYSeis()
    {
        var generador = new GeneradorAleatorio(123);

        int minimo = 7;
        int maximo = 0;
        for (int indice = 0; indice < 10000; indice++)
        {
            int resultado = generador.LanzarD6();
            resultado.Should().BeInRange(1, 6);
            minimo = Math.Min(minimo, resultado);
            maximo = Math.Max(maximo, resultado);
        }

        minimo.Should().Be(1);
        maximo.Should().Be(6);
    }

    [Fact]
    public void LanzarDado_ConTresCaras_DevuelveEntreUnoYTres()
    {
        var generador = new GeneradorAleatorio(7);

        for (int indice = 0; indice < 5000; indice++)
        {
            generador.LanzarDado(3).Should().BeInRange(1, 3);
        }
    }

    private static int[] LanzarSecuencia(GeneradorAleatorio generador, int cantidad)
    {
        var resultados = new int[cantidad];
        for (int indice = 0; indice < cantidad; indice++)
        {
            resultados[indice] = generador.LanzarD6();
        }

        return resultados;
    }
}