namespace MathHammer.Api.Reglas;

/// <summary>
/// Calcula magnitudes de la distribución binomial: éxitos esperados,
/// probabilidad exacta y probabilidad acumulada de obtener un mínimo de éxitos
/// al lanzar un número de dados independientes.
/// </summary>
public static class DistribucionBinomial
{
    /// <summary>
    /// Devuelve el número esperado de éxitos: cantidad de dados por probabilidad
    /// de éxito individual.
    /// </summary>
    public static double ExitosEsperados(int cantidadDados, double probabilidadExito)
    {
        ValidarProbabilidad(cantidadDados, probabilidadExito);
        return cantidadDados * probabilidadExito;
    }

    /// <summary>
    /// Devuelve la probabilidad de obtener al menos <paramref name="minimoExitos"/>
    /// éxitos al lanzar <paramref name="cantidadDados"/> dados.
    /// </summary>
    public static double ProbabilidadAlMenos(int cantidadDados, int minimoExitos, double probabilidadExito)
    {
        ValidarProbabilidad(cantidadDados, probabilidadExito);

        if (minimoExitos <= 0)
        {
            return 1.0;
        }

        if (minimoExitos > cantidadDados)
        {
            return 0.0;
        }

        double acumulada = 0.0;
        for (int exitos = minimoExitos; exitos <= cantidadDados; exitos++)
        {
            acumulada += ProbabilidadExacta(cantidadDados, exitos, probabilidadExito);
        }

        return acumulada;
    }

    /// <summary>
    /// Devuelve la probabilidad de obtener exactamente <paramref name="exitos"/>
    /// éxitos al lanzar <paramref name="cantidadDados"/> dados.
    /// </summary>
    public static double ProbabilidadExacta(int cantidadDados, int exitos, double probabilidadExito)
    {
        ValidarProbabilidad(cantidadDados, probabilidadExito);

        if (exitos < 0 || exitos > cantidadDados)
        {
            return 0.0;
        }

        double combinaciones = CoeficienteBinomial(cantidadDados, exitos);
        double exito = Math.Pow(probabilidadExito, exitos);
        double fracaso = Math.Pow(1.0 - probabilidadExito, cantidadDados - exitos);

        return combinaciones * exito * fracaso;
    }

    /// <summary>
    /// Devuelve el coeficiente binomial C(total, elegidos) expresado como valor
    /// real para evitar desbordamientos con cantidades elevadas.
    /// </summary>
    public static double CoeficienteBinomial(int total, int elegidos)
    {
        if (elegidos < 0 || elegidos > total)
        {
            return 0.0;
        }

        if (elegidos == 0 || elegidos == total)
        {
            return 1.0;
        }

        int menor = Math.Min(elegidos, total - elegidos);
        double resultado = 1.0;
        for (int indice = 1; indice <= menor; indice++)
        {
            resultado = resultado * (total - menor + indice) / indice;
        }

        return resultado;
    }

    private static void ValidarProbabilidad(int cantidadDados, double probabilidadExito)
    {
        if (cantidadDados < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cantidadDados), "La cantidad de dados no puede ser negativa.");
        }

        if (probabilidadExito < 0.0 || probabilidadExito > 1.0)
        {
            throw new ArgumentOutOfRangeException(nameof(probabilidadExito), "La probabilidad debe estar entre 0.0 y 1.0.");
        }
    }
}
