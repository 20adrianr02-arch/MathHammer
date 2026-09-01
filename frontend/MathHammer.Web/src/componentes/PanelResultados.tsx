import type { ResultadoCombate } from '../contratos/tipos'

interface PropiedadesPanelResultados {
  resultado: ResultadoCombate | null
  cargando: boolean
  error: string | null
}

export function PanelResultados({ resultado, cargando, error }: PropiedadesPanelResultados) {
  return (
    <section className="resultados" aria-label="Resultados de la simulación">
      <div className="resultados__cabecera"><span>RESULTADOS DE COMBATE</span></div>

      {cargando && <p className="resultados__estado">CALCULANDO...</p>}

      {!cargando && error && <p className="resultados__error">{error}</p>}

      {!cargando && !error && resultado && (
        <>
          <div className="resultados__rejilla">
            <Tarjeta etiqueta="Impactos esperados" valor={formatearNumero(resultado.metricas.impactosEsperados)} />
            <Tarjeta etiqueta="Heridas esperadas" valor={formatearNumero(resultado.metricas.heridasEsperadas)} />
            <Tarjeta etiqueta="Salvaciones del enemigo" valor={formatearNumero(resultado.metricas.salvacionesEnemigo)} />
            <Tarjeta etiqueta="Prob. de matar unidad (%)" valor={formatearPorcentaje(resultado.metricas.probabilidadMatarUnidad)} />
            <Tarjeta etiqueta="Miniaturas eliminadas" valor={formatearNumero(resultado.metricas.miniaturasEliminadas)} />
            <Tarjeta etiqueta="Daño medio esperado" valor={formatearNumero(resultado.metricas.danioMedioEsperado)} />
            <Tarjeta etiqueta="P25 (Rango mínimo)" valor={formatearNumero(resultado.metricas.percentil25)} amplio />
            <Tarjeta etiqueta="P75 (Rango máximo)" valor={formatearNumero(resultado.metricas.percentil75)} amplio />
          </div>
          <p className="resultados__resumen">
            {resultado.resumen.iteracionesEjecutadas} iteraciones · {resultado.resumen.duracionMilisegundos} ms
          </p>
        </>
      )}
    </section>
  )
}

interface PropiedadesTarjeta {
  etiqueta: string
  valor: string
  amplio?: boolean
}

function Tarjeta({ etiqueta, valor, amplio = false }: PropiedadesTarjeta) {
  return (
    <article className={`resultado ${amplio ? 'resultado--amplio' : ''}`}>
      <p className="resultado__etiqueta">{etiqueta}</p>
      <p className="resultado__valor">{valor}</p>
    </article>
  )
}

function formatearNumero(valor: number): string {
  return valor.toFixed(2)
}

function formatearPorcentaje(valor: number): string {
  return `${(valor * 100).toFixed(1)}%`
}