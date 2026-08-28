interface DatoResultado {
  etiqueta: string
  valor: string
  amplio?: boolean
}

const metricas: DatoResultado[] = [
  { etiqueta: 'Impactos esperados', valor: '—' },
  { etiqueta: 'Heridas esperadas', valor: '—' },
  { etiqueta: 'Salvaciones del enemigo', valor: '—' },
  { etiqueta: 'Prob. de matar unidad (%)', valor: '—' },
  { etiqueta: 'Miniaturas eliminadas', valor: '—' },
  { etiqueta: 'Daño medio esperado', valor: '—' },
  { etiqueta: 'P25 (Rango mínimo)', valor: '—', amplio: true },
  { etiqueta: 'P75 (Rango máximo)', valor: '—', amplio: true },
]

export function PanelResultados() {
  return (
    <section className="resultados" aria-label="Resultados de la simulación">
      <div className="resultados__cabecera"><span>RESULTADOS DE COMBATE</span></div>
      <div className="resultados__rejilla">
        {metricas.map((metrica) => (
          <article key={metrica.etiqueta} className={`resultado ${metrica.amplio ? 'resultado--amplio' : ''}`}>
            <p className="resultado__etiqueta">{metrica.etiqueta}</p>
            <p className="resultado__valor">{metrica.valor}</p>
          </article>
        ))}
      </div>
    </section>
  )
}