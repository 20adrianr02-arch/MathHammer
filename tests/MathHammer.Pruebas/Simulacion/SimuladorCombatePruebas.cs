using FluentAssertions;
using MathHammer.Api.Reglas;
using MathHammer.Api.Simulacion;

namespace MathHammer.Pruebas.Simulacion;

public class SimuladorCombatePruebas
{
    [Fact]
    public void SimularUnAtaque_ConvergeALaProbabilidadAnalitica()
    {
        // Impacta a 4+ (0.5), hiere a 4+ (0.5), salvación a 4+ (0.5 de salvada).
        // Probabilidad de que un ataque aplique daño = 0.5 * 0.5 * 0.5 = 0.125.
        double probabilidadImpacto = CalculadoraProbabilidades.ProbabilidadExito(4);
        double probabilidadHerida = CalculadoraProbabilidades.ProbabilidadExito(4);
        double probabilidadFallarSalvacion = CalculadoraProbabilidades.ProbabilidadFallo(4);
        double esperado = probabilidadImpacto * probabilidadHerida * probabilidadFallarSalvacion;

        var perfil = CrearPerfil(cantidadAtaques: 1, heridasPorMiniatura: 100);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 200000, semilla: 2026);

        double media = resultados.Average(r => r.HeridasInfligidas);
        media.Should().BeApproximately(esperado, 0.01);
    }

    [Fact]
    public void SimularMuchosAtaques_ConvergeAlValorEsperado()
    {
        double esperado = 100 * 0.5 * 0.5 * 0.5;
        var perfil = CrearPerfil(cantidadAtaques: 100, heridasPorMiniatura: 1000);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 100000, semilla: 777);

        double media = resultados.Average(r => r.HeridasInfligidas);
        media.Should().BeApproximately(esperado, 0.4);
    }

    [Fact]
    public void SinSpillover_ElDañoRespetaLasHeridasDeCadaMiniatura()
    {
        var perfil = CrearPerfil(cantidadAtaques: 20, heridasPorMiniatura: 2, cantidadMiniaturas: 5, golpesLetales: true);
        int heridasTotalesUnidad = perfil.HeridasPorMiniatura * perfil.CantidadMiniaturas;

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 10000, semilla: 555);

        foreach (ResultadoIteracion resultado in resultados)
        {
            resultado.HeridasInfligidas.Should().BeInRange(0, heridasTotalesUnidad);
            resultado.MiniaturasDestruidas.Should().BeInRange(0, perfil.CantidadMiniaturas);
            resultado.MiniaturasDestruidas.Should().Be(resultado.HeridasInfligidas / perfil.HeridasPorMiniatura);
        }
    }

    [Fact]
    public void MismaSemilla_ProduceResultadosIdenticos()
    {
        var perfil = CrearPerfil(cantidadAtaques: 12, heridasPorMiniatura: 3, cantidadMiniaturas: 4, golpesSostenidos: 2);

        ResultadoIteracion[] primera = SimuladorCombate.Simular(perfil, 5000, semilla: 999);
        ResultadoIteracion[] segunda = SimuladorCombate.Simular(perfil, 5000, semilla: 999);

        primera.Should().Equal(segunda);
    }

    [Fact]
    public void Simular_IteracionesInvalidas_LanzaExcepcion()
    {
        var perfil = CrearPerfil(cantidadAtaques: 1, heridasPorMiniatura: 1);

        Action accion = () => SimuladorCombate.Simular(perfil, 0);

        accion.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Simular_PerfilInvalido_LanzaExcepcion()
    {
        var perfil = CrearPerfil(cantidadAtaques: 1, heridasPorMiniatura: 1) with { ImpactaA = 1 };

        Action accion = () => SimuladorCombate.Simular(perfil, 100);

        accion.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void SustainedHits_UnImpactoCritico_GeneraImpactoExtra()
    {
        // Solo impacta con 6 natural (P = 1/6); cada crítico genera 1 impacto extra.
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 100, golpesSostenidos: 1, impactaA: 6);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 1);

        double mediaImpactos = resultados.Average(r => r.ImpactosLogrados);
        mediaImpactos.Should().BeApproximately(1000 * (2.0 / 6.0), 3.0);
    }

    [Fact]
    public void LethalHits_ElSeisNatural_HiereAutomaticamente()
    {
        // Fuerza 1 vs Resistencia 10: heriría a 6+, pero Lethal convierte el 6
        // natural de impacto en herida automática. Heridas esperadas = N / 6.
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 100, golpesLetales: true, impactaA: 6, fuerza: 1, resistencia: 10);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 2);

        double mediaHeridas = resultados.Average(r => r.HeridasLogradas);
        mediaHeridas.Should().BeApproximately(1000.0 / 6.0, 3.0);
    }

    [Fact]
    public void RepiteParaImpactar_AumentaLaProbabilidadEfectiva()
    {
        // Impacta a 4+ (0.5). Con repetición completa: 0.5 + 0.5*0.5 = 0.75.
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 1000000, repiteParaImpactar: true);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 3);

        double mediaImpactos = resultados.Average(r => r.ImpactosLogrados);
        mediaImpactos.Should().BeApproximately(1000 * 0.75, 3.0);
    }

    [Fact]
    public void Lance_SumeUnoALaTiradaDeHerida()
    {
        // Fuerza = Resistencia → heriría a 4+ (0.5); con Lance herida a 3+ (2/3).
        // Impacta a 2+ (5/6). Heridas esperadas = N * 5/6 * 2/3.
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 1000000, lanza: true, impactaA: 2);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 4);

        double mediaHeridas = resultados.Average(r => r.HeridasLogradas);
        mediaHeridas.Should().BeApproximately(1000.0 * (5.0 / 6.0) * (2.0 / 3.0), 3.0);
    }

    [Fact]
    public void Fnp_SeisMas_ReduceElDañoRecibido()
    {
        // Impacta 2+ (5/6), hiere 4+ (0.5), falla salvación 4+ (0.5) y FNP 6+ deja pasar 5/6.
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 1000000, impactaA: 2, sensacionDolor: 6);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 5);

        double esperado = 1000.0 * (5.0 / 6.0) * 0.5 * 0.5 * (5.0 / 6.0);
        double mediaDanio = resultados.Average(r => r.HeridasInfligidas);
        mediaDanio.Should().BeApproximately(esperado, 3.0);
    }

    [Fact]
    public void ReduccionDanio_ReduceElDañoAUno()
    {
        // Impacta 2+ (5/6), hiere 4+ (0.5), falla salvación 4+ (0.5) y daño 2→1.
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 1000000, impactaA: 2, danio: 2, reduccionDanio: true);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 6);

        double esperado = 1000.0 * (5.0 / 6.0) * 0.5 * 0.5;
        double mediaDanio = resultados.Average(r => r.HeridasInfligidas);
        mediaDanio.Should().BeApproximately(esperado, 3.0);
    }

    [Fact]
    public void PenalizacionImpactar_EmpeoraLaTiradaDeImpacto()
    {
        // Impacta a 4+ con -1 → impacta a 5+ (P = 2/6).
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 100, penalizacionImpactar: true);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 7);

        double mediaImpactos = resultados.Average(r => r.ImpactosLogrados);
        mediaImpactos.Should().BeApproximately(1000.0 * (2.0 / 6.0), 3.0);
    }

    [Fact]
    public void TwinLinked_RepiteLaTiradaDeHerida()
    {
        // Fuerza = Resistencia → herida a 4+ (0.5). Twin-linked → 0.5 + 0.5*0.5 = 0.75.
        var perfil = CrearPerfil(cantidadAtaques: 1000, heridasPorMiniatura: 1000000, impactaA: 2, repetirTiradaHerida: true);

        ResultadoIteracion[] resultados = SimuladorCombate.Simular(perfil, 20000, semilla: 8);

        double mediaHeridas = resultados.Average(r => r.HeridasLogradas);
        mediaHeridas.Should().BeApproximately(1000.0 * (5.0 / 6.0) * 0.75, 3.0);
    }

    private static PerfilCombate CrearPerfil(
        int cantidadAtaques,
        int heridasPorMiniatura,
        int cantidadMiniaturas = 1,
        int impactaA = 4,
        int fuerza = 4,
        int resistencia = 4,
        int danio = 1,
        bool lanza = false,
        int golpesSostenidos = 0,
        bool golpesLetales = false,
        bool heridasDevastadoras = false,
        bool repetirTiradaHerida = false,
        bool repiteParaImpactar = false,
        bool repiteUnoParaHerir = false,
        int? sensacionDolor = null,
        bool reduccionDanio = false,
        bool penalizacionImpactar = false,
        bool penalizacionHerir = false)
    {
        return new PerfilCombate
        {
            CantidadAtaques = cantidadAtaques,
            ImpactaA = impactaA,
            Fuerza = fuerza,
            PenetracionArmadura = 0,
            Danio = danio,
            Resistencia = resistencia,
            Salvacion = 4,
            SalvacionInvulnerable = null,
            HeridasPorMiniatura = heridasPorMiniatura,
            CantidadMiniaturas = cantidadMiniaturas,
            Lanza = lanza,
            GolpesSostenidos = golpesSostenidos,
            GolpesLetales = golpesLetales,
            HeridasDevastadoras = heridasDevastadoras,
            RepetirTiradaHerida = repetirTiradaHerida,
            RepiteParaImpactar = repiteParaImpactar,
            RepiteUnoParaHerir = repiteUnoParaHerir,
            SensacionDolor = sensacionDolor,
            ReduccionDanio = reduccionDanio,
            PenalizacionImpactar = penalizacionImpactar,
            PenalizacionHerir = penalizacionHerir,
        };
    }
}