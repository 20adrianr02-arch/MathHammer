# MathHammer

Calculadora de probabilidades y simulación de combate para Warhammer 40k (10ª Edición). Diseñada para jugadores y analistas tácticos que necesitan conocer el rendimiento estadístico exacto (daño medio, percentiles P25/P75 y probabilidad de eliminación de unidades) en tiempo real.

## 1. Stack Tecnológico

| Capa | Tecnología | Justificación |
| :--- | :--- | :--- |
| **Backend** | ASP.NET Core (.NET 8/9) con Minimal APIs | Alto rendimiento, sintaxis moderna en C# y arquitectura directa sin sobreingeniería. |
| **Pruebas (Testing)** | xUnit + FluentAssertions | Validación matemática del motor Monte Carlo y tests unitarios de reglas 40k. |
| **Frontend Web / PWA** | React 19 + TypeScript + Tailwind CSS (Vite) | Interfaz reactiva, tipado estricto, sin complejidad de SSR innecesaria y lista para PWA. |
| **Frontend Móvil (Alt.)** | Flutter (Dart) | Opción para generación de `.apk`/Android nativo si se requiere entrega móvil pura en DAM. |
| **Gráficos** | Recharts (React) / FL Chart (Flutter) | Renderizado de histogramas de frecuencia, distribución y marcas visuales de percentiles. |
| **DevOps & Cloud** | Docker + GitHub Actions + Render / Azure | Contenerización multi-stage y pipeline automatizado de integración continua. |

## 2. Convenciones de Código y Estilo

Todo el código debe parecer escrito por un **desarrollador Senior pragmático**: limpio, estructurado, directo y fácil de auditar por un perfil junior o graduado de FP DAM.

* **Idioma:** Todo el código de dominio (nombres de variables, métodos, clases, comentarios y mensajes de error) debe estar **100% en español**.
* **Nombres descriptivos:** Prohibidas las letras sueltas (`a`, `d`, `tmp`) o abreviaciones ambiguas. Usar nombres que expliquen la regla de negocio (ej. `cantidadAtaques`, `simularSecuenciaCombate`, `heridasRestantes`).
* **Estilo C# (.NET):**
  * `PascalCase` para Clases, Records, Métodos, Propiedades y Nombres de archivo (`SimuladorCombate.cs`).
  * `camelCase` para variables locales y parámetros (`tiradaSalvacionRequerida`).
* **Estilo TypeScript / React:**
  * `PascalCase` para Componentes e Interfaces (`PanelResultados.tsx`, `PerfilAtacante`).
  * `camelCase` para funciones, hooks y variables (`usarCalculoCombate.ts`, `obtenerPercentil`).
* **Ubicación de Tests:**
  * Backend: Proyecto dedicado `tests/MathHammer.Pruebas/` replicando la estructura de carpetas.
  * Frontend: Archivos contiguos al componente (`FormularioCombate.tsx` + `FormularioCombate.test.tsx`).
* **Manejo de Errores y Validaciones:**
  * Validación temprana (*fail-fast*) al recibir parámetros.
  * Respuestas HTTP estandarizadas bajo RFC 9457 (Problem Details) con códigos semánticos (400, 422, 500).

## 3. Principios de Desarrollo (Clean Code)

* **Legibilidad sobre astucia:** Prohibido el código críptico, trucos sintácticos innecesarios, anidaciones excesivas de operadores ternarios o "one-liners" difíciles de leer.
* **Funciones de responsabilidad única:** Métodos cortos (menos de 25-30 líneas preferiblemente) que hagan una sola cosa y la hagan de forma predecible.
* **Sin sobreingeniería:** No crear interfaces, fábricas o capas abstractas si solo existe una única implementación real (enfoque Vertical Slice / Minimal API).
* **Gestión de Memoria y Eficiencia:** En el motor Monte Carlo, priorizar tipos por valor y estructuras ligeras (`Span<T>`, arrays directos) para procesar 10.000 iteraciones en milisegundos.

## 4. Flujo de Trabajo y Git

* **Estrategia de Ramas (GitHub Flow):**
  * `main`: Rama protegida. Solo código estable, probado y listo para despliegue.
  * Ramas de trabajo: `feature/motor-montecarlo`, `feature/api-endpoints`, `feature/interfaz-grafica`, `fix/asignacion-heridas`.
