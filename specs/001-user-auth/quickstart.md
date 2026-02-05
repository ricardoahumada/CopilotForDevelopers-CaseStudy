# Quickstart: Portal Empleo API - User Authentication Module

**Feature**: 001-user-auth | **Date**: 2026-02-04  
**Purpose**: Guía de configuración rápida para desarrollo local del módulo de gestión de usuarios y autenticación.

---

## Prerequisites

### Required Software

| Tool | Version | Purpose | Installation |
|------|---------|---------|--------------|
| **.NET SDK** | 8.0.x | Runtime y compilador C# | [Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) |
| **Git** | 2.40+ | Control de versiones | [Download Git](https://git-scm.com/downloads) |
| **Visual Studio Code** (recommended) | 1.85+ | Editor de código | [Download VS Code](https://code.visualstudio.com/) |
| **C# Dev Kit Extension** | Latest | IntelliSense, debugging | Install from VS Code marketplace |

### Optional Tools

| Tool | Purpose | Installation |
|------|---------|--------------|
| **Postman** / **Thunder Client** | API testing | [Postman](https://www.postman.com/) / [Thunder Client extension](https://marketplace.visualstudio.com/items?itemName=rangav.vscode-thunder-client) |
| **Docker Desktop** | Production database simulation | [Docker](https://www.docker.com/products/docker-desktop/) |
| **dotnet-ef CLI** | EF Core migrations (for production DB) | `dotnet tool install --global dotnet-ef` |

### Verification

```bash
# Verify .NET SDK installation
dotnet --version
# Expected output: 8.0.x

# Verify Git installation
git --version
# Expected output: git version 2.40+

# Verify dotnet-ef (optional, for migrations)
dotnet ef --version
# Expected output: Entity Framework Core .NET Command-line Tools 8.0.x
```

---

## Project Setup

### 1. Clone Repository

```bash
# Clone from GitHub (replace with actual repository URL)
git clone https://github.com/your-org/portal-empleo-api.git
cd portal-empleo-api

# Checkout feature branch
git checkout 001-user-auth
```

### 2. Restore NuGet Packages

```bash
# Restore dependencies for all projects in solution
dotnet restore

# Expected output:
#   Restored PortalEmpleo.Domain (in 1.2s)
#   Restored PortalEmpleo.Application (in 1.5s)
#   Restored PortalEmpleo.Infrastructure (in 2.0s)
#   Restored PortalEmpleo.Api (in 2.3s)
```

### 3. Configure JWT Settings

Create `src/PortalEmpleo.Api/appsettings.Development.json` with JWT configuration:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyAtLeast32CharactersLongForDevelopment123!",
    "Issuer": "PortalEmpleoDev",
    "Audience": "PortalEmpleoUsers",
    "AccessTokenExpiryMinutes": 60,
    "RefreshTokenExpiryDays": 7
  },
  "AllowedOrigins": [
    "http://localhost:3000",
    "http://localhost:5173"
  ]
}
```

**⚠️ Security Notes**:
- Never commit `appsettings.Development.json` to Git (already in `.gitignore`)
- Production secrets must use Azure Key Vault or environment variables
- `SecretKey` must be **at least 32 characters** for HS256 algorithm

### 4. Environment Variables (Alternative to appsettings)

If you prefer environment variables over config files:

```bash
# PowerShell
$env:JWT__SECRET_KEY="YourSuperSecretKeyAtLeast32CharactersLongForDevelopment123!"
$env:JWT__ISSUER="PortalEmpleoDev"
$env:JWT__AUDIENCE="PortalEmpleoUsers"
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:ASPNETCORE_URLS="http://localhost:5000"

# Linux/Mac (bash)
export JWT__SECRET_KEY="YourSuperSecretKeyAtLeast32CharactersLongForDevelopment123!"
export JWT__ISSUER="PortalEmpleoDev"
export JWT__AUDIENCE="PortalEmpleoUsers"
export ASPNETCORE_ENVIRONMENT="Development"
export ASPNETCORE_URLS="http://localhost:5000"
```

---

## Build & Run

### Development Mode (Hot Reload)

```bash
# Run with hot reload (restarts on file changes)
dotnet watch --project src/PortalEmpleo.Api

# Expected output:
# Building...
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:5000
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shut down.
```

### Production Mode

```bash
# Build in Release configuration
dotnet build --configuration Release

# Run compiled binary
dotnet run --project src/PortalEmpleo.Api --configuration Release --no-build

# Alternative: Run directly from bin/
cd src/PortalEmpleo.Api/bin/Release/net8.0
dotnet PortalEmpleo.Api.dll
```

### Verify API is Running

Open browser or use curl:

```bash
# Health check endpoint
curl http://localhost:5000/health

# Expected output:
# {"status":"Healthy","database":"InMemory"}

# Swagger UI (if Development environment)
# Open in browser: http://localhost:5000/swagger
```

---

## API Testing

### Using Swagger UI (Development Only)

1. Navigate to **http://localhost:5000/swagger**
2. Expand **POST /api/v1/auth/register**
3. Click **Try it out**
4. Fill request body:

```json
{
  "email": "test.user@example.com",
  "password": "Secure123",
  "firstName": "Test",
  "lastName": "User",
  "phoneNumber": "+1234567890",
  "dateOfBirth": "1995-01-01"
}
```

5. Click **Execute** → Should return `201 Created` with access/refresh tokens

### Using curl (Cross-Platform)

#### 1. Register New User

```bash
curl -X POST http://localhost:5000/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test.user@example.com",
    "password": "Secure123",
    "firstName": "Test",
    "lastName": "User",
    "phoneNumber": "+1234567890",
    "dateOfBirth": "1995-01-01"
  }'

# Expected response (201 Created):
# {
#   "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
#   "refreshToken": "a1b2c3d4e5f6g7h8i9j0...",
#   "expiresAt": "2026-02-04T15:30:00Z"
# }
```

#### 2. Login (Obtain Tokens)

```bash
curl -X POST http://localhost:5000/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test.user@example.com",
    "password": "Secure123"
  }'

