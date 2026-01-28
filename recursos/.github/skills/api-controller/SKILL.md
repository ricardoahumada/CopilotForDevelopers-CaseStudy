---
name: api-controller
description: Generates REST API controllers following Clean Architecture and PortalEmpleo conventions. Use when implementing REST API endpoints, creating controllers, or defining API routes for the job portal application.
license: MIT
metadata:
  author: course-team@netmind.es
  version: "1.0.0"
compatibility: Requires .NET 8 SDK and ASP.NET Core Web API
allowed-tools: Read Write Bash(git:*) Bash(dotnet:*)
---

# API Controller Skill - PortalEmpleo

## When to use this skill

Use this skill when implementing REST API controllers for PortalEmpleo project. This skill is automatically triggered when the user mentions:
- Creating controllers
- API endpoints
- REST API
- Web API routes

## Project Context

- **Stack:** .NET 8, ASP.NET Core Web API
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, Api)
- **Autenticación:** JWT HS256 (60min access, 7days refresh)
- **Validation:** FluentValidation

## Controller Standards

### Estructura del Controlador

```csharp
namespace PortalEmpleo.Api.Controllers;

/// <summary>
/// Controlador para gestión de autenticación
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Registrar nuevo usuario
    /// </summary>
    /// <param name="registerDto">Datos de registro</param>
    /// <returns>Usuario creado con tokens</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation Error",
                Detail = ex.Message
            });
        }
    }
}
```

## Required Elements

### 1. Atributos de Clase
- `[ApiController]`
- `[Route("api/v1/[controller]")]`
- `[Produces("application/json")]`

### 2. Documentación XML
- `<summary>` para la clase
- `<summary>` para cada método público
- `<param>` para parámetros
- `<returns>` para valor de retorno
- `[ProducesResponseType]` para todos los status codes posibles

### 3. Dependency Injection
- Inyectar interfaces de servicios
- Logger con ILogger<T>

### 4. Manejo de Errores
- Try-catch con logging
- Devolver ProblemDetails para errores

### 5. convenciones de Nombres
- Controller suffix en nombre de clase
- PascalCase para métodos
- HTTP verbs: [HttpGet], [HttpPost], [HttpPut], [HttpPatch], [HttpDelete]

## Example Endpoints PortalEmpleo

### AuthController

```csharp
[HttpPost("register")]      // POST /auth/register
[HttpPost("login")]         // POST /auth/login
[HttpPost("refresh")]       // POST /auth/refresh
```

### UsersController

```csharp
[HttpGet("me")]             // GET /users/me
[HttpPut("me")]             // PUT /users/me
[HttpDelete("me")]          // DELETE /users/me (soft delete)
```

### JobOffersController

```csharp
[HttpGet]                   // GET /job-offers (público con filtros)
[HttpPost]                  // POST /job-offers (COMPANY)
[HttpGet("{id}")]           // GET /job-offers/{id}
[HttpPut("{id}")]           // PUT /job-offers/{id} (COMPANY own)
[HttpPatch("{id}/publish")] // PATCH /job-offers/{id}/publish (COMPANY)
[HttpPatch("{id}/pause")]   // PATCH /job-offers/{id}/pause (COMPANY)
[HttpPatch("{id}/close")]   // PATCH /job-offers/{id}/close (COMPANY)
```

### ApplicationsController

```csharp
[HttpPost]                              // POST /applications (CANDIDATE)
[HttpGet("me")]                         // GET /applications/me (CANDIDATE)
[HttpGet("../job-offers/{id}/applications")] // GET /job-offers/{id}/applications (COMPANY)
[HttpPatch("{id}/status")]              // PATCH /{id}/status (COMPANY)
```

## Output Format

Generate complete .cs file with:
- All using statements
- Full class implementation
- XML documentation
- Error handling
- Route attributes

## Files to Write

- `src/PortalEmpleo.Api/Controllers/[Feature]Controller.cs`
