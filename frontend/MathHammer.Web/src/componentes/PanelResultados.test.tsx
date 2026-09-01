import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { PanelResultados } from './PanelResultados'
import type { ResultadoCombate } from '../contratos/tipos'

describe('PanelResultados', () => {
  it('muestra las métricas cuando hay resultado', () => {
    render(<PanelResultados resultado={crearResultado()} cargando={false} error={null} />)

    expect(screen.getByText('Impactos esperados')).toBeTruthy()
    expect(screen.getByText('5.33')).toBeTruthy()
    expect(screen.getByText('Prob. de matar unidad (%)')).toBeTruthy()
    expect(screen.getByText('31.2%')).toBeTruthy()
    expect(screen.getByText('10000 iteraciones · 14 ms')).toBeTruthy()
  })

  it('muestra el estado de carga', () => {
    render(<PanelResultados resultado={null} cargando error={null} />)

    expect(screen.getByText('CALCULANDO...')).toBeTruthy()
  })

  it('muestra el error cuando lo hay', () => {
    render(<PanelResultados resultado={null} cargando={false} error="No se pudo conectar." />)

    expect(screen.getByText('No se pudo conectar.')).toBeTruthy()
  })
})

function crearResultado(): ResultadoCombate {
  return {
    metricas: {
      impactosEsperados: 5.333,
      heridasEsperadas: 3.556,
      salvacionesEnemigo: 1.185,
      probabilidadMatarUnidad: 0.312,
      miniaturasEliminadas: 0.935,
      danioMedioEsperado: 2.377,
      percentil25: 1.0,
      percentil75: 3.0,
    },
    resumen: {
      iteracionesEjecutadas: 10000,
      duracionMilisegundos: 14,
    },
  }
}