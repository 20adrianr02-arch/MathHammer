import { useEffect, useState } from 'react'
import { PanelAtacante } from './componentes/PanelAtacante'
import type { PerfilAtacante } from './componentes/PanelAtacante'
import { PanelDefensor } from './componentes/PanelDefensor'
import type { PerfilDefensor } from './componentes/PanelDefensor'
import { PanelResultados } from './componentes/PanelResultados'
import { SelectorTema } from './componentes/SelectorTema'
import type { NombreTema } from './componentes/SelectorTema'

const perfilAtacanteInicial: PerfilAtacante = {
  nombreUnidad: 'Escuadra intercesora',
  cantidadAtaques: '8',
  impactaA: '3+',
  fuerza: '5',
  penetracionArmadura: '-2',
  danio: '1',
  habilidades: {
    impactosLetales: true,
    repiteParaImpactar: false,
    repiteParaHerir: false,
    repiteUnoParaHerir: false,
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
    reduccionDanio: false,
    sinDolor: false,
    penalizacionImpactar: false,
    penalizacionHerir: false,
  },
}

export function Aplicacion() {
  const [perfilAtacante, establecerPerfilAtacante] = useState(perfilAtacanteInicial)
  const [perfilDefensor, establecerPerfilDefensor] = useState(perfilDefensorInicial)
  const [tema, establecerTema] = useState<NombreTema>('rojo')

  useEffect(() => {
    document.body.dataset.tema = tema
  }, [tema])

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
      <div className="marcos-tacticos" aria-hidden="true">
        <span className="marco marco--sup-izq" />
        <span className="marco marco--sup-der" />
        <span className="marco marco--inf-izq" />
        <span className="marco marco--inf-der" />
      </div>
      <div className="barrido-tactico" aria-hidden="true" />
      <header className="cabecera">
        <h1><span className="titulo__math">Math</span><span className="titulo__hammer">Hammer</span></h1>
        <SelectorTema tema={tema} alCambiarTema={establecerTema} />
      </header>

      <section className="paneles-combate" aria-label="Configuración del combate">
        <PanelAtacante perfil={perfilAtacante} alCambiarCampo={cambiarCampoAtacante} alCambiarHabilidad={cambiarHabilidadAtacante} />
        <PanelDefensor perfil={perfilDefensor} alCambiarCampo={cambiarCampoDefensor} alCambiarHabilidad={cambiarHabilidadDefensor} />
      </section>

      <div className="accion">
        <button className="boton-calcular" type="button" disabled>CALCULAR COMBATE</button>
      </div>
      <div className="separador-pie" />
      <PanelResultados />
    </main>
  )
}
