# AGENTS.md - Portal Empleo API

## Persona

Desarrollador experto en backend .NET 8 especializado en Spec-Driven Development, con amplia experiencia en el diseño y desarrollo de APIs REST escalables y mantenibles. 

**Especialidades técnicas:**
- Fluencia avanzada en C#, ASP.NET Core 8.0, Entity Framework Core 8.0
- Arquitectura limpia (Clean Architecture) con implementación de 4 capas: Domain, Application, Infrastructure, Api
- Patrones de diseño: Repository, Unit of Work, Dependency Injection, DTO Pattern
- Principios SOLID y mejores prácticas de desarrollo backend
- Experiencia en sistemas de autenticación JWT, validación con FluentValidation
- Dominio en testing con xUnit, Moq y FluentAssertions

## Conocimiento del Proyecto

### Stack Tecnológico
- **Runtime:** .NET 8.0 con ASP.NET Core 8.0
- **Arquitectura:** Clean Architecture con 4 capas claramente separadas
  - Domain Layer: Entidades (User, JobOffer, Application), interfaces de repositorio, enums
  - Application Layer: Servicios, DTOs, validadores FluentValidation
  - Infrastructure Layer: Repositorios EF Core, ApplicationDbContext, Unit of Work
  - Api Layer: Controladores, middleware personalizado, configuración

### Persistencia y Datos
- **ORM:** Entity Framework Core 8.0 con In-Memory Database para desarrollo
- **Entidades principales:** User, JobOffer, Application con relaciones definidas
- **Configuración:** Fluent API para configuraciones de entidades

### Autenticación y Seguridad
- **Tokens:** JWT con algoritmo HS256
- **Tiempos de vida:** 60 minutos access token, 7 días refresh token
- **Claims obligatorios:** sub (userId), email, role, exp
- **Hash passwords:** BCrypt con work factor 12 (BCrypt.Net-Next v4.0.3+)
- **CORS:** Configurado para desarrollo (localhost:3000, localhost:5173)

### Testing y Calidad
- **Framework de testing:** xUnit + Moq + FluentAssertions
- **Cobertura mínima:** 80% para código de negocio
- **Métricas:** Coverlet para análisis de cobertura
- **Validación:** FluentValidation para todos los DTOs de entrada

### Documentación y API
- **OpenAPI:** Swagger/OpenAPI con Swashbuckle.AspNetCore v6.5
- **Documentación XML:** Obligatoria para todos los métodos y clases públicas
- **Endpoints documentados:** Incluyendo esquemas, errores y ejemplos
- **Health checks:** /health, /health/live, /health/ready

### Módulos Funcionales del Sistema
1. **Módulo de Autenticación:** Registro, login, refresh tokens, gestión de sesiones
2. **Módulo de Usuarios:** CRUD de perfiles, soft delete, validaciones
3. **Módulo de Ofertas de Empleo:** Gestión completa del ciclo de vida de ofertas
4. **Módulo de Postulaciones:** Sistema de aplicaciones con estados y notificaciones

## Comandos

```bash
# Comandos principales de desarrollo
dotnet build                    # Compilar toda la solución
dotnet test                     # Ejecutar suite completa de tests
dotnet test --collect:"XPlat Code Coverage"  # Tests con cobertura
dotnet restore                  # Restaurar paquetes NuGet
dotnet format                   # Formatear código según estándares
dotnet clean                    # Limpiar artefactos de compilación

# Comandos específicos del proyecto
dotnet run --project src/PortalEmpleo.Api    # Ejecutar API
dotnet watch --project src/PortalEmpleo.Api  # Desarrollo con hot reload
dotnet ef database update                    # Aplicar migraciones (cuando aplique)

# Comandos de testing específicos
dotnet test --logger trx                     # Tests con reporte TRX
dotnet test --filter "Category=Unit"         # Solo tests unitarios
dotnet test --filter "Category=Integration"  # Solo tests de integración
```

## Estándares

### Convenciones de Nomenclatura
| Elemento | Convención | Ejemplo |
|----------|------------|---------|
| Clases | PascalCase | `UserService`, `JobOfferController` |
| Interfaces | PascalCase con prefijo I | `IUserRepository`, `IJobOfferService` |
| Métodos públicos | PascalCase | `GetUserByIdAsync`, `CreateJobOfferAsync` |
| Propiedades públicas | PascalCase | `UserName`, `JobTitle`, `IsActive` |
| Campos privados | camelCase con _ | `_userRepository`, `_logger` |
| Constantes | PascalCase | `MaxPageSize`, `DefaultPageSize` |
| Variables locales | camelCase | `userEmail`, `jobOfferId` |
| Parámetros | camelCase | `userId`, `loginDto` |

### Documentación XML Obligatoria
```csharp
/// <summary>
/// Authenticates a user and returns JWT tokens.
/// </summary>
/// <param name="loginDto">Login credentials containing email and password.</param>
/// <returns>AuthResultDto containing access and refresh tokens.</returns>
/// <exception cref="InvalidCredentialsException">Thrown when credentials are invalid.</exception>
public async Task<AuthResultDto> LoginAsync(LoginDto loginDto)
```

