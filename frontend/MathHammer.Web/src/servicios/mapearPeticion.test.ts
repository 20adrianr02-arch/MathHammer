import { describe, it, expect } from 'vitest'
import { mapearPeticion } from './mapearPeticion'
import type { PerfilAtacante } from '../componentes/PanelAtacante'
import type { PerfilDefensor } from '../componentes/PanelDefensor'

describe('mapearPeticion', () => {
  it('mapea un formulario válido a la petición', () => {
    const { peticion, errores } = mapearPeticion(crearAtacante(), crearDefensor())

    expect(errores).toEqual([])
    expect(peticion).not.toBeNull()
    expect(peticion!.atacante.impactaA).toBe(3)
    expect(peticion!.arma.cantidadAtaques).toBe(8)
    expect(peticion!.arma.penetracionArmadura).toBe(-2)
    expect(peticion!.defensor.salvacion).toBe(4)
    expect(peticion!.defensor.salvacionInvulnerable).toBe(5)
    expect(peticion!.configuracionSimulacion.iteraciones).toBe(10000)
    expect(peticion!.configuracionSimulacion.semillaAleatoria).toBeNull()
  })

  it('convierte la salvación invulnerable vacía a null', () => {
    const { peticion } = mapearPeticion(crearAtacante(), crearDefensor({ salvacionInvulnerable: '' }))

    expect(peticion!.defensor.salvacionInvulnerable).toBeNull()
  })

  it('mapea la FNP solo cuando la casilla está activa', () => {
    const { peticion: conFnp } = mapearPeticion(crearAtacante(), crearDefensor({ sinDolor: true, sensacionDolor: '6+' }))
    const { peticion: sinFnp } = mapearPeticion(crearAtacante(), crearDefensor({ sinDolor: false, sensacionDolor: '6+' }))

    expect(conFnp!.defensor.sensacionDolor).toBe(6)
    expect(sinFnp!.defensor.sensacionDolor).toBeNull()
  })

  it('mapea los golpes sostenidos solo cuando la casilla está activa', () => {
    const { peticion: activo } = mapearPeticion(crearAtacante({ golpesSostenidos: true, valorGolpesSostenidos: '2' }), crearDefensor())
    const { peticion: inactivo } = mapearPeticion(crearAtacante({ golpesSostenidos: false, valorGolpesSostenidos: '2' }), crearDefensor())

    expect(activo!.arma.habilidades.golpesSostenidos).toBe(2)
    expect(inactivo!.arma.habilidades.golpesSostenidos).toBe(0)
  })

  it('devuelve error si falta impacta a', () => {
    const { peticion, errores } = mapearPeticion(crearAtacante({ impactaA: '' }), crearDefensor())

    expect(peticion).toBeNull()
    expect(errores.join(' ')).toContain('IMPACTA A')
  })

  it('devuelve error si la cantidad de ataques es 0', () => {
    const { peticion, errores } = mapearPeticion(crearAtacante({ cantidadAtaques: '0' }), crearDefensor())

    expect(peticion).toBeNull()
    expect(errores.join(' ')).toContain('ataques')
  })
})

interface OpcionesAtacante {
  impactaA?: string
  cantidadAtaques?: string
  golpesSostenidos?: boolean
  valorGolpesSostenidos?: string
}

function crearAtacante(opciones: OpcionesAtacante = {}): PerfilAtacante {
  return {
    nombreUnidad: 'Escuadra intercesora',
    cantidadAtaques: opciones.cantidadAtaques ?? '8',
    impactaA: opciones.impactaA ?? '3+',
    fuerza: '5',
    penetracionArmadura: '-2',
    danio: '1',
    habilidades: {
      impactosLetales: false,
      repiteParaImpactar: false,
      repetirTiradaHerida: false,
      repiteUnoParaHerir: false,
      lance: false,
      heridasDevastadoras: false,
      golpesSostenidos: opciones.golpesSostenidos ?? false,
    },
    golpesSostenidos: opciones.valorGolpesSostenidos ?? '1',
  }
}

interface OpcionesDefensor {
  salvacion?: string
  salvacionInvulnerable?: string
  sinDolor?: boolean
  sensacionDolor?: string
}

function crearDefensor(opciones: OpcionesDefensor = {}): PerfilDefensor {
  return {
    nombreUnidad: 'Guerreros Necrones',
    resistencia: '4',
    heridasPorMiniatura: '2',
    cantidadMiniaturas: '5',
    salvacion: opciones.salvacion ?? '4+',
    salvacionInvulnerable: opciones.salvacionInvulnerable ?? '5+',
    sensacionDolor: opciones.sensacionDolor ?? '6+',
    habilidades: {
      reduccionDanio: false,
      sinDolor: opciones.sinDolor ?? false,
      penalizacionImpactar: false,
      penalizacionHerir: false,
    },
  }
}