namespace MathHammer.Api.Contratos;

/// <summary>
/// Expresión de dados aleatorios para un atributo (ataques o daño). Representa
/// lanzar <see cref="CantidadDados"/> dados de <see cref="Caras"/> caras y sumar
/// <see cref="Modificador"/> al total.
/// </summary>
public record DadosAleatorios(
    int CantidadDados,
    int Caras,
    int Modificador);
