# BACKLOG.md — Historias de usuario

> **Propósito:** registro de las historias de usuario de MathHammer. Cada
> historia define una funcionalidad desde el punto de vista del usuario,
> con criterios de aceptación y estado de seguimiento.

## Formato de historia de usuario

Cada historia sigue el patrón estándar:

> **Como** `[rol]`, **quiero** `[funcionalidad]`, **para** `[beneficio]`.

### Criterios de aceptación

- [ ] Criterio de aceptación 1
- [ ] Criterio de aceptación 2

### Notas técnicas (opcional)

- Referencias a `CONTEXT.md`, `docs/contrato-api.md` u otros documentos.

### Estado

`Pendiente` · `En curso` · `En revisión/testing` · `Completado`

---

## Historias de usuario

### Épica 1: Motor Matemático y Probabilidad Básica

#### HU01: Cálculo de probabilidad en 1D6

**Como** jugador, **quiero** calcular la probabilidad de superar una tirada en 1D6 con un valor objetivo, **para** conocer mis opciones de éxito considerando que 1 siempre falla y 6 siempre acierta.

**Criterios de aceptación:**

- [ ] Dado un valor objetivo entre 2 y 6, el resultado es `(7 - objetivo) / 6`.
- [ ] Dado un objetivo menor o igual que 1, la probabilidad de éxito es `1.0`.
- [ ] Dado un objetivo mayor o igual que 7, la probabilidad de éxito es `0.0`.

**Estado:** `Pendiente`

#### HU02: Distribución binomial de éxitos

**Como** jugador, **quiero** calcular la probabilidad de obtener `K` o más éxitos al lanzar `N` dados, **para** estimar el rendimiento de mis tiradas agrupadas.

**Criterios de aceptación:**

- [ ] Los éxitos esperados se calculan como `N * P(exito)`.
- [ ] La probabilidad acumulada de obtener al menos `K` éxitos aplica la suma binomial `P(X >= K)`.

**Estado:** `Pendiente`

#### HU03: Mecánicas de repetición (Rerolls)

**Como** jugador, **quiero** calcular el impacto de relanzar dados fallidos o unos, **para** evaluar el beneficio de habilidades de repetición.

**Criterios de aceptación:**

- [ ] Con `Reroll All`, la probabilidad efectiva aplica `P + (1 - P) * P`.
- [ ] Con `Reroll 1s`, solo se relanza el resultado 1 y la contribución es `(1 / 6) * P`.

**Estado:** `Pendiente`

### Épica 2: Reglas de Combate y Modificadores (WH40k 10ª Ed.)

#### HU04: Tabla de Fuerza vs Resistencia

**Como** jugador, **quiero** determinar la tirada requerida para herir comparando Fuerza (`S`) y Resistencia (`T`), **para** saber qué resultado necesito en el dado de herida.

**Criterios de aceptación:**

- [ ] Si `S >= 2T`, se hiere a `2+`.
- [ ] Si `S > T` y `S < 2T`, se hiere a `3+`.
- [ ] Si `S = T`, se hiere a `4+`.
- [ ] Si `S < T` y `2S > T`, se hiere a `5+`.
- [ ] Si `2S <= T`, se hiere a `6+`.

**Estado:** `Pendiente`

#### HU05: Modificadores combinados al herir

**Como** jugador, **quiero** aplicar modificadores a la tirada de herida respetando los límites de la edición, **para** resolver interacciones entre habilidades ofensivas y defensivas.

**Criterios de aceptación:**

- [ ] La suma de uno o varios modificadores se limita estrictamente al rango `[-1, +1]`.
- [ ] El valor requerido final queda acotado entre `2+` y `6+`.

**Estado:** `Pendiente`

#### HU06: Impactos críticos (Lethal Hits y Sustained Hits) con repetición de tiradas

**Como** jugador, **quiero** procesar los resultados de impacto considerando repeticiones de tiradas y reglas especiales de críticos, **para** resolver correctamente los impactos adicionales y las heridas automáticas.

**Reglas de flujo y precedencia:**

- [ ] Las repeticiones se resuelven antes de consolidar los resultados finales y aplicar habilidades de impacto crítico.
- [ ] Los dados repetidos descartan su resultado previo y solo el nuevo resultado determina su clasificación.
- [ ] Ningún dado puede repetirse más de una vez.
- [ ] Cada 6 natural final activa las reglas correspondientes.