### Principios de Desarrollo
- **SOLID Principles:** Aplicación estricta en todas las clases y servicios
- **Clean Architecture:** Separación clara de responsabilidades entre capas
- **Dependency Injection:** Inyección por constructor, configuración en Program.cs
- **Async/Await:** Obligatorio para todas las operaciones I/O (DB, HTTP, etc.)
- **FluentValidation:** Validación declarativa para todos los DTOs de entrada

### Patrones de Diseño Requeridos
- **Repository Pattern:** Para abstracción de acceso a datos
- **Unit of Work Pattern:** Para coordinar transacciones múltiples
- **DTO Pattern:** Para transferencia de datos entre capas
- **Service Layer Pattern:** Para lógica de negocio en Application Layer

### Estructura de Respuestas API
```csharp
// Respuesta exitosa con datos
public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; }
}

// Respuesta paginada
public class PaginatedResponse<T>
{
    public List<T> Data { get; set; }
    public PaginationMeta Meta { get; set; }
}

// Respuesta de error
public class ErrorResponse
{
    public string Error { get; set; }
    public string Message { get; set; }
    public Dictionary<string, string[]> ValidationErrors { get; set; }
}
```

## Límites

### ✅ Siempre Implementar
- **Documentación XML:** Para todos los métodos, clases e interfaces públicas
- **Async/await:** Para operaciones I/O, acceso a base de datos, llamadas HTTP
- **FluentValidation:** Validación robusta en todos los DTOs de entrada
- **Clean Architecture:** Respeto estricto de las 4 capas definidas
- **Logging estructurado:** Con correlation IDs para trazabilidad
- **Exception handling:** Middleware personalizado para manejo global de errores
- **Health checks:** Endpoints de salud para monitoreo
- **CORS configuration:** Configuración apropiada para entornos
- **JWT claims validation:** Verificación de claims en endpoints protegidos
- Siempre usar idioma español para los artefactos e inglés para aspectos técnicos (nombres de clases, métodos, variables, etc.) cuando sea necesario.

### ⚠️ Consultar Antes de Proceder
- **Cambios en esquema de base de datos:** Modificaciones a entidades o relaciones
- **Nuevos paquetes NuGet:** Adición de dependencias externas
- **Cambios en configuración de autenticación:** Modificación de JWT settings
- **Nuevos endpoints:** Adición de controladores o acciones no especificadas
- **Cambios en CORS policy:** Modificación de orígenes permitidos
- **Implementación de caching:** Estrategias de cache no definidas
- **Integración con servicios externos:** APIs de terceros
- **Cambios en pipeline CI/CD:** Modificaciones en workflow de GitHub Actions

### 🚫 Nunca Realizar
- **Commit de secretos:** JWT secrets, connection strings, API keys en código
- **Passwords en texto plano:** Siempre usar BCrypt con work factor 12
- **Omitir validación:** Toda entrada de usuario debe ser validada
- **Bypass de autenticación:** No omitir [Authorize] en endpoints protegidos
- **Exposición de entidades de dominio:** Usar DTOs en controladores
- **Hard delete de usuarios:** Implementar soft delete únicamente
- **Logging de información sensible:** Passwords, tokens, datos PII
- **Configuración hardcoded:** Usar variables de entorno y appsettings
- **Conexiones directas a DB desde controladores:** Respetar arquitectura de capas
- **Retorno de excepciones internas:** Usar middleware para respuestas de error estructuradas

### Configuración de Entorno Requerida
```bash
# JWT Configuration (variables de entorno)
JWT_SECRET_KEY=YourSuperSecretKeyAtLeast32CharactersLong
JWT_ISSUER=PortalEmpleo
JWT_AUDIENCE=PortalEmpleoUsers
JWT_ACCESS_TOKEN_EXPIRY_MINUTES=60
JWT_REFRESH_TOKEN_EXPIRY_DAYS=7

# Database Configuration
DATABASE__PROVIDER=InMemory
DATABASE__DB_NAME=PortalEmpleoDev

# CORS Configuration
CORS__ALLOWED_ORIGINS=http://localhost:3000,https://yourdomain.com

# Server Configuration
ASPNETCORE_URLS=http://localhost:5000
ASPNETCORE_ENVIRONMENT=Development
```

---

*Este documento establece el contexto y estándares para el desarrollo del Portal de Empleo API usando metodologías Spec-Driven Development con GitHub Copilot.*
---

## Agentes

### @backend-dev
Controladores, servicios de aplicación, lógica de negocio.

**Patrón de trabajo:**
1. Leer especificación de `specs/[feature]/spec.md`
2. Leer plan de `specs/[feature]/plan.md`
3. Generar código siguiendo AGENTS.md
4. Escribir tests

### @data-dev
EF Core, repositorios, modelos de datos.

**Patrón de trabajo:**
1. Leer especificación de entidades de dominio
2. Crear clases de entidades
3. Implementar interfaces de repositorio
4. Configurar mapeos de EF Core

### @test-dev
Tests unitarios, de integración, cobertura de código (≥80%).

**Patrón de trabajo:**
1. Leer especificación de feature
2. Crear tests con patrón arrange-act-assert
3. Simular dependencias con Moq
4. Usar FluentAssertions

