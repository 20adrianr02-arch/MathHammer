namespace MathHammer.Api.Reglas;

/// <summary>
/// Calcula la probabilidad de superar una tirada con un dado de seis caras (1D6),
/// considerando que un resultado natural de 1 siempre falla y un 6 natural
/// siempre acierta.
/// </summary>
public static class CalculadoraProbabilidades
{
    private const double CarasDelDado = 6.0;

    /// <summary>
    /// Devuelve la probabilidad de éxito al necesitar alcanzar o superar
    /// <paramref name="objetivo"/> con un 1D6. El resultado se expresa entre
    /// 0.0 y 1.0.
    /// </summary>
    public static double ProbabilidadExito(int objetivo)
    {
        if (objetivo <= 1)
        {
            return 1.0;
        }

        if (objetivo >= 7)
        {
            return 0.0;
        }

        return (7.0 - objetivo) / CarasDelDado;
    }

    /// <summary>
    /// Devuelve la probabilidad de fallo, complementaria a la de éxito.
    /// </summary>
    public static double ProbabilidadFallo(int objetivo)
    {
        return 1.0 - ProbabilidadExito(objetivo);
    }
}
