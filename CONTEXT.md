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
- **Última actualización:** 2026-08-26 23:18 CEST
- **Tarea:** Mejorar la organización documental y de configuración sin implementar código.
- **Objetivo inmediato:** Dejar preparado el repositorio para crear posteriormente los proyectos ejecutables con configuración reproducible y contrato sin ambigüedades.
- **Bloqueos:** Ninguno conocido.
- **Próxima acción:** Crear la solución .NET, la aplicación web y la aplicación Flutter en una tarea independiente.

## Contexto del proyecto

- **Tipo:** Calculadora de probabilidades y simulación de combate para Warhammer 40k (10ª Edición).
- **Backend:** ASP.NET Core (.NET 8/9) con Minimal APIs, pruebas con xUnit + FluentAssertions.
- **Frontend:** React 19 + TypeScript + Tailwind CSS (Vite), gráficos con Recharts.
- **Orquestación:** agente `techlead` (padre) + subagentes `backend` y `frontend`.
- **Estado de implementación:** Pendiente de confirmar; aún no hay código de aplicación.

## Objetivos y alcance

### Objetivos activos

- Pendiente de definir con el usuario.

### Fuera de alcance

- Nada definido todavía.

## Inventario de archivos

| Archivo | Propósito | Estado | Última modificación registrada |
|---|---|---|---|
| `CONTEXT.md` | Memoria persistente de sesiones | En curso | 2026-08-26 23:18 CEST |
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
| `docs/contrato-api.md` | Contrato JSON de entrada y salida de la API de combate | Completado | 2026-08-26 23:04 CEST |
| `.opencode/.gitignore` | Exclusiones de dependencias locales de OpenCode | Completado | 2026-08-26 23:18 CEST |
| `.opencode/package.json` | Dependencia local del plugin de OpenCode | Completado | 2026-08-26 23:18 CEST |
| `.opencode/package-lock.json` | Versiones bloqueadas de dependencias de OpenCode | Completado | 2026-08-26 23:18 CEST |
| `src/MathHammer.Api/Contratos/` | Futura ubicación de DTOs del backend | Completado | 2026-08-26 23:10 CEST |
| `src/MathHammer.Api/Reglas/` | Futura ubicación de reglas de combate | Completado | 2026-08-26 23:10 CEST |
| `src/MathHammer.Api/Simulacion/` | Futura ubicación del motor de simulación | Completado | 2026-08-26 23:10 CEST |
| `tests/MathHammer.Pruebas/Reglas/` | Futura ubicación de pruebas de reglas | Completado | 2026-08-26 23:10 CEST |
| `tests/MathHammer.Pruebas/Simulacion/` | Futura ubicación de pruebas de simulación | Completado | 2026-08-26 23:10 CEST |
| `frontend/MathHammer.Web/src/` | Futura ubicación del código fuente web | Completado | 2026-08-26 23:10 CEST |
| `frontend/MathHammer.Web/public/` | Futura ubicación de recursos públicos web | Completado | 2026-08-26 23:10 CEST |
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
- Contrato API: definido en `docs/contrato-api.md`, versión `1.0`, con una petición de un arma y dos histogramas en la respuesta.
- Estructura física: creadas las ramas base `src/`, `tests/`, `frontend/` y `mobile/`; los directorios se conservan en Git mediante `.gitkeep` vacíos.
- Configuración de OpenCode: `.opencode/.gitignore` conserva únicamente `node_modules/` como exclusión; los manifiestos de dependencias pueden versionarse.
- Documentación: `README.md` describe el estado actual y los próximos pasos.

## Traspaso a la siguiente sesión

- **Estado al cerrar:** Mejoras de organización y documentación completadas; cambios por commitear.
- **Última tarea realizada:** Corrección de configuración, README, contexto y contrato sin crear código de aplicación.
- **Archivos modificados en esta sesión:** `.opencode/.gitignore`, `README.md`, `docs/contrato-api.md` y `CONTEXT.md`.
- **Decisiones pendientes:** Crear la solución .NET, el proyecto web y el proyecto Flutter en una tarea posterior.
- **Bloqueos:** Ninguno conocido.
- **Siguiente paso recomendado:** Reiniciar opencode y comprobar `/mcps` (Context7) y `/agents` (techlead, backend, frontend).

## Historial resumido

Sin entradas archivadas.