**Criterios de aceptación:**

- [ ] `Lethal Hits`: cada 6 natural final se clasifica como herida automática y omite la tirada para herir.
- [ ] `Sustained Hits X`: cada 6 natural final cuenta como impacto exitoso y genera `X` impactos adicionales no críticos.
- [ ] Con ambas habilidades, el impacto original se convierte en herida automática y los impactos adicionales pasan a la tirada para herir.
- [ ] Los 6 naturales conservados se mantienen; los 6 obtenidos mediante repetición se suman a los impactos críticos.

**Estado:** `Pendiente`

#### HU07: Resolución de salvaciones y penetración de armadura (AP)

**Como** jugador, **quiero** calcular la tirada de salvación considerando el AP del arma y las salvaciones invulnerables, **para** determinar qué impactos superan la defensa.

**Criterios de aceptación:**

- [ ] Con salvación de armadura (`Sv`), AP y salvación invulnerable (`Inv`), se usa la mejor opción: `min(Sv - AP, Inv)`, limitada al rango `2+` a `7+`.
- [ ] Un ataque con `Devastating Wounds` que obtenga un 6 crítico al herir aplica el daño directamente, ignorando armadura e invulnerable.

**Estado:** `Pendiente`

### Épica 3: Asignación de Daño y Mitigación

#### HU08: Reducción plana de daño

**Como** jugador, **quiero** aplicar habilidades que reducen el daño entrante, **para** mitigar el impacto recibido por cada herida no salvada.

**Criterios de aceptación:**

- [ ] Dado un daño base `D` y una reducción de daño de 1, el resultado es `max(1, D - 1)`.

**Estado:** `Pendiente`

#### HU09: Feel No Pain (FNP)

**Como** jugador, **quiero** resolver tiradas de salvación de daño punto por punto mediante FNP, **para** ignorar puntos de daño individuales antes de que afecten a la miniatura.

**Criterios de aceptación:**

- [ ] Para un daño `D` y un FNP de `N+`, se tira 1D6 independientemente por cada punto de daño.
- [ ] Cada tirada mayor o igual que `N` ignora ese punto de daño específico.

**Estado:** `Pendiente`

#### HU10: Asignación óptima de daño sin derrame (No Spillover)

**Como** jugador, **quiero** asignar el daño a miniaturas de una unidad siguiendo las reglas oficiales, **para** modelar que el daño sobrante de un ataque no pasa a la siguiente miniatura.

**Criterios de aceptación:**

- [ ] El daño entrante se asigna prioritariamente a miniaturas previamente heridas.
- [ ] Si el daño supera las heridas restantes de la miniatura objetivo, el excedente se descarta y la miniatura queda destruida.

**Estado:** `Pendiente`

### Épica 4: Simulación Monte Carlo y Métricas

#### HU11: Simulación de secuencia de combate completa

**Como** jugador, **quiero** ejecutar simulaciones de combate mediante Monte Carlo, **para** obtener una estimación precisa del resultado frente a perfiles complejos.

**Criterios de aceptación:**

- [ ] Cada una de las `N` iteraciones ejecuta la secuencia completa: impacto, herida, salvación, FNP y asignación.

**Estado:** `Pendiente`

#### HU12: Estadísticos y distribución de daño

**Como** jugador, **quiero** consultar la media, mediana, percentiles P25/P75, mínimo y máximo del daño, **para** entender la expectativa media y la dispersión del resultado.

**Criterios de aceptación:**

- [ ] Los estadísticos se calculan a partir del conjunto ordenado de daños.
- [ ] Los percentiles usan interpolación lineal.
- [ ] Se devuelve un histograma de frecuencias.

**Estado:** `Pendiente`

#### HU13: Probabilidades de baja (Kill) y aniquilación (Wipe)

**Como** jugador, **quiero** conocer la probabilidad de destruir al menos una miniatura o la unidad completa, **para** evaluar el riesgo táctico de un enfrentamiento.

**Criterios de aceptación:**

- [ ] `KillProbability` es la proporción de iteraciones con al menos una miniatura destruida.
- [ ] `WipeProbability` es la proporción de iteraciones en las que se destruyen todas las miniaturas.

**Estado:** `Pendiente`
