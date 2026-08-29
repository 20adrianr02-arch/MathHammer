namespace MathHammer.Api.Simulacion;

/// <summary>
/// Calcula las métricas del panel de resultados a partir de los resultados de
/// la simulación: medias secuenciales, letalidad y percentiles de daño.
/// </summary>
public static class CalculadoraMetricas
{
    /// <summary>
    /// Calcula las ocho métricas del panel de resultados para el perfil y los
    /// resultados de simulación indicados.
    /// </summary>
    public static ResultadoMetricas Calcular(PerfilCombate perfil, ResultadoIteracion[] resultados)
    {
        ValidarResultados(resultados);

        double impactosEsperados = resultados.Average(r => r.ImpactosLogrados);
        double heridasEsperadas = resultados.Average(r => r.HeridasLogradas);
        double salvacionesEnemigo = resultados.Average(r => r.SalvacionesLogradas);

        double danioPromedio = resultados.Average(r => r.HeridasInfligidas);
        double miniaturasEliminadas = resultados.Average(r => r.MiniaturasDestruidas);
        double probabilidadMatarUnidad = (double)resultados.Count(r => r.MiniaturasDestruidas == perfil.CantidadMiniaturas) / resultados.Length;

        double[] danioOrdenado = resultados
            .Select(r => (double)r.HeridasInfligidas)
            .OrderBy(valor => valor)
            .ToArray();

        double percentil25 = CalcularPercentil(danioOrdenado, 0.25);
        double percentil75 = CalcularPercentil(danioOrdenado, 0.75);

        return new ResultadoMetricas(
            impactosEsperados,
            heridasEsperadas,
            salvacionesEnemigo,
            probabilidadMatarUnidad,
            miniaturasEliminadas,
            danioPromedio,
            percentil25,
            percentil75);
    }

    private static double CalcularPercentil(double[] ordenados, double fraccion)
    {
        if (ordenados.Length == 1)
        {
            return ordenados[0];
        }

        double posicion = fraccion * (ordenados.Length - 1);
        int indiceInferior = (int)Math.Floor(posicion);
        int indiceSuperior = (int)Math.Ceiling(posicion);

        if (indiceInferior == indiceSuperior)
        {
            return ordenados[indiceInferior];
        }

        double factor = posicion - indiceInferior;
        return ordenados[indiceInferior] + (ordenados[indiceSuperior] - ordenados[indiceInferior]) * factor;
    }

    private static void ValidarResultados(ResultadoIteracion[] resultados)
    {
        if (resultados.Length == 0)
        {
            throw new ArgumentException("No hay resultados de simulación para calcular métricas.", nameof(resultados));
        }
    }
}