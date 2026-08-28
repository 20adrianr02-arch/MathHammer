import { useState } from 'react'

export type NombreTema = 'rojo' | 'amarillo' | 'azul' | 'verde' | 'negro'

interface OpcionTema {
  id: NombreTema
  etiqueta: string
  color: string
}

const opcionesTema: OpcionTema[] = [
  { id: 'rojo', etiqueta: 'MEPHISTON RED', color: '#b3242a' },
  { id: 'amarillo', etiqueta: 'AVERLAND SUNSET', color: '#e8a33d' },
  { id: 'azul', etiqueta: 'MACRAGGE BLUE', color: '#1f4690' },
  { id: 'verde', etiqueta: 'WAAAGH! FLESH', color: '#3dff2e' },
  { id: 'negro', etiqueta: 'ABADDON BLACK', color: '#1a1d21' },
]

interface PropiedadesSelectorTema {
  tema: NombreTema
  alCambiarTema: (tema: NombreTema) => void
}

export function SelectorTema({ tema, alCambiarTema }: PropiedadesSelectorTema) {
  const [abierto, establecerAbierto] = useState(false)
  const opcionActiva = opcionesTema.find((opcion) => opcion.id === tema) ?? opcionesTema[0]

  return (
    <div className="selector-tema">
      <button
        type="button"
        className="selector-tema__boton"
        onClick={() => establecerAbierto(!abierto)}
        aria-haspopup="listbox"
        aria-expanded={abierto}
      >
        <span className="selector-tema__circulo" style={{ background: opcionActiva.color }} />
        <span>{opcionActiva.etiqueta}</span>
        <span className="selector-tema__flecha">▾</span>
      </button>
      {abierto && (
        <ul className="selector-tema__menu" role="listbox">
          {opcionesTema.map((opcion) => (
            <li key={opcion.id}>
              <button
                type="button"
                className={`selector-tema__opcion ${opcion.id === tema ? 'selector-tema__opcion--activa' : ''}`}
                onClick={() => {
                  alCambiarTema(opcion.id)
                  establecerAbierto(false)
                }}
              >
                <span className="selector-tema__circulo" style={{ background: opcion.color }} />
                <span>{opcion.etiqueta}</span>
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  )
}