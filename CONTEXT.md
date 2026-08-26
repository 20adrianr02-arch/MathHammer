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
- **Última actualización:** 2026-08-26 22:30 CEST
- **Tarea:** Enlazar y configurar el MCP de Context7.
- **Objetivo inmediato:** Dejar operativo Context7 con su API key protegida fuera de git.
- **Bloqueos:** Ninguno conocido.
- **Próxima acción:** Confirmar el alcance de la primera tarea de desarrollo.

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
| `CONTEXT.md` | Memoria persistente de sesiones | En curso | 2026-08-26 22:19 CEST |
| `AGENTS.md` | Instrucciones para agentes y contexto inicial | Completado | 2026-08-26 22:19 CEST |
| `opencode.json` | Configuración de OpenCode, MCP y agente por defecto | Completado | 2026-08-26 22:19 CEST |
| `.opencode/agents/techlead.md` | Agente padre: planifica, delega, audita e integra | Completado | 2026-08-26 22:19 CEST |
| `.opencode/agents/backend.md` | Agente backend (ASP.NET Core / C#) | Completado | 2026-08-26 22:19 CEST |
| `.opencode/agents/frontend.md` | Agente frontend (React / TypeScript) | Completado | 2026-08-26 22:19 CEST |
| `.opencode/context7.key` | API key de Context7 (secreto, ignorado por git) | Completado | 2026-08-26 22:30 CEST |
| `.gitignore` | Exclusiones de git (secretos, bin/, node_modules/) | Completado | 2026-08-26 22:30 CEST |
| `README.md` | Presentación inicial del proyecto | Existente | 2026-08-26 22:04 CEST |
| `LICENSE` | Licencia MIT del proyecto | Existente | 2026-08-26 22:04 CEST |

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

## Verificaciones realizadas

- La carpeta de trabajo es `C:\Users\20adr\Desktop\Poyecto MathHammer`.
- Se comprobó la existencia del archivo `CONTEXT.md`.
- Se comprobó la configuración existente en `AGENTS.md` y `opencode.json`.
- Git: repositorio inicializado, remoto `origin` configurado y rama `main` publicada en GitHub.
- Agentes: creados los tres archivos de agente y `techlead` registrado como `default_agent`.
- Context7: `opencode.json` válido, header `CONTEXT7_API_KEY` apuntando a `.opencode/context7.key`, clave excluida de git (verificado con `git check-ignore`).
- Pruebas de la aplicación: No aplican todavía; no se detectó una aplicación implementada.

## Traspaso a la siguiente sesión

- **Estado al cerrar:** Orquestación de agentes y MCP de Context7 configurados; cambios por commitear.
- **Última tarea realizada:** Enlace y configuración del MCP de Context7 con API key protegida.
- **Archivos modificados en esta sesión:** `AGENTS.md`, `CONTEXT.md`, `opencode.json`, `.gitignore` y `.opencode/agents/*`.
- **Decisiones pendientes:** Confirmar el contrato de API y la primera funcionalidad de desarrollo.
- **Bloqueos:** Ninguno conocido.
- **Siguiente paso recomendado:** Reiniciar opencode para cargar los agentes y Context7, y definir el contrato de API con el `techlead`.

## Historial resumido

Sin entradas archivadas.
