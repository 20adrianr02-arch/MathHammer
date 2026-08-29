namespace MathHammer.Api.Simulacion;

/// <summary>
/// Genera números aleatorios para las tiradas de la simulación. Permite fijar
/// una semilla para que una simulación sea reproducible.
/// </summary>
public sealed class GeneradorAleatorio
{
    private readonly Random _aleatorio;

    public GeneradorAleatorio()
    {
        _aleatorio = new Random();
    }

    public GeneradorAleatorio(int semilla)
    {
        _aleatorio = new Random(semilla);
    }

    /// <summary>
    /// Devuelve un resultado entre 1 y <paramref name="caras"/> inclusive.
    /// </summary>
    public int LanzarDado(int caras)
    {
        return _aleatorio.Next(1, caras + 1);
    }

    /// <summary>
    /// Devuelve el resultado de un dado de seis caras (1D6).
    /// </summary>
    public int LanzarD6()
    {
        return LanzarDado(6);
    }
}