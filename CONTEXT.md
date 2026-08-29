# CONTEXT.md — Contexto persistente del proyecto

> **Propósito:** memoria operativa para que cualquier agente o sesión pueda
> continuar el trabajo sin depender del historial de conversación. Este archivo
> debe reflejar el estado real del repositorio, no planes desactualizados.

## Reglas obligatorias de mantenimiento

1. **Al iniciar una tarea:** lee este archivo y actualiza **Estado actual** con el
   objetivo, el alcance y la fecha/hora de inicio.
2. **Antes de modificar archivos:** revisa el estado del repositorio y confirma
   que los cambios existentes no pertenecen a otra tarea.
3. **Después de cada paso importante:** registra en **Registro de pasos** la fecha,
   hora, acción, archivos afectados y resultado.
4. **Cada vez que cambie un archivo:** actualiza su fila en **Inventario de
   archivos**. Añade archivos nuevos y elimina los que ya no existan.
5. **Toda decisión relevante:** regístrala en **Decisiones clave**, incluyendo el
   motivo y sus consecuencias.
6. **Al terminar:** ejecuta las verificaciones apropiadas, actualiza el estado y
   completa **Traspaso a la siguiente sesión**.
7. **No inventes información:** si un dato no está confirmado, márcalo como
   `Pendiente de confirmar`.
8. **Mantén el archivo conciso:** cuando crezca demasiado, mueve los registros
   antiguos a **Historial resumido**, conservando decisiones, resultados y
   referencias necesarias para reconstruir el contexto.

## Convenciones

- Conversación y registros: español.
- Fechas: `YYYY-MM-DD HH:MM` con zona horaria local.
- Estados permitidos: `Pendiente`, `En curso`, `Bloqueado`, `Completado`,
  `Descartado`.
- Las rutas se escriben relativas a la raíz del proyecto.
- No borres entradas del registro; corrige errores con una nueva entrada.

## Estado actual

- **Estado:** Completado
- **Última actualización:** 2026-08-29 17:30 CEST
- **Tarea:** Módulo 6: aplicar las habilidades ofensivas y defensivas; contrato v1.3.
- **Objetivo inmediato:** Simulador completo con habilidades, métricas por simulación, FNP con desplegable en el front y contrato actualizado.
- **Bloqueos:** Ninguno conocido.
- **Próxima acción:** Conectar el frontend al endpoint y revisar el flujo completo.

## Contexto del proyecto

- **Tipo:** Calculadora de probabilidades y simulación de combate para Warhammer 40k (10ª Edición).
- **Backend:** ASP.NET Core (.NET 8/9) con Minimal APIs, pruebas con xUnit + FluentAssertions.
- **Frontend:** React 19 + TypeScript + Tailwind CSS (Vite), gráficos con Recharts.
- **Orquestación:** agente `techlead` (padre) + subagentes `backend` y `frontend`.
- **Estado de implementación:** Maqueta web estática inicial completada; backend y lógica de combate siguen pendientes.

## Objetivos y alcance

### Objetivos activos

- Continuar con la auditoría visual y técnica del frontend por `techlead`.

### Fuera de alcance

- Nada definido todavía.

## Inventario de archivos

