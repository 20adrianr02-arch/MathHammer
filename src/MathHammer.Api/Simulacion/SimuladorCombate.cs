using MathHammer.Api.Reglas;

namespace MathHammer.Api.Simulacion;

/// <summary>
/// Simula la secuencia de combate completa con Monte Carlo, aplicando las
/// habilidades ofensivas y defensivas del perfil: impacto, herida, salvación,
/// daño (reducción y FNP) y asignación sin spillover.
/// </summary>
public static class SimuladorCombate
{
    /// <summary>
    /// Ejecuta una única iteración de combate con el perfil indicado.
    /// </summary>
    public static ResultadoIteracion SimularIteracion(PerfilCombate perfil, GeneradorAleatorio generador)
    {
        ValidarPerfil(perfil);

        int heridasInfligidas = 0;
        int miniaturasDestruidas = 0;
        int miniaturasVivas = perfil.CantidadMiniaturas;
        int heridasRestantesModelo = perfil.HeridasPorMiniatura;

        int impactosLogrados = 0;
        int heridasLogradas = 0;
        int salvacionesLogradas = 0;

        int tiradaHerida = ReglaHerida.TiradaHeridaRequerida(perfil.Fuerza, perfil.Resistencia);
        int tiradaSalvacion = ReglaSalvacion.TiradaSalvacionRequerida(
            perfil.Salvacion, perfil.PenetracionArmadura, perfil.SalvacionInvulnerable);

        int modificadorImpacto = perfil.PenalizacionImpactar ? -1 : 0;
        int modificadorHerida = Math.Clamp((perfil.Lanza ? 1 : 0) + (perfil.PenalizacionHerir ? -1 : 0), -1, 1);

        TipoRepeticion repeticionImpacto = perfil.RepiteParaImpactar ? TipoRepeticion.Todas : TipoRepeticion.Ninguna;
        TipoRepeticion repeticionHerida = ObtenerRepeticionHerida(perfil);

        for (int ataque = 0; ataque < perfil.CantidadAtaques && miniaturasVivas > 0; ataque++)
        {
            int rollImpacto = TirarConRepeticion(generador, perfil.ImpactaA, repeticionImpacto);

            if (!EsExitoConModificador(rollImpacto, perfil.ImpactaA, modificadorImpacto))
            {
                continue;
            }

            bool criticoImpacto = rollImpacto == 6;
            int impactosExtra = criticoImpacto ? perfil.GolpesSostenidos : 0;
            bool impactoLetal = criticoImpacto && perfil.GolpesLetales;

            impactosLogrados += 1 + impactosExtra;

            int heridasNormales = (impactoLetal ? 0 : 1) + impactosExtra;
            int heridasAutomaticas = impactoLetal ? 1 : 0;

            for (int indice = 0; indice < heridasNormales && miniaturasVivas > 0; indice++)
            {
                int rollHerida = TirarConRepeticion(generador, tiradaHerida, repeticionHerida);

                if (!EsExitoConModificador(rollHerida, tiradaHerida, modificadorHerida))
                {
                    continue;
                }

                heridasLogradas++;
                bool criticoHerida = rollHerida == 6;
                bool devastador = criticoHerida && perfil.HeridasDevastadoras;

                AplicarHerida(
                    perfil,
                    generador,
                    tiradaSalvacion,
                    devastador,
                    ref heridasInfligidas,
                    ref miniaturasDestruidas,
                    ref miniaturasVivas,
                    ref heridasRestantesModelo,
                    ref salvacionesLogradas);
            }

            for (int indice = 0; indice < heridasAutomaticas && miniaturasVivas > 0; indice++)
            {
                heridasLogradas++;
                AplicarHerida(
                    perfil,
                    generador,
                    tiradaSalvacion,
                    esDevastador: false,
                    ref heridasInfligidas,
                    ref miniaturasDestruidas,
                    ref miniaturasVivas,
                    ref heridasRestantesModelo,
                    ref salvacionesLogradas);
            }
        }

        return new ResultadoIteracion(
            heridasInfligidas,
            miniaturasDestruidas,
            impactosLogrados,
            heridasLogradas,
            salvacionesLogradas);
    }

    /// <summary>
    /// Ejecuta <paramref name="iteraciones"/> iteraciones de combate y devuelve
    /// el resultado de cada una. Con <paramref name="semilla"/> es reproducible.
    /// </summary>
    public static ResultadoIteracion[] Simular(PerfilCombate perfil, int iteraciones, int? semilla = null)
    {
        ValidarPerfil(perfil);

        if (iteraciones < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(iteraciones), "La cantidad de iteraciones debe ser al menos 1.");
        }