* **Commits Semánticos:** Usar siempre el formato estándar:
  * `feat:` nueva funcionalidad.
  * `fix:` corrección de errores.
  * `test:` adición o corrección de pruebas unitarias.
  * `refactor:` mejora de código sin cambiar comportamiento externo.
  * `docs:` cambios en documentación o markdown.
* **Definition of Done (DoD):**
  1. El código compila sin advertencias críticas.
  2. Cumple el contrato de tipos estricto (C# / TypeScript).
  3. Tiene pruebas unitarias asociadas y pasan en verde (`dotnet test` / `npm test`).
  4. Sigue la regla de idioma en español y legibilidad.
* **Gestión Visual:** Seguimiento mediante GitHub Projects (Tablero Kanban: *Backlog*, *En Progreso*, *En Revisión/Testing*, *Completado*).

## 5. Reglas de Interacción para el Asistente / Agente

* **Planificación previa:** Antes de abordar cualquier tarea no trivial, propón un plan paso a paso y espera la confirmación explícita del usuario.
* **Paso a paso:** Trabaja en una sola tarea a la vez. Al finalizar, resume con precisión qué archivos se crearon o modificaron.
* **Certeza técnica:** Si no estás seguro al menos en un 80% del impacto de una decisión o regla de Warhammer 40k, **pregunta antes de asumir o inventar**.
* **Mantenimiento de Contexto:** Lee, utiliza y actualiza activamente `CONTEXT.md` y la documentación asociada en cada cambio importante del proyecto.

## 6. Prohibiciones Explícitas ("No Hagas")

* NO utilices Clean Architecture clásica de 4 capas redundantes; mantén la Minimal API directa y desacoplada.
* NO escribas identificadores o lógica en inglés (mantener el estándar en español).
* NO dejes variables con nombres genéricos (`x`, `res`, `temp`, `data`).
* NO generes código sin tipado estricto (prohibido el uso de `any` en TypeScript o tipos dinámicos no justificados en C#).
* NO implementes spillover en la asignación de daño (el exceso de daño a una miniatura eliminada se pierde, según el reglamento oficial).
* NO realices llamadas de red bloqueantes dentro de bucles de simulación.

## 7. Referencias y Documentación del Proyecto

* **Reglamento Base:** Warhammer 40k (10ª Edición) - Reglas de fase de ataque, modificadores netos tope ±1 y asignación de heridas.
* **Documentación Backend:** Minimal APIs en ASP.NET Core (.NET 8).
* **Documentación Frontend:** React 19, Vite y Tailwind CSS v3/v4.
* **Pruebas y Validación:** Suite de pruebas matemáticas de convergencia Monte Carlo vs. Probabilidad Binomial exacta.

## 8. Memoria Persistente de Sesión

Este proyecto mantiene la memoria operativa en `CONTEXT.md`. Todo agente debe:

1. Leerlo antes de comenzar cualquier tarea.
2. Actualizar **Estado actual** antes de empezar una tarea.
3. Registrar cada paso importante y cambio de archivo con fecha y hora.
4. Registrar las decisiones relevantes y sus motivos.
5. Completar **Traspaso a la siguiente sesión** al terminar.

Las reglas detalladas de mantenimiento están dentro de `CONTEXT.md`.

## 9. Orquestación de Agentes

El proyecto se organiza con un agente padre y dos agentes hijos, definidos en
`.opencode/agents/`:

| Agente | Rol | Modo |
| :--- | :--- | :--- |
| `techlead` | Agente padre: planifica, delega, audita contra el DoD e integra. Único que committea y hace push. | `primary` (agente por defecto) |
| `backend` | Desarrolla el backend (ASP.NET Core / C# / xUnit). | `subagent` |
| `frontend` | Desarrolla el frontend (React / TypeScript / Tailwind). | `subagent` |

Reglas de coordinación:

1. El `techlead` define el **contrato de API** (endpoints, tipos JSON y nombres en
   español) antes de delegar, para evitar interfaces incompatibles.
2. Los agentes `backend` y `frontend` **no committean ni hacen push**; entregan
   código y el `techlead` audita e integra.
3. Todos leen y actualizan `CONTEXT.md` según sus reglas de mantenimiento.
4. El `techlead` valida el trabajo de los hijos contra este `AGENTS.md` y el
   Definition of Done antes de fusionar en `main`.
