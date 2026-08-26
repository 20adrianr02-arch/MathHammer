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
- **Última actualización:** 2026-08-26 22:07 CEST
- **Tarea:** Inicializar Git, enlazar el repositorio remoto y crear el primer commit.
- **Objetivo inmediato:** Mantener el proyecto sincronizado con GitHub sin sobrescribir historial remoto.
- **Bloqueos:** Ninguno conocido.
- **Próxima acción:** Confirmar el alcance de la primera tarea de desarrollo.

## Contexto del proyecto

- **Tipo:** Página web estática de Ángeles Sangrientos (Blood Angels / WH40k).
- **Tecnologías previstas:** HTML, CSS, JavaScript vanilla y Three.js mediante CDN.
- **Servidor local:** `python3 -m http.server 8080`.
- **URL local:** `http://localhost:8080`.
- **Estado de implementación:** Pendiente de confirmar; actualmente no se han
  detectado archivos de aplicación en la raíz durante esta sesión.

## Objetivos y alcance

### Objetivos activos

- Pendiente de definir con el usuario.

### Fuera de alcance

- Nada definido todavía.

## Inventario de archivos

| Archivo | Propósito | Estado | Última modificación registrada |
|---|---|---|---|
| `CONTEXT.md` | Memoria persistente de sesiones | Completado | 2026-08-26 22:07 CEST |
| `AGENTS.md` | Instrucciones para agentes y contexto inicial | Completado | 2026-08-26 21:49 CEST |
| `opencode.json` | Configuración de OpenCode y MCP | Existente | Pendiente de confirmar |
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

## Verificaciones realizadas

- La carpeta de trabajo es `C:\Users\20adr\Desktop\Poyecto MathHammer`.
- Se comprobó la existencia del archivo `CONTEXT.md`.
- Se comprobó la configuración existente en `AGENTS.md` y `opencode.json`.
- Git: no disponible en esta carpeta porque no se detectó un repositorio (`.git`).
- Git: repositorio inicializado, remoto `origin` configurado y rama `main` publicada en GitHub.
- Pruebas de la aplicación: No aplican todavía; no se detectó una aplicación implementada.

## Traspaso a la siguiente sesión

- **Estado al cerrar:** Repositorio local y remoto sincronizados correctamente.
- **Última tarea realizada:** Publicación del estado inicial en GitHub.
- **Archivos modificados en esta sesión:** `AGENTS.md`, `CONTEXT.md`, `opencode.json`, `README.md`, `LICENSE`.
- **Decisiones pendientes:** Ninguna relacionada con esta tarea.
- **Bloqueos:** Ninguno conocido.
- **Siguiente paso recomendado:** Preguntar o confirmar la primera funcionalidad de la página y revisar el estado completo del repositorio.

## Historial resumido

Sin entradas archivadas.