# Store access token for next requests
ACCESS_TOKEN="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

#### 3. Get Own Profile (Authenticated)

```bash
curl -X GET http://localhost:5000/api/v1/users/me \
  -H "Authorization: Bearer $ACCESS_TOKEN"

# Expected response (200 OK):
# {
#   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
#   "email": "test.user@example.com",
#   "firstName": "Test",
#   "lastName": "User",
#   "phoneNumber": "+1234567890",
#   "dateOfBirth": "1995-01-01",
#   "role": "CANDIDATE",
#   "createdAt": "2026-02-04T14:00:00Z",
#   "updatedAt": "2026-02-04T14:00:00Z"
# }
```

#### 4. Update Profile

```bash
curl -X PUT http://localhost:5000/api/v1/users/me \
  -H "Authorization: Bearer $ACCESS_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Updated",
    "lastName": "User",
    "phoneNumber": "+1987654321",
    "dateOfBirth": "1995-01-01"
  }'

# Expected response (200 OK): Updated UserDto
```

#### 5. Refresh Access Token

```bash
curl -X POST http://localhost:5000/api/v1/auth/refresh \
  -H "Content-Type: application/json" \
  -d '{
    "refreshToken": "a1b2c3d4e5f6g7h8i9j0..."
  }'

# Expected response (200 OK): New access + refresh tokens
```

#### 6. Delete Account (Soft Delete)

```bash
curl -X DELETE http://localhost:5000/api/v1/users/me \
  -H "Authorization: Bearer $ACCESS_TOKEN"

# Expected response (204 No Content)
```

### Postman Collection

Import `docs/postman/PortalEmpleo-Auth.postman_collection.json` (to be created) with pre-configured requests.

---

## Testing

### Run All Tests

```bash
# Run all tests in solution
dotnet test

# Expected output:
# Passed!  - Failed:     0, Passed:    42, Skipped:     0, Total:    42
```

### Run Tests with Coverage

```bash
# Install coverlet if not already installed
dotnet tool install --global coverlet.console

# Run tests with coverage collection
dotnet test --collect:"XPlat Code Coverage"

# Generate HTML report (requires reportgenerator)
dotnet tool install --global dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html

# Open coverage-report/index.html in browser
```

### Run Specific Test Category

```bash
# Run only unit tests (Domain + Application layers)
dotnet test --filter "Category=Unit"

# Run only integration tests (Infrastructure + Api layers)
dotnet test --filter "Category=Integration"
```

---

## Database

### Development (In-Memory)

In-Memory database is configured by default in `Development` environment:

```csharp
// src/PortalEmpleo.Api/Program.cs
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("PortalEmpleoDev"));
}
```

**Characteristics**:
- Database is **recreated on each startup** (data is lost)
- No migrations needed
- Ideal for rapid development and testing
- Test users are seeded automatically (see `ApplicationDbContextSeed.cs`)

### Production (PostgreSQL with Docker)

For production-like environment, use PostgreSQL with Docker:

