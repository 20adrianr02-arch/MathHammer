interface PropiedadesCampo {
  etiqueta: string
  nombre: string
  tipo?: 'texto' | 'number'
  valor: string
  alCambiar: (valor: string) => void
  sufijo?: string
}

export function CampoCombate({ etiqueta, nombre, tipo = 'number', valor, alCambiar, sufijo }: PropiedadesCampo) {
  return (
    <label className="campo">
      <span className="campo__etiqueta">{etiqueta}</span>
      <span className="campo__control">
        <input name={nombre} type={tipo} value={valor} onChange={(evento) => alCambiar(evento.target.value)} />
        {sufijo && <span className="campo__sufijo">{sufijo}</span>}
      </span>
    </label>
  )
}

interface PropiedadesSelector {
  etiqueta: string
  nombre: string
  valor: string
  opciones: string[]
  alCambiar: (valor: string) => void
  marcadorVacio?: string
}

export function SelectorCombate({ etiqueta, nombre, valor, opciones, alCambiar, marcadorVacio }: PropiedadesSelector) {
  return (
    <label className="campo">
      <span className="campo__etiqueta">{etiqueta}</span>
      <select name={nombre} value={valor} onChange={(evento) => alCambiar(evento.target.value)}>
        {marcadorVacio && <option value="" disabled>{marcadorVacio}</option>}
        {opciones.map((opcion) => <option key={opcion} value={opcion}>{opcion}</option>)}
      </select>
    </label>
  )
}

interface PropiedadesHabilidad {
  texto: string
  activa: boolean
  alCambiar: (activa: boolean) => void
  selector?: boolean
  valorSelector?: string
  alCambiarSelector?: (valor: string) => void
  opcionesSelector?: string[]
  etiquetaSelector?: string
}

export function HabilidadCombate({
  texto,
  activa,
  alCambiar,
  selector = false,
  valorSelector,
  alCambiarSelector,
  opcionesSelector = ['1', '2', '3'],
  etiquetaSelector = 'Cantidad de golpes sostenidos',
}: PropiedadesHabilidad) {
  return (
    <label className={`habilidad ${activa ? 'habilidad--activa' : ''}`}>
      <input type="checkbox" checked={activa} onChange={(evento) => alCambiar(evento.target.checked)} />
      <span className="habilidad__marca" />
      <span>{texto}</span>
      {selector && activa && valorSelector && alCambiarSelector && (
        <select aria-label={etiquetaSelector} value={valorSelector} onChange={(evento) => alCambiarSelector(evento.target.value)}>
          {opcionesSelector.map((opcion) => <option key={opcion} value={opcion}>{opcion}</option>)}
        </select>
      )}
    </label>
  )
}
