# GestionTareas

[![CI](https://github.com/pertuznavas/GestionTareas/actions/workflows/ci.yml/badge.svg)](https://github.com/pertuznavas/GestionTareas/actions/workflows/ci.yml)

Task management with users: ASP.NET Core API and Angular frontend.

## Structure

- `src/backend/GestionTareasApi`: REST API (.NET 10, EF Core, SQL Server).
- `src/backend/GestionTareasApi.Tests`: unit tests (xUnit).
- `src/frontend/gestion-tareas-web`: frontend (Angular 22).
- `database/queries.sql`: sample queries on the JSON column.

## Requirements

- .NET 10 SDK
- Node.js and npm
- SQL Server or LocalDB
- EF Core tools: `dotnet tool install --global dotnet-ef`

## Database

The connection string is read from `ConnectionStrings:DefaultConnection`. With LocalDB, from `src/backend/GestionTareasApi`:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=GestionTareas;Trusted_Connection=True;TrustServerCertificate=True"
dotnet ef database update
```

## Run

API (https://localhost:7100, docs at `/scalar`):

```bash
dotnet run --project src/backend/GestionTareasApi --launch-profile https
```

Frontend (http://localhost:4200):

```bash
cd src/frontend/gestion-tareas-web
npm install
npx ng serve
```

Tests, from `src/backend`:

```bash
dotnet test
```

## Endpoints

| Method | Route | Description |
|---|---|---|
| GET | `/api/tasks?status=` | List tasks, optionally filtered by status |
| GET | `/api/tasks/{id}` | Get a task |
| POST | `/api/tasks` | Create a task |
| PUT | `/api/tasks/{id}/status` | Change the status |
| PUT | `/api/tasks/{id}/user` | Reassign the user |
| GET | `/api/users` | List users |
| GET | `/api/users/{id}` | Get a user |
| POST | `/api/users` | Create a user |

Statuses: `1` Pending, `2` In progress, `3` Done. Only Pending → In progress → Done is allowed.

Each task's additional info (priority, estimated end date and tags) is stored as JSON in a column validated with `CHECK (ISJSON(...))`.

## Security

- Input validation with data annotations on the DTOs.
- Global exception handler: unexpected errors return a generic message, without internal details.
- CORS limited to the origins in `Cors:AllowedOrigins` (`appsettings.json`).
- HTTPS redirection, and HSTS outside Development.
- Connection string kept in user secrets, out of the repository.
- EF Core parameterized queries.
- OpenAPI and Scalar exposed only in Development.

## Future improvements

- Authentication and authorization (JWT), so each user only sees and manages their own tasks.

## AI usage

I used AI as occasional support for code review and to get oriented with JSON in SQL Server, which I had not worked with before. The architecture and base implementation are my own. In the frontend the base structure is my own; to move faster I used AI to implement the components, reviewing and adjusting the result.
