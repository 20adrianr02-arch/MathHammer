// Tipos que reflejan el contrato de la API (docs/contrato-api.md, v1.4).
//
// La serialización usa los nombres JSON del contrato (camelCase).

class PeticionCombate {
  const PeticionCombate({
    required this.atacante,
    required this.arma,
    required this.defensor,
    required this.iteraciones,
  });

  final Atacante atacante;
  final Arma arma;
  final Defensor defensor;
  final int iteraciones;

  Map<String, dynamic> toJson() => {
        'atacante': atacante.toJson(),
        'arma': arma.toJson(),
        'defensor': defensor.toJson(),
        'configuracionSimulacion': {
          'iteraciones': iteraciones,
          'semillaAleatoria': null,
        },
      };
}

class Atacante {
  const Atacante({
    required this.nombreUnidad,
    required this.impactaA,
    this.repiteParaImpactar = false,
    this.repiteUnoParaHerir = false,
  });

  final String nombreUnidad;
  final int impactaA;
  final bool repiteParaImpactar;
  final bool repiteUnoParaHerir;

  Map<String, dynamic> toJson() => {
        'nombreUnidad': nombreUnidad,
        'impactaA': impactaA,
        'repiteParaImpactar': repiteParaImpactar,
        'repiteUnoParaHerir': repiteUnoParaHerir,
      };
}

class Arma {
  const Arma({
    required this.cantidadAtaques,
    required this.fuerza,
    required this.penetracionArmadura,
    required this.danio,
    this.repetirTiradaHerida = false,
    this.lanza = false,
    this.golpesSostenidos = 0,
    this.golpesLetales = false,
    this.heridasDevastadoras = false,
  });

  final int cantidadAtaques;
  final int fuerza;
  final int penetracionArmadura;
  final int danio;
  final bool repetirTiradaHerida;
  final bool lanza;
  final int golpesSostenidos;
  final bool golpesLetales;
  final bool heridasDevastadoras;

  Map<String, dynamic> toJson() => {
        'cantidadAtaques': cantidadAtaques,
        'fuerza': fuerza,
        'penetracionArmadura': penetracionArmadura,
        'danio': danio,
        'repetirTiradaHerida': repetirTiradaHerida,
        'habilidades': {
          'lanza': lanza,
          'golpesSostenidos': golpesSostenidos,
          'golpesLetales': golpesLetales,
          'heridasDevastadoras': heridasDevastadoras,
        },
      };
}

class Defensor {
  const Defensor({
    required this.nombreUnidad,
    required this.resistencia,
    required this.salvacion,
    required this.heridasPorMiniatura,
    required this.cantidadMiniaturas,
    this.salvacionInvulnerable,
    this.sensacionDolor,
    this.reduccionDanio = false,
    this.penalizacionImpactar = false,
    this.penalizacionHerir = false,
  });

  final String nombreUnidad;
  final int resistencia;
  final int salvacion;
  final int heridasPorMiniatura;
  final int cantidadMiniaturas;
  final int? salvacionInvulnerable;
  final int? sensacionDolor;
  final bool reduccionDanio;
  final bool penalizacionImpactar;
  final bool penalizacionHerir;

  Map<String, dynamic> toJson() => {
        'nombreUnidad': nombreUnidad,
        'resistencia': resistencia,
        'salvacion': salvacion,
        'salvacionInvulnerable': salvacionInvulnerable,
        'sensacionDolor': sensacionDolor,
        'reduccionDanio': reduccionDanio,
        'penalizacionImpactar': penalizacionImpactar,
        'penalizacionHerir': penalizacionHerir,
        'heridasPorMiniatura': heridasPorMiniatura,
        'cantidadMiniaturas': cantidadMiniaturas,
      };
}

class ResultadoCombate {
  const ResultadoCombate({
    required this.metricas,
    required this.iteracionesEjecutadas,
    required this.duracionMilisegundos,
  });

  final Metricas metricas;
  final int iteracionesEjecutadas;
  final int duracionMilisegundos;

  factory ResultadoCombate.fromJson(Map<String, dynamic> json) {
    return ResultadoCombate(
      metricas: Metricas.fromJson(json['metricas'] as Map<String, dynamic>),
      iteracionesEjecutadas: (json['resumen'] as Map<String, dynamic>)['iteracionesEjecutadas'] as int,
      duracionMilisegundos: (json['resumen'] as Map<String, dynamic>)['duracionMilisegundos'] as int,
    );
  }
}

class Metricas {
  const Metricas({
    required this.impactosEsperados,
    required this.heridasEsperadas,
    required this.salvacionesEnemigo,
    required this.probabilidadMatarUnidad,
    required this.miniaturasEliminadas,
    required this.danioMedioEsperado,
    required this.percentil25,
    required this.percentil75,
  });

  final double impactosEsperados;
  final double heridasEsperadas;
  final double salvacionesEnemigo;
  final double probabilidadMatarUnidad;
  final double miniaturasEliminadas;
  final double danioMedioEsperado;
  final double percentil25;
  final double percentil75;

  factory Metricas.fromJson(Map<String, dynamic> json) {
    double numero(dynamic valor) => (valor as num).toDouble();
    return Metricas(
      impactosEsperados: numero(json['impactosEsperados']),
      heridasEsperadas: numero(json['heridasEsperadas']),
      salvacionesEnemigo: numero(json['salvacionesEnemigo']),
      probabilidadMatarUnidad: numero(json['probabilidadMatarUnidad']),
      miniaturasEliminadas: numero(json['miniaturasEliminadas']),
      danioMedioEsperado: numero(json['danioMedioEsperado']),
      percentil25: numero(json['percentil25']),
      percentil75: numero(json['percentil75']),
    );
  }
}