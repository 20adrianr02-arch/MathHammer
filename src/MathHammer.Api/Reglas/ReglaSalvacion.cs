namespace MathHammer.Api.Reglas;

/// <summary>
/// Determina la tirada de salvación efectiva considerando la salvación de
/// armadura, la penetración de armadura (AP) y la salvación invulnerable.
/// </summary>
public static class ReglaSalvacion
{
    /// <summary>
    /// Valor que representa que no existe salvación posible (7+).
    /// </summary>
    public const int SinSalvacion = 7;

    /// <summary>
    /// Devuelve la tirada de salvación requerida (2 a 7) usando la mejor opción
    /// entre armadura e invulnerable, acotada al rango [2, 7].
    /// </summary>
    public static int TiradaSalvacionRequerida(int salvacion, int penetracionArmadura, int? salvacionInvulnerable)
    {
        ValidarSalvacion(salvacion, salvacionInvulnerable);

        int armaduraRequerida = salvacion - penetracionArmadura;
        int requerida = armaduraRequerida;

        if (salvacionInvulnerable is int invulnerable && invulnerable < requerida)
        {
            requerida = invulnerable;
        }

        if (requerida < 2)
        {
            requerida = 2;
        }

        if (requerida > SinSalvacion)
        {
            requerida = SinSalvacion;
        }

        return requerida;
    }

    private static void ValidarSalvacion(int salvacion, int? salvacionInvulnerable)
    {
        if (salvacion < 2 || salvacion > 6)
        {
            throw new ArgumentOutOfRangeException(nameof(salvacion), "La salvación debe estar entre 2 y 6.");
        }

        if (salvacionInvulnerable is int invulnerable && (invulnerable < 2 || invulnerable > 6))
        {
            throw new ArgumentOutOfRangeException(nameof(salvacionInvulnerable), "La salvación invulnerable debe estar entre 2 y 6.");
        }
    }
}