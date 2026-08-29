# Contrato de datos de la API

Versión: `1.3`

Este documento define el contrato JSON compartido por el backend y el
frontend de MathHammer. Los nombres del contrato están en español y usan
`camelCase` para las propiedades JSON.

## Endpoint

```http
POST /api/combate/simular
Content-Type: application/json
```

El endpoint ejecuta una simulación Monte Carlo de un único perfil de arma
contra una unidad defensora.

### Respuestas HTTP

| Código | Significado |
|---|---|
| `200` | Simulación completada. Devuelve `ResultadoCombate`. |
| `400` | JSON ausente, mal formado o con tipos incompatibles. |
| `422` | JSON válido, pero con valores fuera de las reglas de validación. |
| `500` | Error inesperado del servidor. |

Los errores `400`, `422` y `500` usan `application/problem+json` conforme a
RFC 9457.

## DTO `PeticionCombate`

### Ejemplo JSON

```json
{
  "atacante": {
    "nombreUnidad": "Escuadra intercesora",
    "impactaA": 3,
    "repiteParaImpactar": false,
    "repiteUnoParaHerir": false
  },
  "arma": {
    "cantidadAtaques": 8,
    "ataquesAleatorios": null,
    "fuerza": 5,
    "penetracionArmadura": -2,
    "danio": 1,
    "danioAleatorio": null,
    "repetirTiradaHerida": false,
    "habilidades": {
      "lanza": true,
      "golpesSostenidos": 0,
      "golpesLetales": false,
      "heridasDevastadoras": false
    }
  },
  "defensor": {
    "nombreUnidad": "Guerreros Necrones",
    "resistencia": 4,
    "salvacion": 3,
    "salvacionInvulnerable": null,
    "sensacionDolor": null,
    "reduccionDanio": false,
    "penalizacionImpactar": false,
    "penalizacionHerir": false,
    "heridasPorMiniatura": 2,
    "cantidadMiniaturas": 5
  },
  "configuracionSimulacion": {
    "iteraciones": 10000,
    "semillaAleatoria": null
  }
}
```

### `atacante`

| Campo | Tipo | Obligatorio | Descripción |
|---|---:|---:|---|
| `nombreUnidad` | cadena | Sí | Nombre mostrado del atacante. |
| `impactaA` | entero | Sí | Habilidad de impacto requerida, entre `2` y `6`. |
| `repiteParaImpactar` | booleano | Sí | Repite todas las tiradas de impacto fallidas (`Full re-roll to hit`). |
| `repiteUnoParaHerir` | booleano | Sí | Repite únicamente los resultados naturales de 1 en la tirada de herida. |

### `arma`

| Campo | Tipo | Obligatorio | Descripción |
|---|---|---:|---|
| `cantidadAtaques` | entero | Sí | Ataques fijos. Se ignora si se usa `ataquesAleatorios`. |
| `ataquesAleatorios` | `Dados` o `null` | Sí | Fuente alternativa para obtener la cantidad de ataques. |
| `fuerza` | entero | Sí | Fuerza del arma. |
| `penetracionArmadura` | entero | Sí | AP del arma, normalmente `0` o negativo. |
| `danio` | entero | Sí | Daño fijo. Se ignora si se usa `danioAleatorio`. |
| `danioAleatorio` | `Dados` o `null` | Sí | Fuente alternativa para obtener el daño de cada impacto. |
| `repetirTiradaHerida` | booleano | Sí | Regla `Twin-linked`: repite tiradas de herida fallidas. Es la repetición completa de herida. |
| `habilidades` | `HabilidadesArma` | Sí | Habilidades universales incluidas en la v1. |

`HabilidadesArma` contiene:

| Campo | Tipo | Descripción |
|---|---|---|
| `lanza` | booleano | `Lance`: concede `+1` a la tirada para herir cuando la regla sea aplicable. |
| `golpesSostenidos` | entero | `Sustained Hits X`. `0` significa que no existe. |
| `golpesLetales` | booleano | `Lethal Hits`. |
| `heridasDevastadoras` | booleano | `Devastating Wounds`. |

### `defensor`

| Campo | Tipo | Obligatorio | Descripción |
|---|---:|---:|---|
| `nombreUnidad` | cadena | Sí | Nombre mostrado del defensor. |
| `resistencia` | entero | Sí | Resistencia de la unidad, entre `1` y `20`. |
| `salvacion` | entero | Sí | Salvación de armadura, entre `2` y `6`. |
| `salvacionInvulnerable` | entero o `null` | Sí | Salvación invulnerable entre `2` y `6`, si existe. |
| `sensacionDolor` | entero o `null` | Sí | FNP entre `3` y `6`, si existe. |
| `reduccionDanio` | booleano | Sí | Reduce en `1` el daño de cada ataque no salvado, con un mínimo de `1`. |
| `penalizacionImpactar` | booleano | Sí | Aplica `-1` a la tirada de impacto del atacante. |
| `penalizacionHerir` | booleano | Sí | Aplica `-1` a la tirada de herida del atacante. |
| `heridasPorMiniatura` | entero | Sí | Heridas iniciales de cada miniatura, mayor que `0`. |
| `cantidadMiniaturas` | entero | Sí | Número de miniaturas, mayor que `0`. |

### `configuracionSimulacion`

| Campo | Tipo | Obligatorio | Descripción |
|---|---|---:|---|
| `iteraciones` | entero | Sí | Número de simulaciones, entre `1` y `100000`. |
| `semillaAleatoria` | entero o `null` | Sí | Semilla opcional para reproducir una simulación. |

### `Dados`

Representa una expresión de dados como `D3`, `D6`, `D6+1` o `2D6`.

```json
{
  "dados": 1,
  "caras": 6,
  "modificador": 1
}
```