        var generador = semilla.HasValue
            ? new GeneradorAleatorio(semilla.Value)
            : new GeneradorAleatorio();

        var resultados = new ResultadoIteracion[iteraciones];
        for (int indice = 0; indice < iteraciones; indice++)
        {
            resultados[indice] = SimularIteracion(perfil, generador);
        }

        return resultados;
    }

    /// <summary>
    /// Resuelve la salvación (salvo devastador) y aplica el daño con reducción,
    /// FNP y asignación sin spillover.
    /// </summary>
    private static void AplicarHerida(
        PerfilCombate perfil,
        GeneradorAleatorio generador,
        int tiradaSalvacion,
        bool esDevastador,
        ref int heridasInfligidas,
        ref int miniaturasDestruidas,
        ref int miniaturasVivas,
        ref int heridasRestantesModelo,
        ref int salvacionesLogradas)
    {
        if (!esDevastador && tiradaSalvacion < ReglaSalvacion.SinSalvacion)
        {
            int rollSalvacion = generador.LanzarD6();
            if (rollSalvacion >= tiradaSalvacion)
            {
                salvacionesLogradas++;
                return;
            }
        }

        int danioEfectivo = perfil.ReduccionDanio
            ? Math.Max(1, perfil.Danio - 1)
            : perfil.Danio;

        if (perfil.SensacionDolor is int sensacionDolor)
        {
            int danioInfligido = 0;
            for (int punto = 0; punto < danioEfectivo; punto++)
            {
                if (generador.LanzarD6() < sensacionDolor)
                {
                    danioInfligido++;
                }
            }

            danioEfectivo = danioInfligido;
        }

        int aplicado = Math.Min(danioEfectivo, heridasRestantesModelo);
        heridasInfligidas += aplicado;
        heridasRestantesModelo -= aplicado;

        if (heridasRestantesModelo == 0)
        {
            miniaturasDestruidas++;
            miniaturasVivas--;
            heridasRestantesModelo = perfil.HeridasPorMiniatura;
        }
    }

    private static TipoRepeticion ObtenerRepeticionHerida(PerfilCombate perfil)
    {
        if (perfil.RepetirTiradaHerida)
        {
            return TipoRepeticion.Todas;
        }

        if (perfil.RepiteUnoParaHerir)
        {
            return TipoRepeticion.Unos;
        }

        return TipoRepeticion.Ninguna;
    }

    private static int TirarConRepeticion(GeneradorAleatorio generador, int requerido, TipoRepeticion tipoRepeticion)
    {
        int roll = generador.LanzarD6();

        bool debeRepetir = tipoRepeticion switch
        {
            TipoRepeticion.Todas => roll < requerido,
            TipoRepeticion.Unos => roll == 1,
            _ => false,
        };

        if (debeRepetir)
        {
            roll = generador.LanzarD6();
        }

        return roll;
    }

    private static bool EsExitoConModificador(int roll, int requerido, int modificador)
    {
        if (roll == 1)
        {
            return false;
        }

        if (roll == 6)
        {
            return true;
        }

        return roll + modificador >= requerido;
    }

    private static void ValidarPerfil(PerfilCombate perfil)
    {
        if (perfil.CantidadAtaques < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(perfil.CantidadAtaques), "La cantidad de ataques no puede ser negativa.");
        }

        if (perfil.ImpactaA < 2 || perfil.ImpactaA > 6)
        {
            throw new ArgumentOutOfRangeException(nameof(perfil.ImpactaA), "La habilidad de impacto debe estar entre 2 y 6.");
        }

        if (perfil.Danio < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(perfil.Danio), "El daño debe ser al menos 1.");
        }

        if (perfil.GolpesSostenidos < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(perfil.GolpesSostenidos), "Los golpes sostenidos no pueden ser negativos.");
        }

        if (perfil.SensacionDolor is int sensacionDolor && (sensacionDolor < 3 || sensacionDolor > 6))
        {
            throw new ArgumentOutOfRangeException(nameof(perfil.SensacionDolor), "La sensación de dolor debe estar entre 3 y 6.");
        }

        if (perfil.HeridasPorMiniatura < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(perfil.HeridasPorMiniatura), "Las heridas por miniatura deben ser al menos 1.");
        }

        if (perfil.CantidadMiniaturas < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(perfil.CantidadMiniaturas), "La cantidad de miniaturas debe ser al menos 1.");
        }
    }

    private enum TipoRepeticion
    {
        Ninguna,
        Todas,
        Unos,
    }
}