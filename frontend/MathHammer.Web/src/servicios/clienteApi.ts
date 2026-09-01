import type { PeticionCombate, ResultadoCombate } from '../contratos/tipos'

const urlBase = import.meta.env.VITE_API_URL ?? 'http://localhost:5188'

/**
 * Envía la petición de combate al backend y devuelve el resultado. Lanza un
 * error descriptivo cuando la petición falla o no es válida.
 */
export async function simularCombate(peticion: PeticionCombate): Promise<ResultadoCombate> {
  let respuesta: Response
  try {
    respuesta = await fetch(`${urlBase}/api/combate/simular`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(peticion),
    })
  } catch {
    throw new Error('No se pudo conectar con el servidor. Comprueba que la API está en marcha.')
  }

  if (respuesta.ok) {
    return (await respuesta.json()) as ResultadoCombate
  }

  if (respuesta.status === 422) {
    const problema = (await respuesta.json()) as { detail?: string }
    throw new Error(problema.detail ?? 'La petición no es válida.')
  }

  throw new Error(`Error inesperado del servidor (${respuesta.status}).`)
}