Sus campos son enteros positivos, salvo `modificador`, que puede ser
negativo. `dados` debe ser mayor que `0` y `caras` debe estar entre `2` y `6`.

### Reglas de nulabilidad y exclusión

- `ataquesAleatorios` y `danioAleatorio` deben ser `null` cuando se usa el
  valor fijo correspondiente.
- Si se proporciona un dado aleatorio, sustituye al valor fijo del mismo
  campo; el backend debe rechazar una petición que no tenga una fuente válida.
- `salvacionInvulnerable` y `sensacionDolor` usan `null` para indicar que la
  regla no existe.
- `repiteParaImpactar` indica repetición completa de las tiradas de impacto
  fallidas; `repiteUnoParaHerir` repite únicamente los resultados naturales de 1
  en la tirada de herida; `repetirTiradaHerida` (Twin-linked) repite la tirada
  de herida completa.
- Si coinciden varias reglas de repetición sobre el mismo dado, el dado solo se
  repite una vez. El resultado final de esa repetición es el que se procesa.
- `lanza` suma `+1` a la tirada para herir; `penalizacionImpactar` y
  `penalizacionHerir` restan `1`. El modificador neto resultante se acota a
  `[-1, +1]` conforme a la edición.
- La petición representa una sola arma. No se admite un array de armas en la
  versión `1.1`.

## DTO `ResultadoCombate`

### Ejemplo JSON

```json
{
  "metricas": {
    "impactosEsperados": 5.333,
    "heridasEsperadas": 3.556,
    "salvacionesEnemigo": 1.185,
    "probabilidadMatarUnidad": 0.31,
    "miniaturasEliminadas": 0.935,
    "danioMedioEsperado": 2.377,
    "percentil25": 1.0,
    "percentil75": 3.0
  },
  "resumen": {
    "iteracionesEjecutadas": 10000,
    "duracionMilisegundos": 14
  }
}
```

### `metricas`

| Campo | Tipo | Descripción |
|---|---|---|
| `impactosEsperados` | decimal | Media esperada de impactos exitosos (cálculo analítico). |
| `heridasEsperadas` | decimal | Media esperada de heridas (cálculo analítico). |
| `salvacionesEnemigo` | decimal | Media esperada de salvaciones exitosas del defensor. |
| `probabilidadMatarUnidad` | decimal | Proporción de iteraciones con toda la unidad destruida. |
| `miniaturasEliminadas` | decimal | Media de miniaturas destruidas por iteración. |
| `danioMedioEsperado` | decimal | Media de heridas aplicadas a la unidad por iteración. |
| `percentil25` | decimal | Percentil 25 del daño con interpolación lineal. |
| `percentil75` | decimal | Percentil 75 del daño con interpolación lineal. |

Todas las probabilidades se expresan como valores entre `0.0` y `1.0`, no como
porcentajes enteros.

### `resumen`

| Campo | Tipo | Descripción |
|---|---|---|
| `iteracionesEjecutadas` | entero | Número de iteraciones ejecutadas. |
| `duracionMilisegundos` | entero | Duración de la simulación en milisegundos. |

El daño de cada iteración se limita a las heridas que realmente puede perder
la unidad. El exceso de daño de un ataque que destruye una miniatura no se
transfiere a la siguiente miniatura.

### Reglas de combate que debe respetar el servidor

- La tabla de herida usa `2+`, `3+`, `4+`, `5+` o `6+` según la comparación
  entre Fuerza y Resistencia.
- Los modificadores netos de impacto y herida se limitan a `-1` y `+1`.
- Un 1 natural siempre falla en impacto, herida y salvación.
- Un 6 natural es impacto o herida crítica cuando corresponde.
- Las repeticiones se resuelven antes de consolidar los impactos críticos. Las
  repeticiones del atacante (`repiteParaImpactar`, `repiteParaHerir`,
  `repiteUnoParaHerir`) y `Twin-linked` se consideran reglas independientes,
  pero ningún dado puede repetirse más de una vez.
- `Lethal Hits` convierte los 6 naturales finales en heridas automáticas.
- `Sustained Hits X` genera `X` impactos adicionales no críticos por cada 6
  natural final.
- `Lance` suma `+1` a la tirada para herir; `penalizacionHerir` resta `1`. El
  neto se aplica antes de calcular la tirada requerida.
- `Devastating Wounds` evita la salvación cuando se obtiene una herida crítica.
- `reduccionDanio` reduce en `1` el daño de cada ataque no salvado, con un
  mínimo de `1` punto de daño.
- La salvación invulnerable no se modifica por AP.
- FNP se resuelve individualmente por cada punto de daño.
- El daño se asigna primero a miniaturas previamente heridas y no existe
  spillover.

## Decisiones de alcance v1

- Una petición contiene un único perfil de arma.
- Se admiten ataques y daño fijos o expresados mediante dados.
- Las habilidades de arma soportadas son `Lethal Hits`, `Sustained Hits X`,
  `Devastating Wounds`, `Twin-linked` y `Lance`.
- Las repeticiones del atacante son `repiteParaImpactar` y `repiteUnoParaHerir`;
  la repetición completa de herida es `repetirTiradaHerida` (Twin-linked).
- Las habilidades defensivas soportadas son `reduccionDanio`,
  `penalizacionImpactar` y `penalizacionHerir`.
- No existe tipo de ataque ni cobertura: el simulador no diferencia entre
  disparo y combate en esta versión.
- La respuesta incluye únicamente las 8 métricas del panel de resultados; no se
  devuelven histogramas.
- No se incluyen todavía `Anti-X`, `Precision`, `Melta`, `Heavy`, `Torrent` ni
  reglas de unidades con varios perfiles de arma.
