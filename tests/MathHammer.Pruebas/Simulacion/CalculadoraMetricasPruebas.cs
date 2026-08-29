using FluentAssertions;
using MathHammer.Api.Simulacion;

namespace MathHammer.Pruebas.Simulacion;

public class CalculadoraMetricasPruebas
{
    [Fact]
    public void Calcular_MediosSecuenciales_SonLasMediasDeLosConteos()
    {
        ResultadoIteracion[] resultados =
        [
            new ResultadoIteracion(2, 0, 8, 4, 2),
            new ResultadoIteracion(4, 1, 6, 2, 0),
        ];

        var perfil = CrearPerfil(cantidadMiniaturas: 2);

        ResultadoMetricas metricas = CalculadoraMetricas.Calcular(perfil, resultados);

        metricas.ImpactosEsperados.Should().Be(7.0);
        metricas.HeridasEsperadas.Should().Be(3.0);
        metricas.SalvacionesEnemigo.Should().Be(1.0);
    }

    [Fact]
    public void Calcular_DanioMedio_EsLaMediaDeHeridasInfligidas()
    {
        ResultadoIteracion[] resultados =
        [
            new ResultadoIteracion(2, 0, 4, 2, 0),
            new ResultadoIteracion(4, 0, 4, 2, 0),
            new ResultadoIteracion(6, 0, 4, 2, 0),
        ];

        var perfil = CrearPerfil(cantidadMiniaturas: 3);

        ResultadoMetricas metricas = CalculadoraMetricas.Calcular(perfil, resultados);

        metricas.DanioMedioEsperado.Should().Be(4.0);
    }

    [Fact]
    public void Calcular_MiniaturasEliminadas_EsElPromedio()
    {
        ResultadoIteracion[] resultados =
        [
            new ResultadoIteracion(2, 1, 0, 0, 0),
            new ResultadoIteracion(4, 2, 0, 0, 0),
            new ResultadoIteracion(6, 3, 0, 0, 0),
        ];

        var perfil = CrearPerfil(cantidadMiniaturas: 3);

        ResultadoMetricas metricas = CalculadoraMetricas.Calcular(perfil, resultados);

        metricas.MiniaturasEliminadas.Should().Be(2.0);
    }

    [Fact]
    public void Calcular_ProbabilidadMatarUnidad_EsLaProporcionDeAniquilaciones()
    {
        ResultadoIteracion[] resultados =
        [
            new ResultadoIteracion(2, 1, 0, 0, 0),
            new ResultadoIteracion(4, 3, 0, 0, 0),
            new ResultadoIteracion(6, 3, 0, 0, 0),
            new ResultadoIteracion(8, 2, 0, 0, 0),
        ];

        var perfil = CrearPerfil(cantidadMiniaturas: 3);

        ResultadoMetricas metricas = CalculadoraMetricas.Calcular(perfil, resultados);

        metricas.ProbabilidadMatarUnidad.Should().Be(0.5);
    }

    [Fact]
    public void Calcular_Percentiles_UsanInterpolacionLineal()
    {
        ResultadoIteracion[] resultados =
        [
            new ResultadoIteracion(1, 0, 0, 0, 0),
            new ResultadoIteracion(2, 0, 0, 0, 0),
            new ResultadoIteracion(3, 0, 0, 0, 0),
            new ResultadoIteracion(4, 0, 0, 0, 0),
        ];

        var perfil = CrearPerfil(cantidadMiniaturas: 4);

        ResultadoMetricas metricas = CalculadoraMetricas.Calcular(perfil, resultados);

        metricas.Percentil25.Should().BeApproximately(1.75, 1e-9);
        metricas.Percentil75.Should().BeApproximately(3.25, 1e-9);
    }

    [Fact]
    public void Calcular_UnaSolaIteracion_PercentilesIgualesAlValor()
    {
        ResultadoIteracion[] resultados =
        [
            new ResultadoIteracion(5, 1, 0, 0, 0),
        ];

        var perfil = CrearPerfil(cantidadMiniaturas: 1);

        ResultadoMetricas metricas = CalculadoraMetricas.Calcular(perfil, resultados);

        metricas.Percentil25.Should().Be(5.0);
        metricas.Percentil75.Should().Be(5.0);
    }

    [Fact]
    public void Calcular_ResultadosVacios_LanzaExcepcion()
    {
        var perfil = CrearPerfil(cantidadMiniaturas: 1);

        Action accion = () => CalculadoraMetricas.Calcular(perfil, []);

        accion.Should().Throw<ArgumentException>();
    }

    private static PerfilCombate CrearPerfil(int cantidadMiniaturas)
    {
        return new PerfilCombate
        {
            CantidadAtaques = 1,
            ImpactaA = 4,
            Fuerza = 4,
            PenetracionArmadura = 0,
            Danio = 1,
            Resistencia = 4,
            Salvacion = 4,
            SalvacionInvulnerable = null,
            HeridasPorMiniatura = 2,
            CantidadMiniaturas = cantidadMiniaturas,
        };
    }
}