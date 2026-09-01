import { useState } from 'react'
import type { ResultadoCombate } from '../contratos/tipos'
import type { PerfilAtacante } from '../componentes/PanelAtacante'
import type { PerfilDefensor } from '../componentes/PanelDefensor'
import { mapearPeticion } from '../servicios/mapearPeticion'
import { simularCombate } from '../servicios/clienteApi'

interface EstadoCalculo {
  cargando: boolean
  error: string | null
  resultado: ResultadoCombate | null
}

/**
 * Gestiona el cálculo de combate: mapea el formulario, llama a la API y
 * expone el estado de carga, error y resultado.
 */
export function usarCalculoCombate() {
  const [estado, establecerEstado] = useState<EstadoCalculo>({
    cargando: false,
    error: null,
    resultado: null,
  })

  async function calcular(atacante: PerfilAtacante, defensor: PerfilDefensor): Promise<void> {
    const { peticion, errores } = mapearPeticion(atacante, defensor)

    if (peticion === null) {
      establecerEstado({ cargando: false, error: errores.join(' '), resultado: null })
      return
    }

    establecerEstado({ cargando: true, error: null, resultado: null })

    try {
      const resultado = await simularCombate(peticion)
      establecerEstado({ cargando: false, error: null, resultado })
    } catch (excepcion) {
      const mensaje = excepcion instanceof Error ? excepcion.message : 'Error al calcular el combate.'
      establecerEstado({ cargando: false, error: mensaje, resultado: null })
    }
  }

  return { ...estado, calcular }
}