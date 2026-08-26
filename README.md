# MathHammer

Calculadora de probabilidades y simulación de combate para Warhammer 40.000,
10.ª edición.

## Estado actual

El proyecto se encuentra en fase de preparación. Actualmente están definidos:

- El backlog inicial en `BACKLOG.md`.
- El contrato de datos de la API en `docs/contrato-api.md`.
- La estructura de carpetas para backend, pruebas, web y móvil.
- La configuración de agentes y MCP de OpenCode.

Todavía no se han creado los proyectos ejecutables ni se ha implementado código
de aplicación.

## Estructura

- `src/`: backend ASP.NET Core con Minimal APIs.
- `tests/`: pruebas unitarias y matemáticas del backend.
- `frontend/`: aplicación web React, TypeScript y Vite.
- `mobile/`: aplicación móvil Flutter para Android e iOS.
- `docs/`: documentación técnica y contrato de la API.
- `.opencode/`: agentes y configuración local de OpenCode.

## Próximos pasos

1. Crear los proyectos mediante sus respectivas CLI.
2. Verificar que backend, pruebas, web y móvil compilan desde una estructura limpia.
3. Implementar los DTOs conforme al contrato de la API.
4. Implementar las reglas matemáticas y el motor de simulación.

Consulta `AGENTS.md` para las convenciones del proyecto y `CONTEXT.md` para el
estado persistente de trabajo.
