import { CampoCombate, HabilidadCombate } from './ControlesCombate'

export interface PerfilDefensor {
  nombreUnidad: string
  resistencia: string
  heridasPorMiniatura: string
  cantidadMiniaturas: string
  salvacion: string
  salvacionInvulnerable: string
  habilidades: Record<string, boolean>
}

interface PropiedadesPanelDefensor {
  perfil: PerfilDefensor
  alCambiarCampo: (nombre: keyof PerfilDefensor, valor: string) => void
  alCambiarHabilidad: (nombre: string, activa: boolean) => void
}

export function PanelDefensor({ perfil, alCambiarCampo, alCambiarHabilidad }: PropiedadesPanelDefensor) {
  return (
    <article className="panel panel--defensor">
      <header className="panel__cabecera"><h3>DEFENSOR</h3></header>
      <div className="rejilla-campos">
        <CampoCombate etiqueta="Nombre de unidad" nombre="nombreDefensor" tipo="texto" valor={perfil.nombreUnidad} alCambiar={(valor) => alCambiarCampo('nombreUnidad', valor)} />
        <CampoCombate etiqueta="Resistencia" nombre="resistencia" valor={perfil.resistencia} alCambiar={(valor) => alCambiarCampo('resistencia', valor)} />
        <CampoCombate etiqueta="Heridas por miniatura" nombre="heridasPorMiniatura" valor={perfil.heridasPorMiniatura} alCambiar={(valor) => alCambiarCampo('heridasPorMiniatura', valor)} />
        <CampoCombate etiqueta="Cantidad de miniaturas" nombre="cantidadMiniaturas" valor={perfil.cantidadMiniaturas} alCambiar={(valor) => alCambiarCampo('cantidadMiniaturas', valor)} />
        <CampoCombate etiqueta="Salvación" nombre="salvacion" tipo="texto" valor={perfil.salvacion} alCambiar={(valor) => alCambiarCampo('salvacion', valor)} />
        <CampoCombate etiqueta="Salvación invulnerable" nombre="salvacionInvulnerable" tipo="texto" valor={perfil.salvacionInvulnerable} alCambiar={(valor) => alCambiarCampo('salvacionInvulnerable', valor)} />
      </div>
      <div className="subcabecera"><span>HABILIDADES DEFENSIVAS</span></div>
      <div className="habilidades habilidades--defensa">
        <HabilidadCombate texto="-1 AL DAÑO" activa={perfil.habilidades.reduccionDanio} alCambiar={(activa) => alCambiarHabilidad('reduccionDanio', activa)} />
        <HabilidadCombate texto="FEEL NO PAIN" activa={perfil.habilidades.sinDolor} alCambiar={(activa) => alCambiarHabilidad('sinDolor', activa)} />
        <HabilidadCombate texto="-1 AL IMPACTAR" activa={perfil.habilidades.penalizacionImpactar} alCambiar={(activa) => alCambiarHabilidad('penalizacionImpactar', activa)} />
        <HabilidadCombate texto="-1 AL HERIR" activa={perfil.habilidades.penalizacionHerir} alCambiar={(activa) => alCambiarHabilidad('penalizacionHerir', activa)} />
      </div>
    </article>
  )
}
