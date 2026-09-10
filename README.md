# MathHammer

Calculadora de probabilidades y **simulación de combate** para Warhammer 40.000
(10.ª edición). Estima, mediante **Monte Carlo**, el daño medio, los percentiles
y la probabilidad de eliminar una unidad, aplicando las reglas oficiales del
juego.

![Estado del CI](https://github.com/20adrianr02-arch/MathHammer/actions/workflows/ci.yml/badge.svg)

## Características

- Motor de combate completo: impacto → herida → salvación → daño → asignación
  (sin *spillover*).
- Reglas de la 10.ª edición: tabla Fuerza vs Resistencia, modificadores netos
  `±1`, críticos con 6 natural, salvación invulnerable y FNP punto a punto.
- Habilidades ofensivas: `Lethal Hits`, `Sustained Hits X`, `Devastating Wounds`,
  `Lance`, `Twin-linked` y repeticiones.
- Habilidades defensivas: FNP, `-1 al daño`, `-1 al impactar`, `-1 al herir`.
- Métricas: impactos/heridas/salvaciones esperadas, daño medio, miniaturas
  eliminadas, probabilidad de aniquilación y percentiles P25/P75.
- Interfaz web oscura de temática táctica con selector de temas dinámicos.

## Arquitectura

```text
React (Vite + TypeScript)
        │  POST /api/combate/simular  (JSON)
        ▼
ASP.NET Core Minimal API (C# / .NET 9)
        │
        ├── Contratos  → DTOs y validación (RFC 9457)
        ├── Reglas     → probabilidad, herida, salvación
        └── Simulacion → motor Monte Carlo + métricas
```

## Stack

| Capa | Tecnología |
|---|---|
| Backend | ASP.NET Core (.NET 9) con Minimal APIs |
| Pruebas backend | xUnit + FluentAssertions |
| Frontend | React 19 + TypeScript + Vite |
| Pruebas frontend | Vitest + Testing Library |
| CI | GitHub Actions |
| Contenedores | Docker + docker-compose |

## Estructura

```text
src/MathHammer.Api/          Backend (Contratos, Reglas, Simulacion)
tests/MathHammer.Pruebas/    Pruebas unitarias del backend
frontend/MathHammer.Web/     Aplicación web
mobile/MathHammer.App/       Aplicación móvil (Flutter)
docs/contrato-api.md         Contrato de datos de la API (v1.4)
```

## Ejecución en desarrollo

### Backend

```bash
cd /mnt/c/Users/20adr/Desktop/MathHammer
"/mnt/c/Program Files/dotnet/dotnet.exe" run --project src/MathHammer.Api --launch-profile http
```

- API en `http://localhost:5188`.
- Swagger UI en `http://localhost:5188/swagger`.
- Health check en `http://localhost:5188/health`.

### Frontend

```bash
cd frontend/MathHammer.Web
npm install
npm run dev -- --host 0.0.0.0 --port 5173
```

- Web en `http://localhost:5173`.
- Por defecto llama a la API en `http://localhost:5188` (configurable con
  `VITE_API_URL`).

## Pruebas

```bash
# Backend
"/mnt/c/Program Files/dotnet/dotnet.exe" test MathHammer.sln

# Frontend
cd frontend/MathHammer.Web && npm test

# Móvil (Flutter)
cd mobile/MathHammer.App && flutter test
```

## Docker

```bash
docker compose up --build
```

- Web en `http://localhost:8081`.
- API en `http://localhost:8080`.

## Despliegue

El repositorio incluye `render.yaml` como blueprint para [Render](https://render.com).
La API (Docker) y el frontend (sitio estático) están publicados en
`https://mathhammer-web.onrender.com` y `https://mathhammer-api.onrender.com`.

El **manual completo de despliegue** (Docker local, Render, verificación y
solución de problemas) está en [`docs/despliegue.md`](docs/despliegue.md).

## Contrato de la API

`POST /api/combate/simular` acepta `PeticionCombate` y devuelve `ResultadoCombate`
con las 8 métricas y el resumen. Detalle completo en
[`docs/contrato-api.md`](docs/contrato-api.md).

## Estado

- Motor matemático, simulación y habilidades: implementados y probados.
- Frontend web conectado a la API: implementado y desplegado en Render.
- Aplicación móvil (Flutter): implementada (formulario + cliente API + 8 métricas, con tests). El build del APK requiere Android Studio/SDK en el equipo.

## App móvil (Flutter)

```bash
cd mobile/MathHammer.App
flutter pub get
flutter test
```

La app apunta por defecto a la API desplegada en Render
(`https://mathhammer-api.onrender.com`), por lo que funciona en un móvil real
sin configuración. Para desarrollo local contra la API del equipo (emulador de
Android), sobrescribe la URL con `--dart-define`:

```bash
flutter build apk --dart-define=API_URL=http://10.0.2.2:5188
```

El APK resultante se genera en
`mobile/MathHammer.App/build/app/outputs/flutter-apk/app-release.apk`.
