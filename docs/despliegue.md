# Guía de despliegue (RUNBOOK)

Manual operativo para levantar MathHammer de forma local con Docker y publicarla
en Render. Complementa al `README.md` con pasos verificables y solución de
problemas.

## Arquitectura

```text
Navegador
   │
   ├── local:   http://localhost:8081   (nginx sirve la web)
   │              └─ /api/*  → proxy inverso → contenedor api :8080
   │
   └── nube:    https://mathhammer-web.onrender.com  (CDN de Render)
                  └─ llama directamente a https://mathhammer-api.onrender.com
```

| Componente | Local | Render |
|---|---|---|
| Backend (ASP.NET Core / Docker) | contenedor `api` en `:8080` | Web Service `mathhammer-api` |
| Frontend (React / estático) | contenedor `web` (nginx) en `:8081` | Static Site `mathhammer-web` |

En local, nginx actúa de **proxy inverso**: las peticiones `/api/*` de la web se
redirigen al backend, evitando problemas de CORS. En Render la web y la API son
dos servicios separados y la web llama directamente a la API (por eso la API
tiene CORS configurado con el origen de la web).

## Despliegue local con Docker

### Requisitos

- **Docker Desktop** instalado en Windows.
- **Integración WSL activada** para la distro usada por este proyecto:
  1. Abre Docker Desktop → **Settings** → **Resources** → **WSL Integration**.
  2. Activa el interruptor de tu distro (p. ej. `AdrianR`) y pulsa **Apply & Restart**.
  3. Comprueba desde el shell de WSL: `docker --version` y `docker compose version`.

### Construir y levantar

```bash
cd /mnt/c/Users/20adr/Desktop/MathHammer
docker compose up --build
```

- La primera vez descarga las imágenes base y compila; tarda unos minutos.
- Para detener: `Ctrl+C`. Para detener y eliminar los contenedores:
  `docker compose down`.

### Verificación

| Comando | Resultado esperado |
|---|---|
| `curl http://localhost:8080/health` | `Healthy` |
| `curl http://localhost:8080/` | `MathHammer API` |
| `curl http://localhost:8080/swagger` | HTML de Swagger (200) |
| Abrir `http://localhost:8081` | La web carga y permite calcular un combate |

La web en `localhost:8081` llama a la API a través de nginx (`/api/...`), por lo
que no requiere configuración adicional de CORS.

### Notas técnicas

- **Backend** (`src/MathHammer.Api/Dockerfile`): multi-stage. Compila con el SDK
  de .NET 9 y la imagen final usa solo el runtime `aspnet:9.0`, exponiendo `8080`.
- **Frontend** (`frontend/MathHammer.Web/Dockerfile`): multi-stage. Compila con
  Node 22 y sirve los estáticos con `nginx:alpine`.
- **`VITE_API_URL=""`**: en el build del frontend se deja vacío para que el
  cliente use URLs relativas (`/api/...`) y nginx resuelva el proxy. No cambiar
  a no ser que se quiera apuntar a otra API.

## Despliegue en Render

### Pasos

1. Entra en **https://render.com** con tu cuenta de GitHub y autoriza el acceso
   al repositorio `20adrianr02-arch/MathHammer` (GitHub → Settings →
   Applications → Render).
2. En el dashboard: **New +** → **Blueprint**.
3. Selecciona el repositorio y la rama `main`; Render lee `render.yaml` y
   propone 2 recursos:
   - `mathhammer-api` → Web Service (Docker, plan free).
   - `mathhammer-web` → Static Site.
4. Pulsa **Apply** / **Create Resources**. Render construye y despliega.
5. Comprueba el estado de ambos en el dashboard (el primer build tarda 2–5 min).

### URLs esperadas

| Servicio | URL |
|---|---|
| API | `https://mathhammer-api.onrender.com` |
| Health | `https://mathhammer-api.onrender.com/health` |
| Swagger | `https://mathhammer-api.onrender.com/swagger` |
| Web | `https://mathhammer-web.onrender.com` |

### Verificación final

- `https://mathhammer-api.onrender.com/health` responde `Healthy`.
- `https://mathhammer-web.onrender.com` carga la app y un cálculo de combate
  devuelve las 8 métricas.

### Actualizaciones

Render se conecta a la rama `main` con auto-deploy: cada `git push` dispara un
nuevo despliegue de los servicios afectados. El plan `free` de Render duerme los
servicios web tras ~15 min sin uso; la primera visita tras estar dormido tarda
30–60 s en responder (es normal).

## Checklist de aceptación

- [ ] `docker compose up --build` levanta la web en `:8081` y la API en `:8080`.
- [ ] `/health` responde `Healthy` en local y en Render.
- [ ] Un cálculo de combate devuelve las 8 métricas en local y en Render.
- [ ] La web desplegada llama a la API sin errores de CORS.
- [ ] El favicon y el título `MathHammer` se muestran en la pestaña.

## Solución de problemas

### Docker no se encuentra en el shell de WSL
El CLI de Docker no está disponible en la distro. Activa la **WSL Integration**
en Docker Desktop (ver requisitos) y reinicia el terminal.

### La web no conecta con la API en local
Verifica que el contenedor `api` está sano (`docker compose ps`) y que
`http://localhost:8080/health` responde. nginx reenvía `/api/*` al servicio
`api:8080` definido en la red de compose.

### Error de CORS en Render
Si Render añadió un sufijo al nombre del servicio web (p. ej.
`mathhammer-web-abc123`), la URL real difiere de la configurada en
`render.yaml`. Actualiza las variables de entorno del servicio `mathhammer-api`:
`Cors__Origenes__0` debe apuntar a la URL real de la web (y `VITE_API_URL` de la
web a la URL real de la API), y redeploya.

### El blueprint no se aplica
Render mostró "A Blueprint file was found, but there was an issue" cuando el
`render.yaml` usaba el esquema antiguo (`staticSites`/`publishDir`). El archivo
actual usa el esquema vigente: `services` con `runtime: static` y
`staticPublishPath`. Si vuelve a ocurrir, verifica que `render.yaml` está
subido en `main` y que no tiene claves obsoletas.