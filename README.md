# GestionTareas

Gestión de tareas con usuarios: API en ASP.NET Core y frontend en Angular.

## Estructura

- `src/backend/GestionTareasApi`: API REST (.NET 10, EF Core, SQL Server).
- `src/frontend/gestion-tareas-web`: frontend (Angular 22).

## Requisitos

- .NET SDK 10
- Node.js y npm
- SQL Server o LocalDB
- Herramienta de EF Core: `dotnet tool install --global dotnet-ef`

## Base de datos

La cadena de conexión se lee de `ConnectionStrings:DefaultConnection`. Con LocalDB, desde `src/backend/GestionTareasApi`:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=GestionTareas;Trusted_Connection=True;TrustServerCertificate=True"
dotnet ef database update
```

## Ejecutar

API (https://localhost:7100, documentación en `/scalar`):

```bash
dotnet run --project src/backend/GestionTareasApi --launch-profile https
```

Frontend (http://localhost:4200):

```bash
cd src/frontend/gestion-tareas-web
npm install
npx ng serve
```

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/tasks?status=` | Lista tareas, con filtro opcional por estado |
| GET | `/api/tasks/{id}` | Obtiene una tarea |
| POST | `/api/tasks` | Crea una tarea |
| PUT | `/api/tasks/{id}/status` | Cambia el estado |
| PUT | `/api/tasks/{id}/user` | Reasigna el usuario |
| GET | `/api/users` | Lista usuarios |
| GET | `/api/users/{id}` | Obtiene un usuario |
| POST | `/api/users` | Crea un usuario |

Estados: `1` Pendiente, `2` En progreso, `3` Completada. Solo se permite Pendiente → En progreso → Completada.

La información adicional de cada tarea (prioridad, fecha estimada y etiquetas) se guarda como JSON en una columna validada con `CHECK (ISJSON(...))`.

## Uso de IA

Usé IA como apoyo puntual para revisión de código y para orientarme en la parte de JSON en SQL Server, que no había trabajado antes. La arquitectura y la implementación base son propias. En el frontend la estructura base es propia; para avanzar más rápido usé IA en la implementación de los componentes, revisando y ajustando el resultado.
