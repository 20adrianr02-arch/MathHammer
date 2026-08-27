import { useState } from 'react'
import { PanelAtacante } from './componentes/PanelAtacante'
import type { PerfilAtacante } from './componentes/PanelAtacante'
import { PanelDefensor } from './componentes/PanelDefensor'
import type { PerfilDefensor } from './componentes/PanelDefensor'

const perfilAtacanteInicial: PerfilAtacante = {
  nombreUnidad: 'Escuadra intercesora',
  tipoAtaque: 'DISPARO',
  cantidadAtaques: '8',
  impactaA: '3+',
  fuerza: '5',
  penetracionArmadura: '-2',
  danio: '1',
  habilidades: {
    impactosLetales: true,
    repiteParaImpactar: false,
    lance: true,
    heridasDevastadoras: false,
    golpesSostenidos: false,
  },
  golpesSostenidos: '2',
}

const perfilDefensorInicial: PerfilDefensor = {
  nombreUnidad: 'Guerreros Necrones',
  resistencia: '4',
  heridasPorMiniatura: '2',
  cantidadMiniaturas: '5',
  salvacion: '3+',
  salvacionInvulnerable: '5+',
  habilidades: {
    cobertura: true,
    reduccionDanio: false,
    sinDolor: false,
    penalizacionImpactar: false,
  },
}

export function Aplicacion() {
  const [perfilAtacante, establecerPerfilAtacante] = useState(perfilAtacanteInicial)
  const [perfilDefensor, establecerPerfilDefensor] = useState(perfilDefensorInicial)

  function cambiarCampoAtacante(nombre: keyof PerfilAtacante, valor: string) {
    if (nombre === 'habilidades') return
    establecerPerfilAtacante((perfilActual) => ({ ...perfilActual, [nombre]: valor }))
  }

  function cambiarHabilidadAtacante(nombre: string, activa: boolean) {
    establecerPerfilAtacante((perfilActual) => ({ ...perfilActual, habilidades: { ...perfilActual.habilidades, [nombre]: activa } }))
  }

  function cambiarCampoDefensor(nombre: keyof PerfilDefensor, valor: string) {
    if (nombre === 'habilidades') return
    establecerPerfilDefensor((perfilActual) => ({ ...perfilActual, [nombre]: valor }))
  }

  function cambiarHabilidadDefensor(nombre: string, activa: boolean) {
    establecerPerfilDefensor((perfilActual) => ({ ...perfilActual, habilidades: { ...perfilActual.habilidades, [nombre]: activa } }))
  }

  return (
    <main className="aplicacion">
      <header className="cabecera">
        <h1>MATH HAMMER</h1>
      </header>

      <section className="paneles-combate" aria-label="Configuración del combate">
        <PanelAtacante perfil={perfilAtacante} alCambiarCampo={cambiarCampoAtacante} alCambiarHabilidad={cambiarHabilidadAtacante} />
        <PanelDefensor perfil={perfilDefensor} alCambiarCampo={cambiarCampoDefensor} alCambiarHabilidad={cambiarHabilidadDefensor} />
      </section>

      <div className="accion">
        <button className="boton-calcular" type="button" disabled>CALCULAR COMBATE</button>
        <p>SIMULACIÓN ESTÁTICA // DATOS DE EJEMPLO</p>
      </div>
    </main>
  )
}
