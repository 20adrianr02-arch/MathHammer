namespace MathHammer.Api.Reglas;

/// <summary>
/// Determina la tirada requerida para herir comparando la Fuerza del arma con
/// la Resistencia del objetivo, según la tabla de herida de la 10.ª edición.
/// </summary>
public static class ReglaHerida
{
    /// <summary>
    /// Devuelve la tirada requerida para herir (2, 3, 4, 5 o 6) según la
    /// comparación entre Fuerza y Resistencia.
    /// </summary>
    public static int TiradaHeridaRequerida(int fuerza, int resistencia)
    {
        if (fuerza <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(fuerza), "La fuerza debe ser mayor que 0.");
        }

        if (resistencia <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(resistencia), "La resistencia debe ser mayor que 0.");
        }

        if (fuerza >= 2 * resistencia)
        {
            return 2;
        }

        if (fuerza > resistencia)
        {
            return 3;
        }

        if (fuerza == resistencia)
        {
            return 4;
        }

        if (2 * fuerza > resistencia)
        {
            return 5;
        }

        return 6;
    }
}