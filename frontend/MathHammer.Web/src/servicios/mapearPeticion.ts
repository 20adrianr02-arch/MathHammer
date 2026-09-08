import type { PeticionCombate } from '../contratos/tipos'
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

  const cantidadAtaques = parsearEntero(atacante.cantidadAtaques)
  if (cantidadAtaques === null || cantidadAtaques < 1) {
    errores.push('La cantidad de ataques debe ser al menos 1.')
  }

  const fuerza = parsearEntero(atacante.fuerza)
  if (fuerza === null || fuerza < 1) {
    errores.push('La fuerza debe ser al menos 1.')
  }

  const penetracionArmadura = parsearEntero(atacante.penetracionArmadura)
  if (penetracionArmadura === null) {
    errores.push('La penetración de armadura no es válida.')
  }

  const danio = parsearEntero(atacante.danio)
  if (danio === null || danio < 1) {
    errores.push('El daño debe ser al menos 1.')
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
      cantidadAtaques: cantidadAtaques!,
      fuerza: fuerza!,
      penetracionArmadura: penetracionArmadura!,
      danio: danio!,
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