| Archivo | Propósito | Estado | Última modificación registrada |
|---|---|---|---|
| `CONTEXT.md` | Memoria persistente de sesiones | En curso | 2026-08-27 18:28 CEST |
| `AGENTS.md` | Instrucciones para agentes y contexto inicial | Completado | 2026-08-26 22:19 CEST |
| `opencode.json` | Configuración de OpenCode, MCP y agente por defecto (en la raíz) | Completado | 2026-08-26 22:39 CEST |
| `.opencode/agents/techlead.md` | Agente padre: planifica, delega, audita e integra | Completado | 2026-08-26 22:19 CEST |
| `.opencode/agents/backend.md` | Agente backend (ASP.NET Core / C#) | Completado | 2026-08-26 22:19 CEST |
| `.opencode/agents/frontend.md` | Agente frontend (React / TypeScript) | Completado | 2026-08-26 22:19 CEST |
| `.opencode/context7.key` | API key de Context7 (secreto, ignorado por git) | Completado | 2026-08-26 22:30 CEST |
| `.gitignore` | Exclusiones de git (secretos, bin/, node_modules/) | Completado | 2026-08-26 22:30 CEST |
| `README.md` | Presentación inicial y estado del proyecto | Completado | 2026-08-26 23:18 CEST |
| `LICENSE` | Licencia MIT del proyecto | Existente | 2026-08-26 22:04 CEST |
| `BACKLOG.md` | Historias de usuario del proyecto | Completado | 2026-08-26 22:50 CEST |
| `docs/contrato-api.md` | Contrato JSON de entrada y salida de la API de combate | Completado | 2026-08-29 17:30 CEST |
| `.opencode/.gitignore` | Exclusiones de dependencias locales de OpenCode | Completado | 2026-08-26 23:18 CEST |
| `.opencode/package.json` | Dependencia local del plugin de OpenCode | Completado | 2026-08-26 23:18 CEST |
| `.opencode/package-lock.json` | Versiones bloqueadas de dependencias de OpenCode | Completado | 2026-08-26 23:18 CEST |
| `src/MathHammer.Api/Contratos/` | Futura ubicación de DTOs del backend | Completado | 2026-08-26 23:10 CEST |
| `src/MathHammer.Api/Reglas/` | Futura ubicación de reglas de combate | Completado | 2026-08-26 23:10 CEST |
| `src/MathHammer.Api/Simulacion/` | Futura ubicación del motor de simulación | Completado | 2026-08-26 23:10 CEST |
| `tests/MathHammer.Pruebas/Reglas/` | Futura ubicación de pruebas de reglas | Completado | 2026-08-26 23:10 CEST |
| `tests/MathHammer.Pruebas/Simulacion/` | Futura ubicación de pruebas de simulación | Completado | 2026-08-26 23:10 CEST |
| `global.json` | Fija el SDK de .NET a `9.0.203` | Completado | 2026-08-28 12:00 CEST |
| `MathHammer.sln` | Solución del backend .NET | Completado | 2026-08-28 12:00 CEST |
| `src/MathHammer.Api/MathHammer.Api.csproj` | Proyecto web minimal API | Completado | 2026-08-28 12:00 CEST |
| `src/MathHammer.Api/Program.cs` | Punto de entrada minimal API | Completado | 2026-08-28 12:00 CEST |
| `src/MathHammer.Api/Reglas/CalculadoraProbabilidades.cs` | Probabilidad de éxito/falmente en 1D6 | Completado | 2026-08-28 12:00 CEST |
| `src/MathHammer.Api/Reglas/DistribucionBinomial.cs` | Éxitos esperados, exacta y acumulada binomial | Completado | 2026-08-28 12:00 CEST |
| `src/MathHammer.Api/Reglas/ReglaHerida.cs` | Tabla de herida Fuerza vs Resistencia (2+..6+) | Completado | 2026-08-29 14:20 CEST |
| `src/MathHammer.Api/Reglas/ReglaSalvacion.cs` | Salvación armadura/invulnerable acotada (2+..7+) | Completado | 2026-08-29 14:20 CEST |
| `src/MathHammer.Api/Contratos/PeticionCombate.cs` | DTO petición de combate | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/PerfilAtacante.cs` | DTO atacante | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/PerfilArma.cs` | DTO arma | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/HabilidadesArma.cs` | DTO habilidades del arma | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/PerfilDefensor.cs` | DTO defensor | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/ConfiguracionSimulacion.cs` | DTO configuración de simulación | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/ResultadoCombate.cs` | DTO respuesta de combate | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/ResumenSimulacion.cs` | DTO resumen de la simulación | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/ValidadorPeticion.cs` | Validación fail-fast de la petición | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Contratos/MapeadorPeticion.cs` | Mapeo petición → perfil base | Completado | 2026-08-29 16:00 CEST |
| `src/MathHammer.Api/Simulacion/GeneradorAleatorio.cs` | Generador aleatorio con semilla reproducible | Completado | 2026-08-29 14:45 CEST |
| `src/MathHammer.Api/Simulacion/PerfilCombateBase.cs` | Perfil de combate base (sin habilidades) | Eliminado | 2026-08-29 17:30 CEST |
| `src/MathHammer.Api/Simulacion/PerfilCombate.cs` | Perfil completo con habilidades | Completado | 2026-08-29 17:30 CEST |
| `src/MathHammer.Api/Simulacion/ResultadoIteracion.cs` | Heridas infligidas y miniaturas destruidas por iteración | Completado | 2026-08-29 14:45 CEST |
| `src/MathHammer.Api/Simulacion/SimuladorCombate.cs` | Simulador Monte Carlo de la secuencia base | Completado | 2026-08-29 14:45 CEST |
| `src/MathHammer.Api/Simulacion/ResultadoMetricas.cs` | Las 8 métricas del panel de resultados | Completado | 2026-08-29 15:10 CEST |
| `src/MathHammer.Api/Simulacion/CalculadoraMetricas.cs` | Medios analíticos, letalidad y percentiles | Completado | 2026-08-29 15:10 CEST |
| `tests/MathHammer.Pruebas/MathHammer.Pruebas.csproj` | Proyecto xUnit + FluentAssertions | Completado | 2026-08-28 12:00 CEST |
| `tests/MathHammer.Pruebas/Reglas/CalculadoraProbabilidadesPruebas.cs` | Pruebas de probabilidad en 1D6 | Completado | 2026-08-28 12:00 CEST |
| `tests/MathHammer.Pruebas/Reglas/DistribucionBinomialPruebas.cs` | Pruebas de distribución binomial | Completado | 2026-08-28 12:00 CEST |
| `tests/MathHammer.Pruebas/Reglas/ReglaHeridaPruebas.cs` | Pruebas de la tabla de herida | Completado | 2026-08-29 14:20 CEST |
| `tests/MathHammer.Pruebas/Reglas/ReglaSalvacionPruebas.cs` | Pruebas de la salvación | Completado | 2026-08-29 14:20 CEST |
| `tests/MathHammer.Pruebas/Simulacion/GeneradorAleatorioPruebas.cs` | Pruebas de semilla y rango del dado | Completado | 2026-08-29 14:45 CEST |
| `tests/MathHammer.Pruebas/Simulacion/SimuladorCombatePruebas.cs` | Pruebas de convergencia, no-spillover y reproducibilidad | Completado | 2026-08-29 14:45 CEST |
| `tests/MathHammer.Pruebas/Simulacion/CalculadoraMetricasPruebas.cs` | Pruebas de las métricas del panel | Completado | 2026-08-29 15:10 CEST |
| `tests/MathHammer.Pruebas/Contratos/MapeadorPeticionPruebas.cs` | Pruebas del mapeo petición → perfil | Completado | 2026-08-29 16:00 CEST |
| `tests/MathHammer.Pruebas/Contratos/ValidadorPeticionPruebas.cs` | Pruebas de validación de la petición | Completado | 2026-08-29 16:00 CEST |
| `frontend/MathHammer.Web/package.json` | Dependencias y scripts del proyecto web | Completado | 2026-08-27 00:28 CEST |
| `frontend/MathHammer.Web/package-lock.json` | Bloqueo de dependencias del proyecto web | Completado | 2026-08-27 00:28 CEST |
| `frontend/MathHammer.Web/index.html` | Documento HTML de entrada de Vite | Completado | 2026-08-27 00:27 CEST |
| `frontend/MathHammer.Web/tsconfig.json` | Referencias de configuración TypeScript | Completado | 2026-08-27 00:27 CEST |
| `frontend/MathHammer.Web/tsconfig.app.json` | Configuración TypeScript de la aplicación | Completado | 2026-08-27 00:27 CEST |
| `frontend/MathHammer.Web/tsconfig.node.json` | Configuración TypeScript de Vite | Completado | 2026-08-27 00:27 CEST |
| `frontend/MathHammer.Web/vite.config.ts` | Configuración de Vite y React | Completado | 2026-08-27 00:27 CEST |
| `frontend/MathHammer.Web/src/main.tsx` | Punto de entrada React | Completado | 2026-08-27 00:27 CEST |
| `frontend/MathHammer.Web/src/Aplicacion.tsx` | Composición de la maqueta principal | Completado | 2026-08-28 11:40 CEST |
| `frontend/MathHammer.Web/src/componentes/ControlesCombate.tsx` | Controles visuales reutilizables | Completado | 2026-08-28 11:40 CEST |
| `frontend/MathHammer.Web/src/componentes/PanelAtacante.tsx` | Panel interactivo del atacante | Completado | 2026-08-28 11:40 CEST |
| `frontend/MathHammer.Web/src/componentes/PanelDefensor.tsx` | Panel interactivo del defensor | Completado | 2026-08-28 09:50 CEST |
| `frontend/MathHammer.Web/src/componentes/PanelResultados.tsx` | Tarjetas de métricas de resultados | Completado | 2026-08-28 10:25 CEST |
| `frontend/MathHammer.Web/src/componentes/SelectorTema.tsx` | Selector de temas de color dinámicos | Completado | 2026-08-28 10:50 CEST |
| `frontend/MathHammer.Web/src/estilos.css` | Estilos visuales grimdark responsive | Completado | 2026-08-28 11:20 CEST |
| `frontend/MathHammer.Web/src/vite-env.d.ts` | Tipos de entorno de Vite | Completado | 2026-08-27 00:28 CEST |
| `mobile/MathHammer.App/lib/` | Futura ubicación del código fuente Flutter | Completado | 2026-08-26 23:10 CEST |
| `mobile/MathHammer.App/test/` | Futura ubicación de pruebas Flutter | Completado | 2026-08-26 23:10 CEST |
| `mobile/MathHammer.App/android/` | Futura configuración nativa Android | Completado | 2026-08-26 23:10 CEST |
| `mobile/MathHammer.App/ios/` | Futura configuración nativa iOS | Completado | 2026-08-26 23:10 CEST |

> Si aparecen archivos de aplicación, pruebas, documentación o configuración,
> añádelos aquí cuando se inspeccionen o modifiquen.

## Decisiones clave

| Fecha | Decisión | Motivo | Consecuencia |
|---|---|---|---|
| 2026-08-26 21:38 CEST | Mejorar el `contexto_sesion.md` existente en vez de crear una segunda memoria. | Ya había un archivo con ese propósito y duplicarlo podía producir estados contradictorios. | Las sesiones deben usar este archivo como fuente principal. |
| 2026-08-26 21:38 CEST | Mantener inicialmente el nombre y ubicación existentes del archivo. | Evitaba cambios de rutas no solicitados en ese momento. | La decisión fue revisada posteriormente al solicitarse el renombrado a `CONTEXT.md`. |
| 2026-08-26 21:58 CEST | Usar `CONTEXT.md` como nombre definitivo de la memoria persistente. | Facilita identificar el archivo de contexto en cualquier sesión. | `AGENTS.md` y la documentación actual apuntan a `CONTEXT.md`. |
| 2026-08-26 22:19 CEST | Orquestar el desarrollo con un agente padre (`techlead`) y dos hijos (`backend`, `frontend`). | Separa dominios independientes (C# y React) y deja una figura que audita contra el DoD e integra. | `techlead` es el agente por defecto y el único que committea; los hijos entregan código sin integrar. |
| 2026-08-26 22:30 CEST | Guardar la API key de Context7 en `.opencode/context7.key` e inyectarla con `{file:...}`. | Evita exponer el secreto en `opencode.json` o en variables de entorno del sistema; queda excluido por `.gitignore`. | El header usa `CONTEXT7_API_KEY` leído del archivo local; el secreto no se versiona. |
| 2026-08-26 22:35 CEST | Mover `opencode.json` a `.opencode/opencode.json` y ajustar la ruta a `{file:context7.key}`. | Centraliza la configuración en `.opencode/`; `{file:...}` resuelve rutas relativas al directorio del config. | La referencia quedó correcta; verificado con `curl` contra el MCP (HTTP 200, autenticación válida). |
| 2026-08-26 22:39 CEST | Revertir la ubicación del config a la raíz: `opencode.json` no se carga desde `.opencode/`. | La documentación oficial de opencode solo carga el config de proyecto desde `opencode.json` en la raíz; `.opencode/` es para agents/commands/plugins. | El MCP y `default_agent` volvieron a la ubicación soportada; la clave sigue en `.opencode/context7.key` vía `{file:.opencode/context7.key}`. |

## Registro de pasos

| Fecha y hora | Acción | Archivos afectados | Resultado |
|---|---|---|---|
| 2026-08-26 21:38 CEST | Inspección inicial de la carpeta y lectura de la configuración existente. | `contexto_sesion.md`, `AGENTS.md`, `opencode.json` | Se confirmó que ya existía un borrador de memoria y no había README ni archivos de aplicación visibles. |
| 2026-08-26 21:38 CEST | Reestructuración de la memoria persistente con reglas, estado, inventario, decisiones, registro y traspaso. | `contexto_sesion.md` | Completado. |
| 2026-08-26 21:40 CEST | Verificación final del documento y del entorno. | `contexto_sesion.md` | El contenido es válido; Git no detecta un repositorio en esta carpeta. |
| 2026-08-26 21:49 CEST | Inicio de la actualización de las instrucciones del agente con la especificación de MathHammer. | `contexto_sesion.md`, `AGENTS.md` | En curso. |
| 2026-08-26 21:50 CEST | Verificación de `AGENTS.md` y actualización del cierre de sesión. | `AGENTS.md`, `contexto_sesion.md` | Las instrucciones están completas y la ruta de memoria quedó unificada en la raíz. |
| 2026-08-26 21:57 CEST | Renombrado de la memoria persistente. | `CONTEXT.md`, `AGENTS.md` | Completado; se actualizaron las referencias activas. |
| 2026-08-26 21:58 CEST | Verificación final de nombre, referencias y contenido. | `CONTEXT.md`, `AGENTS.md` | Completado; no quedan referencias activas al nombre anterior. |
| 2026-08-26 22:05 CEST | Inicio de la configuración del repositorio Git y enlace con GitHub. | `CONTEXT.md` | En curso. |
| 2026-08-26 22:06 CEST | Creación del commit local inicial `5810454`. | `AGENTS.md`, `CONTEXT.md`, `opencode.json` | Completado. |
| 2026-08-26 22:07 CEST | Integración del historial remoto existente y publicación en GitHub. | `README.md`, `LICENSE` | Completado mediante merge `197c57d`; `main` quedó enlazada con `origin/main`. |
| 2026-08-26 22:19 CEST | Creación de los agentes `techlead`, `backend` y `frontend` en `.opencode/agents/`. | `.opencode/agents/techlead.md`, `.opencode/agents/backend.md`, `.opencode/agents/frontend.md` | Completado. |
| 2026-08-26 22:19 CEST | Registro de `techlead` como agente por defecto y documentación de la orquestación. | `opencode.json`, `AGENTS.md` | Completado. |
| 2026-08-26 22:30 CEST | Corrección del header de Context7 a `CONTEXT7_API_KEY`, alta de `.gitignore` y de la clave local. | `opencode.json`, `.gitignore`, `.opencode/context7.key` | Completado; JSON válido y clave excluida de git. |
| 2026-08-26 22:39 CEST | Reubicación de `opencode.json` a la raíz tras confirmar que `.opencode/opencode.json` no se carga. | `opencode.json`, `CONTEXT.md` | Completado; config en raíz con `{file:.opencode/context7.key}`. |
| 2026-08-26 22:45 CEST | Creación de `BACKLOG.md` como plantilla de historias de usuario, a la espera de que el usuario las proporcione. | `BACKLOG.md`, `CONTEXT.md` | Completado; plantilla con formato de historia, criterios de aceptación y estados. |
| 2026-08-26 22:50 CEST | Incorporación de HU01 a HU13, organizadas en cuatro épicas, con sus criterios de aceptación y estado inicial. | `BACKLOG.md`, `CONTEXT.md` | Completado; backlog inicial definido por el usuario. |
| 2026-08-26 22:58 CEST | Inicio de la definición del contrato de datos de la API tomando como alcance el backlog y las decisiones previas. | `CONTEXT.md` | En curso; se fijó el alcance de una petición con un arma y dos histogramas de salida. |
| 2026-08-26 23:02 CEST | Documentación del contrato JSON, endpoint, validaciones, respuestas de error, métricas e histogramas. | `docs/contrato-api.md`, `CONTEXT.md` | Completado; contrato v1 listo para backend y frontend. |
| 2026-08-26 23:04 CEST | Explicitación de los nombres `PeticionCombate` y `ResultadoCombate` como DTOs del contrato. | `docs/contrato-api.md`, `CONTEXT.md` | Completado; ambos DTOs quedan identificados de forma directa. |
| 2026-08-26 23:08 CEST | Creación de las carpetas base de backend, pruebas, web y móvil con `.gitkeep` vacíos. | `src/`, `tests/`, `frontend/`, `mobile/`, `CONTEXT.md` | Completado; no se crearon proyectos ni código. |
| 2026-08-26 23:18 CEST | Corrección de exclusiones de `.opencode`, ampliación del README y aclaración de repeticiones en el contrato. | `.opencode/.gitignore`, `README.md`, `docs/contrato-api.md`, `CONTEXT.md` | Completado; las dependencias declarativas quedan versionables y el contrato distingue repeticiones generales de `Twin-linked`. |
| 2026-08-27 00:12 CEST | Ajuste del contrato para el frontend: nombres de unidad, `impactaA`, selector `disparo`/`melee`, cobertura y `Lance` con `+1` a herir. | `docs/contrato-api.md`, `CONTEXT.md` | Completado; contrato alineado con el diseño visual inicial. |
| 2026-08-27 00:14 CEST | Inicio de la maqueta visual estática web tras revisar estado Git y documentación obligatoria. | `CONTEXT.md` | Se confirmó que solo existían los `.gitkeep` del frontend y que las modificaciones previas del contrato no pertenecían a esta tarea. |
| 2026-08-27 00:27 CEST | Creación del proyecto Vite y componentes de la maqueta responsive de atacante y defensor. | `frontend/MathHammer.Web/` | Completado sin llamadas HTTP, cálculos ni comportamiento funcional. |
| 2026-08-27 00:28 CEST | Instalación de dependencias y compilación de producción. | `frontend/MathHammer.Web/package-lock.json` | `npm install` y `npm run build` completados correctamente; 0 vulnerabilidades reportadas. |
| 2026-08-27 18:28 CEST | Auditoría de la maqueta y desactivación de controles para preservar el carácter estático solicitado. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/componentes/ControlesCombate.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; sin llamadas HTTP, cálculos ni interacción funcional. |
| 2026-08-27 18:45 CEST | Simplificación de la cabecera, eliminación del bloque introductorio y activación del estado local controlado del formulario. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/componentes/ControlesCombate.tsx`, `frontend/MathHammer.Web/src/componentes/PanelAtacante.tsx`, `frontend/MathHammer.Web/src/componentes/PanelDefensor.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; campos, selectores y habilidades son editables localmente, sin API ni cálculos. |
| 2026-08-27 18:52 CEST | Cambio de tipografías, centrado de `MATH HAMMER`, cierre y reinicio del servidor Vite en `5173`. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` y respuesta HTTP `200` verificados. |
| 2026-08-28 09:20 CEST | Sustitución de `Cinzel Decorative` por `Caslon Antique` en el título y mantenimiento de `Rajdhani` para la interfaz. | `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` y respuesta HTTP `200` verificados. |
| 2026-08-28 09:50 CEST | Rediseño completo del frontend: `Friz Quadrata`, `MathHammer` en una palabra, bordes rojos superiores/inferiores, cabeceras blancas, habilidades ampliadas, acentos degradados, pie de formulario y panel de resultados. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/componentes/PanelAtacante.tsx`, `frontend/MathHammer.Web/src/componentes/PanelDefensor.tsx`, `frontend/MathHammer.Web/src/componentes/PanelResultados.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` correcto y servidor Vite operativo en `5173`. |
| 2026-08-28 10:00 CEST | Actualización del contrato a `1.1`: eliminados `tipoAtaque`, `cobertura` y modificadores genéricos; añadidas repeticiones y defensas booleanas alineadas con la interfaz. | `docs/contrato-api.md`, `CONTEXT.md` | Completado; contrato sincronizado con el frontend. |
| 2026-08-28 10:25 CEST | Sistema de temas dinámicos con variables CSS, selector desplegable, fuente `Caslon Antique Bold` y homogeneización de resultados y legibilidad. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/componentes/SelectorTema.tsx` (nuevo), `frontend/MathHammer.Web/src/componentes/PanelResultados.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` correcto y servidor Vite operativo en `5173`. |
| 2026-08-28 10:35 CEST | Título `MathHammer` con `Cinzel` (pesos 600-800), `Math` gris `#a1a1aa` y `Hammer` rojo `#c3272b`; interfaz con `Inter`; fondo `#030712` con degradado rojo. | `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` correcto. |
| 2026-08-28 10:50 CEST | Corrección de z-index del selector de temas, nomenclatura Citadel (Mephiston Red, Averland Sunset, Macragge Blue, Waaagh! Flesh, Abaddon Black), jerarquía tipográfica, estados activos y fondo ambiental con niebla animada. | `frontend/MathHammer.Web/src/componentes/SelectorTema.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` correcto. |
| 2026-08-28 11:05 CEST | Variables de tema movidas a `body` con sincronización vía `useEffect`; el humo de fondo y el título cambian de color según el tema, y la niebla inicia centrada detrás de `MathHammer`. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` correcto. |
| 2026-08-28 11:20 CEST | Reemplazo del humo por efecto táctico HUD: viñeta oscura, marcos de esquinas con el color del tema y barrido de escaneo auspex animado. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` correcto. |
| 2026-08-28 11:30 CEST | Resultados ocultos hasta el primer cálculo: el botón `CALCULAR COMBATE` queda activo y muestra el separador y el panel al pulsarlo. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/estilos.css`, `CONTEXT.md` | Completado; `npm run build` correcto. |
| 2026-08-28 11:40 CEST | Campos iniciales vacíos o en `0`; `IMPACTA A` con marcador vacío `—` mediante opción deshabilitada en el selector. | `frontend/MathHammer.Web/src/Aplicacion.tsx`, `frontend/MathHammer.Web/src/componentes/ControlesCombate.tsx`, `frontend/MathHammer.Web/src/componentes/PanelAtacante.tsx`, `CONTEXT.md` | Completado; `npm run build` correcto. |
| 2026-08-28 09:00 CEST | Inicio de correcciones visuales y del selector condicional de `Sustained Hits`. | `CONTEXT.md` | En curso; se mantiene el alcance exclusivamente web y local. |
| 2026-08-27 18:34 CEST | Inicio del servidor de desarrollo Vite en el puerto `5173` y comprobación HTTP. | `CONTEXT.md` | Completado; la aplicación responde con `HTTP 200`. |
| 2026-08-27 18:40 CEST | Inicio de la actualización visual y funcional solicitada para la pantalla web. | `CONTEXT.md` | En curso; se mantienen fuera de alcance el backend y los cálculos. |
| 2026-08-28 12:00 CEST | Módulo 0: `global.json` (SDK 9.0.203), `MathHammer.sln`, proyecto API minimal y proyecto xUnit con FluentAssertions. Módulo 1: `CalculadoraProbabilidades` y `DistribucionBinomial` con sus pruebas. | `global.json`, `MathHammer.sln`, `src/MathHammer.Api/*`, `tests/MathHammer.Pruebas/*`, `CONTEXT.md` | Completado; `dotnet build` sin errores y 19 pruebas en verde. |
| 2026-08-29 14:20 CEST | Módulo 2: `ReglaHerida` (tabla S vs T) y `ReglaSalvacion` (Sv/AP/invulnerable acotada a 2+..7+) con sus pruebas. | `src/MathHammer.Api/Reglas/ReglaHerida.cs`, `src/MathHammer.Api/Reglas/ReglaSalvacion.cs`, `tests/MathHammer.Pruebas/Reglas/ReglaHeridaPruebas.cs`, `tests/MathHammer.Pruebas/Reglas/ReglaSalvacionPruebas.cs`, `CONTEXT.md` | Completado; `dotnet build` sin errores y 46 pruebas en verde. |
| 2026-08-29 14:45 CEST | Módulo 3: `GeneradorAleatorio`, `PerfilCombateBase`, `ResultadoIteracion` y `SimuladorCombate` (secuencia base sin spillover) con pruebas de convergencia y reproducibilidad. | `src/MathHammer.Api/Simulacion/*`, `tests/MathHammer.Pruebas/Simulacion/*`, `CONTEXT.md` | Completado; `dotnet build` sin errores y 55 pruebas en verde. |
| 2026-08-29 15:10 CEST | Módulo 4: `ResultadoMetricas` y `CalculadoraMetricas` con las 8 métricas del panel (sin histogramas), usando medios analíticos + simulación. | `src/MathHammer.Api/Simulacion/ResultadoMetricas.cs`, `src/MathHammer.Api/Simulacion/CalculadoraMetricas.cs`, `tests/MathHammer.Pruebas/Simulacion/CalculadoraMetricasPruebas.cs`, `CONTEXT.md` | Completado; `dotnet build` sin errores y 62 pruebas en verde. |
| 2026-08-29 16:00 CEST | Módulo 5: DTOs de petición/respuesta, `ValidadorPeticion`, `MapeadorPeticion`, endpoint `POST /api/combate/simular` con camelCase, CORS y Problem Details; contrato actualizado a v1.2 (8 métricas, sin histogramas). | `src/MathHammer.Api/Contratos/*`, `src/MathHammer.Api/Program.cs`, `tests/MathHammer.Pruebas/Contratos/*`, `docs/contrato-api.md`, `CONTEXT.md` | Completado; 70 pruebas en verde y verificación manual con `curl`/PowerShell (200 y 422 correctos). |
| 2026-08-29 17:30 CEST | Módulo 6: habilidades ofensivas y defensivas en el simulador, `PerfilCombate`, métricas por medias de simulación, eliminación de `repiteParaHerir` y `repetirTiradaSalvacion`, FNP con desplegable en el front y contrato v1.3. | `src/MathHammer.Api/Simulacion/*`, `src/MathHammer.Api/Contratos/*`, `src/MathHammer.Api/Program.cs`, `tests/MathHammer.Pruebas/*`, `frontend/MathHammer.Web/src/*`, `docs/contrato-api.md`, `CONTEXT.md` | Completado; 78 pruebas en verde y `npm run build` correcto. |

## Verificaciones realizadas

- La carpeta de trabajo es `C:\Users\20adr\Desktop\MathHammer`.
- Se comprobó la existencia del archivo `CONTEXT.md`.
- Se comprobó la configuración existente en `AGENTS.md` y `opencode.json`.
- Git: repositorio inicializado, remoto `origin` configurado y rama `main` publicada en GitHub.
- Agentes: creados los tres archivos en `.opencode/agents/` (ubicación de auto-descubrimiento) y `techlead` registrado como `default_agent`.
- Context7: config en la raíz `opencode.json` válido, header `CONTEXT7_API_KEY` apuntando a `{file:.opencode/context7.key}`, clave excluida de git (verificado con `git check-ignore`).
- Context7 (remoto): `curl` contra `https://mcp.context7.com/mcp` con el header y la clave → HTTP 200 y `serverInfo` de Context7 v4.0.3 (autenticación válida).
- Pruebas de la aplicación: No aplican todavía; no se detectó una aplicación implementada.
- Backlog: 13 historias de usuario registradas en `BACKLOG.md`, todas con estado `Pendiente`.
- Contrato API: definido en `docs/contrato-api.md`, versión `1.2`, con una petición de un arma, 8 métricas de respuesta y sin histogramas.
- Estructura física: creadas las ramas base `src/`, `tests/`, `frontend/` y `mobile/`; los directorios se conservan en Git mediante `.gitkeep` vacíos.
- Configuración de OpenCode: `.opencode/.gitignore` conserva únicamente `node_modules/` como exclusión; los manifiestos de dependencias pueden versionarse.
- Documentación: `README.md` describe el estado actual y los próximos pasos.
- Frontend web: proyecto Vite compilado correctamente con maqueta responsive y estado local; no incluye servicios, API ni cálculos.
- Tipografía del título: `Cinzel` con `Math` gris `#a1a1aa` y `Hammer` rojo `#c3272b`; interfaz con `Inter`.
- Temas: rojo código, amarillo imperial, azul ultramar y verde tóxico, aplicados mediante variables CSS.
- Resultados: panel de tarjetas con encabezado `RESULTADOS DE COMBATE` y legibilidad en blanco.
- Servidor local: Vite responde en `http://localhost:5173` con `HTTP 200`.
- Backend .NET: solución compilada con SDK `9.0.203`; `dotnet build` sin errores y `dotnet test` con 78 pruebas en verde.
- Contrato API: `docs/contrato-api.md` v1.3, con 8 métricas de respuesta, habilidades aplicadas y sin histogramas.

## Traspaso a la siguiente sesión

- **Estado al cerrar:** Título con `Cinzel` e interfaz con `Inter` aplicados y servidos en el puerto `5173`; cambios sin commit.
- **Última tarea realizada:** Estilo `Cinzel`/`Inter` y fondo carbón con degradado rojo.
- **Archivos modificados en esta sesión:** `CONTEXT.md` y `frontend/MathHammer.Web/src/estilos.css`.
- **Decisiones pendientes:** Conectar los resultados al backend cuando exista; añadir el valor numérico de FNP cuando el selector esté disponible en el frontend.
- **Bloqueos:** Ninguno conocido.
- **Siguiente paso recomendado:** Revisar visualmente los cuatro temas y preparar la implementación del backend conforme al contrato `1.1`.

## Historial resumido

Sin entradas archivadas.