```bash
# Start PostgreSQL container
docker run --name portalempleo-postgres \
  -e POSTGRES_USER=portalempleo \
  -e POSTGRES_PASSWORD=Dev123! \
  -e POSTGRES_DB=portalempleo_dev \
  -p 5432:5432 \
  -d postgres:16

# Update connection string in appsettings.Development.json
# "ConnectionStrings": {
#   "DefaultConnection": "Host=localhost;Port=5432;Database=portalempleo_dev;Username=portalempleo;Password=Dev123!"
# }

# Change database provider in Program.cs (or use environment variable)
# options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

# Create initial migration
dotnet ef migrations add InitialCreate --project src/PortalEmpleo.Infrastructure --startup-project src/PortalEmpleo.Api

# Apply migration to database
dotnet ef database update --project src/PortalEmpleo.Infrastructure --startup-project src/PortalEmpleo.Api

# Run application
dotnet watch --project src/PortalEmpleo.Api
```

---

## Troubleshooting

### Error: "Failed to bind to address http://localhost:5000: address already in use"

**Solution**: Another process is using port 5000. Change port:

```bash
$env:ASPNETCORE_URLS="http://localhost:5001"
dotnet watch --project src/PortalEmpleo.Api
```

### Error: "JWT Secret Key must be at least 32 characters"

**Solution**: Check `appsettings.Development.json` or environment variable `JWT__SECRET_KEY` has ≥32 characters.

```bash
# PowerShell
$env:JWT__SECRET_KEY="YourSuperSecretKeyAtLeast32CharactersLongForDevelopment123!"
```

### Error: "Unable to resolve service for type 'IUnitOfWork'"

**Solution**: Ensure `ServiceCollectionExtensions.cs` is registering all dependencies:

```csharp
// src/PortalEmpleo.Api/Extensions/ServiceCollectionExtensions.cs
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
```

### Swagger UI Not Showing

**Solution**: Swagger is only enabled in Development environment. Check:

```bash
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet watch --project src/PortalEmpleo.Api
```

### Tests Failing: "Database connection error"

**Solution**: Tests use In-Memory database. Ensure each test creates isolated DbContext:

```csharp
var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
    .Options;
```

---

## IDE Configuration

### Visual Studio Code

Recommended extensions:

1. **C# Dev Kit** (ms-dotnettools.csdevkit)
2. **C#** (ms-dotnettools.csharp)
3. **GitLens** (eamodio.gitlens)
4. **REST Client** / **Thunder Client** for API testing
5. **EditorConfig for VS Code** (respects `.editorconfig` formatting)

Launch configuration (`.vscode/launch.json`):

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/src/PortalEmpleo.Api/bin/Debug/net8.0/PortalEmpleo.Api.dll",
      "args": [],
      "cwd": "${workspaceFolder}/src/PortalEmpleo.Api",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  ]
}
```

### Visual Studio 2022

1. Open `PortalEmpleo.sln`
2. Set `PortalEmpleo.Api` as startup project
3. Press **F5** to debug or **Ctrl+F5** to run without debugging
4. Swagger UI opens automatically at `https://localhost:7000/swagger`

### JetBrains Rider

1. Open `PortalEmpleo.sln`
2. Run configuration is auto-detected
3. Set environment variables in **Run/Debug Configurations** → **Environment variables**
4. Run with **Shift+F10**

---

## Next Steps

1. **Review Specification**: Read [spec.md](spec.md) to understand functional requirements
2. **Explore Architecture**: Check [plan.md](plan.md) for Clean Architecture structure
3. **Study Contracts**: Review [contracts/](contracts/) for OpenAPI endpoint schemas
4. **Implement Tests**: Follow TDD approach (write tests first in `/tests`)
5. **Implement Features**: Start with Domain entities, then Application services, Infrastructure repositories, Api controllers
6. **Run Tests**: Ensure ≥80% code coverage: `dotnet test --collect:"XPlat Code Coverage"`
7. **Commit Changes**: Follow [Conventional Commits](https://www.conventionalcommits.org/): `feat(auth): implement user registration`

---

## Resources

| Resource | URL | Purpose |
|----------|-----|---------|
| **.NET 8 Documentation** | https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8 | Framework features |
| **ASP.NET Core 8** | https://learn.microsoft.com/en-us/aspnet/core/ | Web API development |
| **EF Core 8** | https://learn.microsoft.com/en-us/ef/core/ | ORM documentation |
| **FluentValidation** | https://docs.fluentvalidation.net/ | Validation library |
| **JWT.IO** | https://jwt.io/ | Decode/verify JWT tokens |
| **Swagger Editor** | https://editor.swagger.io/ | Validate OpenAPI schemas |
| **xUnit** | https://xunit.net/ | Testing framework |
| **Moq** | https://github.com/moq/moq4 | Mocking library |

---

**Support**: For issues, contact the development team or create a GitHub issue in the repository.

*Generated by /speckit.plan command - Phase 1 Design*
