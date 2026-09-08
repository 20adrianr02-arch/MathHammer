namespace MathHammer.Api.Contratos;

/// <summary>
/// Valida una petición de combate de forma temprana (fail-fast) y devuelve la
/// lista de errores encontrados.
/// </summary>
public static class ValidadorPeticion
{
    /// <summary>
    /// Devuelve los errores de validación de la petición. Si no hay errores,
    /// la lista está vacía.
    /// </summary>
    public static IReadOnlyList<string> ObtenerErrores(PeticionCombate peticion)
    {
        var errores = new List<string>();

        ValidarAtacante(peticion.Atacante, errores);
        ValidarArma(peticion.Arma, errores);
        ValidarDefensor(peticion.Defensor, errores);
        ValidarConfiguracion(peticion.ConfiguracionSimulacion, errores);

        return errores;
    }

    private static void ValidarAtacante(PerfilAtacante atacante, List<string> errores)
    {
        if (atacante.ImpactaA < 2 || atacante.ImpactaA > 6)
        {
            errores.Add("impactaA debe estar entre 2 y 6.");
        }
    }

    private static void ValidarArma(PerfilArma arma, List<string> errores)
    {
        ValidarFuenteAtaques(arma, errores);
        ValidarFuenteDanio(arma, errores);

        if (arma.Fuerza < 1)
        {
            errores.Add("fuerza debe ser al menos 1.");
        }

        if (arma.Habilidades.GolpesSostenidos < 0)
        {
            errores.Add("golpesSostenidos no puede ser negativo.");
        }
    }

    private static void ValidarFuenteAtaques(PerfilArma arma, List<string> errores)
    {
        if (arma.AtaquesAleatorios is null)
        {
            if (arma.CantidadAtaques < 1)
            {
                errores.Add("cantidadAtaques debe ser al menos 1 o indicarse ataquesAleatorios.");
            }

            return;
        }

        if (arma.CantidadAtaques > 0)
        {
            errores.Add("ataquesAleatorios debe ser null cuando se usa cantidadAtaques.");
        }

        ValidarDadosAleatorios(arma.AtaquesAleatorios, "ataquesAleatorios", errores);
    }

    private static void ValidarFuenteDanio(PerfilArma arma, List<string> errores)
    {
        if (arma.DanioAleatorio is null)
        {
            if (arma.Danio < 1)
            {
                errores.Add("danio debe ser al menos 1 o indicarse danioAleatorio.");
            }

            return;
        }

        if (arma.Danio > 0)
        {
            errores.Add("danioAleatorio debe ser null cuando se usa danio.");
        }

        ValidarDadosAleatorios(arma.DanioAleatorio, "danioAleatorio", errores);
    }

    private static void ValidarDadosAleatorios(DadosAleatorios dados, string nombre, List<string> errores)
    {
        if (dados.CantidadDados < 1)
        {
            errores.Add($"{nombre}.cantidadDados debe ser al menos 1.");
        }

        if (dados.Caras < 2 || dados.Caras > 6)
        {
            errores.Add($"{nombre}.caras debe estar entre 2 y 6.");
        }
    }

    private static void ValidarDefensor(PerfilDefensor defensor, List<string> errores)
    {
        if (defensor.Resistencia < 1 || defensor.Resistencia > 20)
        {
            errores.Add("resistencia debe estar entre 1 y 20.");
        }

        if (defensor.Salvacion < 2 || defensor.Salvacion > 6)
        {
            errores.Add("salvacion debe estar entre 2 y 6.");
        }

        if (defensor.SalvacionInvulnerable is int invulnerable && (invulnerable < 2 || invulnerable > 6))
        {
            errores.Add("salvacionInvulnerable debe estar entre 2 y 6.");
        }

        if (defensor.SensacionDolor is int sensacionDolor && (sensacionDolor < 3 || sensacionDolor > 6))
        {
            errores.Add("sensacionDolor debe estar entre 3 y 6.");
        }

        if (defensor.HeridasPorMiniatura < 1)
        {
            errores.Add("heridasPorMiniatura debe ser al menos 1.");
        }

        if (defensor.CantidadMiniaturas < 1)
        {
            errores.Add("cantidadMiniaturas debe ser al menos 1.");
        }
    }

    private static void ValidarConfiguracion(ConfiguracionSimulacion configuracion, List<string> errores)
    {
        if (configuracion.Iteraciones < 1 || configuracion.Iteraciones > 100000)
        {
            errores.Add("iteraciones debe estar entre 1 y 100000.");
        }
    }
}