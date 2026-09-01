// Tipos que reflejan el contrato de la API (docs/contrato-api.md, versión 1.3).

export interface PerfilAtacanteApi {
  nombreUnidad: string
  impactaA: number
  repiteParaImpactar: boolean
  repiteUnoParaHerir: boolean
}

export interface HabilidadesArmaApi {
  lanza: boolean
  golpesSostenidos: number
  golpesLetales: boolean
  heridasDevastadoras: boolean
}

export interface PerfilArmaApi {
  cantidadAtaques: number
  fuerza: number
  penetracionArmadura: number
  danio: number
  repetirTiradaHerida: boolean
  habilidades: HabilidadesArmaApi
}

export interface PerfilDefensorApi {
  nombreUnidad: string
  resistencia: number
  salvacion: number
  salvacionInvulnerable: number | null
  sensacionDolor: number | null
  reduccionDanio: boolean
  penalizacionImpactar: boolean
  penalizacionHerir: boolean
  heridasPorMiniatura: number
  cantidadMiniaturas: number
}

export interface ConfiguracionSimulacionApi {
  iteraciones: number
  semillaAleatoria: number | null
}

export interface PeticionCombate {
  atacante: PerfilAtacanteApi
  arma: PerfilArmaApi
  defensor: PerfilDefensorApi
  configuracionSimulacion: ConfiguracionSimulacionApi
}

export interface MetricasCombate {
  impactosEsperados: number
  heridasEsperadas: number
  salvacionesEnemigo: number
  probabilidadMatarUnidad: number
  miniaturasEliminadas: number
  danioMedioEsperado: number
  percentil25: number
  percentil75: number
}

export interface ResumenSimulacion {
  iteracionesEjecutadas: number
  duracionMilisegundos: number
}

export interface ResultadoCombate {
  metricas: MetricasCombate
  resumen: ResumenSimulacion
}