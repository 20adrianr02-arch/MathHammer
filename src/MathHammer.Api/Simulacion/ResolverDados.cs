using MathHammer.Api.Contratos;

namespace MathHammer.Api.Simulacion;

/// <summary>
/// Resuelve una expresión de dados aleatorios (<see cref="DadosAleatorios"/>) en
/// un valor concreto: lanza la cantidad de dados indicada, suma el modificador y
/// garantiza un resultado mínimo de 1.
/// </summary>
public static class ResolverDados
{
    /// <summary>
    /// Devuelve el total de lanzar <paramref name="dados"/> dados con el generador
    /// indicado, acotado a un mínimo de 1.
    /// </summary>
    public static int Resolver(DadosAleatorios dados, GeneradorAleatorio generador)
    {
        int total = dados.Modificador;

        for (int dado = 0; dado < dados.CantidadDados; dado++)
        {
            total += generador.LanzarDado(dados.Caras);
        }

        return Math.Max(1, total);
    }
}
