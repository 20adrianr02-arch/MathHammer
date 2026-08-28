import { CampoCombate, HabilidadCombate, SelectorCombate } from './ControlesCombate'

export interface PerfilAtacante {
  nombreUnidad: string
  cantidadAtaques: string
  impactaA: string
  fuerza: string
  penetracionArmadura: string
  danio: string
  habilidades: Record<string, boolean>
  golpesSostenidos: string
}

interface PropiedadesPanelAtacante {
  perfil: PerfilAtacante
  alCambiarCampo: (nombre: keyof PerfilAtacante, valor: string) => void
  alCambiarHabilidad: (nombre: string, activa: boolean) => void
}

export function PanelAtacante({ perfil, alCambiarCampo, alCambiarHabilidad }: PropiedadesPanelAtacante) {
  return (
    <article className="panel panel--atacante">
      <header className="panel__cabecera"><h3>ATACANTE</h3></header>
      <div className="rejilla-campos">
        <CampoCombate etiqueta="Nombre de unidad" nombre="nombreAtacante" tipo="texto" valor={perfil.nombreUnidad} alCambiar={(valor) => alCambiarCampo('nombreUnidad', valor)} />
        <CampoCombate etiqueta="Cantidad de ataques" nombre="cantidadAtaques" valor={perfil.cantidadAtaques} alCambiar={(valor) => alCambiarCampo('cantidadAtaques', valor)} />
        <SelectorCombate etiqueta="Impacta a" nombre="impactaA" valor={perfil.impactaA} opciones={['2+', '3+', '4+', '5+', '6+']} alCambiar={(valor) => alCambiarCampo('impactaA', valor)} />
        <CampoCombate etiqueta="Fuerza" nombre="fuerza" valor={perfil.fuerza} alCambiar={(valor) => alCambiarCampo('fuerza', valor)} />
        <CampoCombate etiqueta="AP" nombre="penetracionArmadura" valor={perfil.penetracionArmadura} alCambiar={(valor) => alCambiarCampo('penetracionArmadura', valor)} />
        <CampoCombate etiqueta="Daño" nombre="danio" valor={perfil.danio} alCambiar={(valor) => alCambiarCampo('danio', valor)} />
      </div>
      <div className="subcabecera"><span>HABILIDADES OFENSIVAS</span></div>
      <div className="habilidades">
        <HabilidadCombate texto="LETHAL HITS" activa={perfil.habilidades.impactosLetales} alCambiar={(activa) => alCambiarHabilidad('impactosLetales', activa)} />
        <HabilidadCombate texto="REPITE PARA IMPACTAR" activa={perfil.habilidades.repiteParaImpactar} alCambiar={(activa) => alCambiarHabilidad('repiteParaImpactar', activa)} />
        <HabilidadCombate texto="REPETIR PARA HERIR" activa={perfil.habilidades.repiteParaHerir} alCambiar={(activa) => alCambiarHabilidad('repiteParaHerir', activa)} />
        <HabilidadCombate texto="REPETIR 1 PARA HERIR" activa={perfil.habilidades.repiteUnoParaHerir} alCambiar={(activa) => alCambiarHabilidad('repiteUnoParaHerir', activa)} />
        <HabilidadCombate texto="LANCE (+1 AL HERIR)" activa={perfil.habilidades.lance} alCambiar={(activa) => alCambiarHabilidad('lance', activa)} />
        <HabilidadCombate texto="DEVASTATING WOUNDS" activa={perfil.habilidades.heridasDevastadoras} alCambiar={(activa) => alCambiarHabilidad('heridasDevastadoras', activa)} />
        <HabilidadCombate texto="SUSTAINED HITS" activa={perfil.habilidades.golpesSostenidos} alCambiar={(activa) => alCambiarHabilidad('golpesSostenidos', activa)} selector valorSelector={perfil.golpesSostenidos} alCambiarSelector={(valor) => alCambiarCampo('golpesSostenidos', valor)} />
      </div>
    </article>
  )
}
