import type { DadosApi, PeticionCombate } from '../contratos/tipos'
import type { PerfilAtacante } from '../componentes/PanelAtacante'
import type { PerfilDefensor } from '../componentes/PanelDefensor'

export interface ResultadoMapeo {
  peticion: PeticionCombate | null
  errores: string[]
}

const IteracionesFijas = 10000

/**
 * Convierte el estado del formulario (strings) a la petición de la API,
 * validando los valores. Devuelve la petición o la lista de errores.
 */
export function mapearPeticion(atacante: PerfilAtacante, defensor: PerfilDefensor): ResultadoMapeo {
  const errores: string[] = []

  const impactaA = parsearValor(atacante.impactaA)
  if (impactaA === null) {
    errores.push('Selecciona IMPACTA A.')
  }

  const ataques = mapearFuenteAtaques(atacante, errores)
  const danio = mapearFuenteDanio(atacante, errores)

  const fuerza = parsearEntero(atacante.fuerza)
  if (fuerza === null || fuerza < 1) {
    errores.push('La fuerza debe ser al menos 1.')
  }

  const penetracionArmadura = parsearEntero(atacante.penetracionArmadura)
  if (penetracionArmadura === null) {
    errores.push('La penetración de armadura no es válida.')
  }

  const salvacion = parsearValor(defensor.salvacion)
  if (salvacion === null) {
    errores.push('Introduce la salvación.')
  }

  const salvacionInvulnerable = defensor.salvacionInvulnerable === '' ? null : parsearValor(defensor.salvacionInvulnerable)
  if (defensor.salvacionInvulnerable !== '' && salvacionInvulnerable === null) {
    errores.push('La salvación invulnerable no es válida.')
  }

  const resistencia = parsearEntero(defensor.resistencia)
  if (resistencia === null || resistencia < 1 || resistencia > 20) {
    errores.push('La resistencia debe estar entre 1 y 20.')
  }

  const heridasPorMiniatura = parsearEntero(defensor.heridasPorMiniatura)
  if (heridasPorMiniatura === null || heridasPorMiniatura < 1) {
    errores.push('Las heridas por miniatura deben ser al menos 1.')
  }

  const cantidadMiniaturas = parsearEntero(defensor.cantidadMiniaturas)
  if (cantidadMiniaturas === null || cantidadMiniaturas < 1) {
    errores.push('La cantidad de miniaturas debe ser al menos 1.')
  }

  const sensacionDolor = defensor.habilidades.sinDolor ? parsearValor(defensor.sensacionDolor) : null
  if (defensor.habilidades.sinDolor && sensacionDolor === null) {
    errores.push('Selecciona el valor de FEEL NO PAIN.')
  }

  const golpesSostenidos = atacante.habilidades.golpesSostenidos ? parsearEntero(atacante.golpesSostenidos) : 0

  if (errores.length > 0) {
    return { peticion: null, errores }
  }

  const peticion: PeticionCombate = {
    atacante: {
      nombreUnidad: atacante.nombreUnidad,
      impactaA: impactaA!,
      repiteParaImpactar: atacante.habilidades.repiteParaImpactar,
      repiteUnoParaHerir: atacante.habilidades.repiteUnoParaHerir,
    },
    arma: {
      cantidadAtaques: ataques.cantidadAtaques,
      ataquesAleatorios: ataques.dados,
      fuerza: fuerza!,
      penetracionArmadura: penetracionArmadura!,
      danio: danio.cantidadDanio,
      danioAleatorio: danio.dados,
      repetirTiradaHerida: atacante.habilidades.repetirTiradaHerida,
      habilidades: {
        lanza: atacante.habilidades.lance,
        golpesSostenidos: golpesSostenidos ?? 0,
        golpesLetales: atacante.habilidades.impactosLetales,
        heridasDevastadoras: atacante.habilidades.heridasDevastadoras,
      },
    },
    defensor: {
      nombreUnidad: defensor.nombreUnidad,
      resistencia: resistencia!,
      salvacion: salvacion!,
      salvacionInvulnerable,
      sensacionDolor,
      reduccionDanio: defensor.habilidades.reduccionDanio,
      penalizacionImpactar: defensor.habilidades.penalizacionImpactar,
      penalizacionHerir: defensor.habilidades.penalizacionHerir,
      heridasPorMiniatura: heridasPorMiniatura!,
      cantidadMiniaturas: cantidadMiniaturas!,
    },
    configuracionSimulacion: {
      iteraciones: IteracionesFijas,
      semillaAleatoria: null,
    },
  }

  return { peticion, errores: [] }
}

interface FuenteAtaques {
  cantidadAtaques: number
  dados: DadosApi | null
}

interface FuenteDanio {
  cantidadDanio: number
  dados: DadosApi | null
}

function mapearFuenteAtaques(atacante: PerfilAtacante, errores: string[]): FuenteAtaques {
  if (atacante.habilidades.ataquesAleatorios) {
    const dados = parsearExpresionDados(atacante.cantidadAtaques)
    if (dados === null) {
      errores.push('La cantidad de ataques no es una expresión de dados válida (ej. D6, 2D3, D6+1).')
      return { cantidadAtaques: 0, dados: null }
    }
    return { cantidadAtaques: 0, dados }
  }

  const cantidadAtaques = parsearEntero(atacante.cantidadAtaques)
  if (cantidadAtaques === null || cantidadAtaques < 1) {
    errores.push('La cantidad de ataques debe ser al menos 1.')
    return { cantidadAtaques: 0, dados: null }
  }

  return { cantidadAtaques, dados: null }
}

function mapearFuenteDanio(atacante: PerfilAtacante, errores: string[]): FuenteDanio {
  if (atacante.habilidades.danioAleatorio) {
    const dados = parsearExpresionDados(atacante.danio)
    if (dados === null) {
      errores.push('El daño no es una expresión de dados válida (ej. D6, 2D3, D6+1).')
      return { cantidadDanio: 0, dados: null }
    }
    return { cantidadDanio: 0, dados }
  }

  const cantidadDanio = parsearEntero(atacante.danio)
  if (cantidadDanio === null || cantidadDanio < 1) {
    errores.push('El daño debe ser al menos 1.')
    return { cantidadDanio: 0, dados: null }
  }

  return { cantidadDanio, dados: null }
}

/**
 * Interpreta una expresión de dados como "D6", "2D3", "D6+1" o "D6-1".
 * Devuelve null si no es válida.
 */
function parsearExpresionDados(texto: string): DadosApi | null {
  const coincidencia = /^(\d*)[Dd](\d+)([+-]\d+)?$/.exec(texto.trim())
  if (coincidencia === null) {
    return null
  }

  const cantidadDados = coincidencia[1] === '' ? 1 : Number.parseInt(coincidencia[1], 10)
  const caras = Number.parseInt(coincidencia[2], 10)
  const modificador = coincidencia[3] === undefined ? 0 : Number.parseInt(coincidencia[3], 10)

  if (cantidadDados < 1 || caras < 2 || caras > 6) {
    return null
  }

  return { cantidadDados, caras, modificador }
}

/**
 * Interpreta un valor tipo "3+" como número. Devuelve null si no es válido.
 */
function parsearValor(valor: string): number | null {
  const entero = parsearEntero(valor)
  if (entero === null || entero < 2 || entero > 6) {
    return null
  }
  return entero
}

function parsearEntero(valor: string): number | null {
  const texto = valor.trim()
  if (texto === '') {
    return null
  }
  const entero = Number.parseInt(texto, 10)
  return Number.isNaN(entero) ? null : entero
}