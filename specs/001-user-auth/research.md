# Research: Gestión de Usuarios y Autenticación

**Feature**: 001-user-auth | **Date**: 2026-02-04  
**Purpose**: Resolver decisiones técnicas y mejores prácticas para implementación de autenticación JWT y gestión de usuarios con Clean Architecture en .NET 8.

---

## Phase 0: Research Findings

### 1. JWT Token Management con .NET 8

**Decision**: Usar `System.IdentityModel.Tokens.Jwt` con HS256 algorithm

**Rationale**:
- Biblioteca nativa de Microsoft con soporte oficial para .NET 8
- HS256 (symmetric signing) suficiente para arquitectura monolítica inicial
- Access token 60 min + Refresh token 7 días permite balance seguridad/UX
- Claims obligatorios: `sub` (userId), `email`, `role`, `exp`, `iss`, `aud`

**Implementation Pattern**:
```csharp
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    
    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _audience)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

**Alternatives Considered**:
- **RS256 (asymmetric)**: Rechazado por complejidad innecesaria en fase inicial sin múltiples consumers
- **Identity Server / Duende**: Rechazado por overhead en feature simple, evaluable en futuro
- **OAuth2 con Authorization Code Flow**: Fuera de scope, considera para integración futura con SSO

**Best Practices Applied**:
- Secret key ≥32 caracteres en variable de entorno `JWT_SECRET_KEY`
- Token rotation obligatorio cada 7 días mediante refresh token
- Validación de `exp` claim automática por ASP.NET Core middleware
- No almacenar access tokens (stateless), sí almacenar refresh tokens en DB

---

### 2. BCrypt Password Hashing - Work Factor Configuration

**Decision**: BCrypt.Net-Next v4.0.3+ con work factor 12

**Rationale**:
- Work factor 12 produce 200-300ms hashing time (especificación RNF-007)
- Balance seguridad vs UX: factor 10 = 100ms (débil), factor 14 = 1000ms (lento)
- BCrypt.Net-Next es thread-safe y compatible con .NET 8
- Auto-genera salt único por password (no requiere gestión manual)

**Implementation Pattern**:
```csharp
public class BcryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;
    
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }
    
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
```

**Alternatives Considered**:
- **Argon2**: Ganador Password Hashing Competition 2015, pero requiere dependencia externa adicional
- **PBKDF2 (ASP.NET Identity default)**: Menos resistente a GPU attacks que BCrypt
- **Scrypt**: Mayor uso memoria, overkill para caso de uso actual

**Best Practices Applied**:
- Never log passwords o hashes en structured logging
- Validar complejidad password en FluentValidation (≥8 chars, uppercase, lowercase, digit)
- Rate limiting en endpoint /auth/login para prevenir offline attacks
- Considerar PasswordHasher de ASP.NET Identity en futuro para compatibilidad con Identity framework

---

### 3. Entity Framework Core In-Memory Database para Desarrollo

**Decision**: EF Core In-Memory provider configurado para desarrollo, PostgreSQL para producción

**Rationale**:
- Velocidad setup: no requiere instalación DB, migraciones automáticas
- Testing simplificado: cada test crea DB independiente en memoria
- Facilita onboarding developers sin dependencias externas
- Preparado para PostgreSQL mediante Npgsql.EntityFrameworkCore.PostgreSQL

**Implementation Pattern**:
```csharp
// Program.cs - Development
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("PortalEmpleoDev");
    }
    else
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});
```

**Migration Strategy**:
- Desarrollo: In-Memory DB regenerada en cada startup
- Testing: In-Memory DB única por test method
- Producción: PostgreSQL con EF Core Migrations (`dotnet ef migrations add`)

**Alternatives Considered**:
- **SQLite In-Memory**: Más cercano a SQL real, pero limitaciones foreign keys y tipos
- **Docker PostgreSQL local**: Rechazado por complejidad setup inicial
- **SQL Server LocalDB**: Windows-only, equipo usa macOS/Linux

**Best Practices Applied**:
- Configurar `EnableSensitiveDataLogging` solo en Development
- Usar `EnsureDeleted() + EnsureCreated()` en test setup
- No compartir DbContext entre requests (scoped lifetime)
- Preparar archivo `docker-compose.yml` con PostgreSQL para staging

---

### 4. FluentValidation - Validación Declarativa de DTOs

**Decision**: FluentValidation 11.9+ para todos los DTOs de entrada

**Rationale**:
- Separación de concerns: validación fuera de controladores y entidades
- Testing independiente de validadores sin levantar API
- Expresividad: `RuleFor(x => x.Email).EmailAddress().NotEmpty()`
- Integración ASP.NET Core: `AddFluentValidationAutoValidation()`

**Implementation Pattern**:
```csharp
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido.")
            .EmailAddress().WithMessage("Formato de email inválido.")
            .Matches(@"^[^@]+@[^@]+\.[^@]+$").WithMessage("Debe cumplir RFC 5322.");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z]").WithMessage("Debe contener mayúscula.")
            .Matches(@"[a-z]").WithMessage("Debe contener minúscula.")
            .Matches(@"\d").WithMessage("Debe contener dígito.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Debe cumplir formato E.164.");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.UtcNow.AddYears(-16)).WithMessage("Debe ser mayor de 16 años.");
    }
}
```

**Registration in DI**:
```csharp
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
```

**Alternatives Considered**:
- **Data Annotations**: Menos expresivo, dificulta testing, mixing concerns
- **Manual validation en servicios**: Duplicación código, coupling
- **Guard clauses**: Útil para entidades, no para DTOs de entrada

**Best Practices Applied**:
- Validators como services registrados en DI (lifetime scoped)
- `TestValidate()` para tests unitarios de validators
- Mensajes de error en español (idioma del negocio)
- No validar en entidades de dominio (solo invariantes de negocio)

---

### 5. Repository + Unit of Work Pattern con EF Core

**Decision**: Implementar Repository pattern para abstracción de data access + Unit of Work para transacciones

**Rationale**:
- Testabilidad: permite mockear repositorios en tests de servicios (Application layer)
- Separación de concerns: Application layer no conoce EF Core
- Facilita cambio de ORM futuro (ej: Dapper para queries complejas)
- Unit of Work coordina múltiples operaciones en transacción única

**Implementation Pattern**:
```csharp
// Domain Layer - Interface
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
    Task UpdateAsync(User user, CancellationToken ct = default);
    Task DeleteAsync(User user, CancellationToken ct = default);
}

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitTransactionAsync(CancellationToken ct = default);
    Task RollbackTransactionAsync(CancellationToken ct = default);
}

// Infrastructure Layer - Implementation
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Users
            .Where(u => !u.IsDeleted) // Soft delete filter
            .FirstOrDefaultAsync(u => u.Id == id, ct);
    }
    
    // ... other methods
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new UserRepository(context);
        RefreshTokens = new RefreshTokenRepository(context);
    }
    
    public IUserRepository Users { get; }
    public IRefreshTokenRepository RefreshTokens { get; }
    
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
    
    // ... transaction methods
}
```

**Service Usage**:
```csharp
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new DuplicateEmailException();
        
        var user = new User { /* ... */ };
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        // Generate tokens...
    }
}
```

**Alternatives Considered**:
- **Direct DbContext injection**: Rechazado por coupling Application layer a EF Core
- **Generic Repository**: Rechazado por over-abstraction, preferir repositories específicos
- **CQRS con MediatR**: Overkill para CRUD simple, considerar en features complejas futuras

**Best Practices Applied**:
- Interfaces en Domain layer, implementaciones en Infrastructure layer
- CancellationToken en todos los métodos async para cancelación de requests largos
- No exponer IQueryable desde repositorios (evita query leakage)
- Soft delete filter aplicado en repositorio, no en cada query manual

---

### 6. Protección contra Brute Force - Rate Limiting

**Decision**: Middleware personalizado con in-memory tracking de intentos fallidos por IP/email

**Rationale**:
- RNF-008 especifica lockout 15 min tras 5 intentos consecutivos fallidos
- In-memory suficiente para single instance, Redis para multi-instance futuro
- Middleware aplica antes de autenticación (protege endpoint /auth/login)

**Implementation Pattern**:
```csharp
public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, LoginAttempt> _attempts = new();
    
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/v1/auth/login"))
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (ipAddress != null && _attempts.TryGetValue(ipAddress, out var attempt))
            {
                if (attempt.IsLockedOut())
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsJsonAsync(new { 
                        error = "TooManyRequests",
                        message = $"Cuenta bloqueada. Intenta en {attempt.RemainingLockoutTime()} minutos."
                    });
                    return;
                }
            }
        }
        
        await _next(context);
    }
}

public class LoginAttempt
{
    public int FailedCount { get; set; }
    public DateTime LastAttempt { get; set; }
    public DateTime? LockoutUntil { get; set; }
    
    public bool IsLockedOut() => LockoutUntil.HasValue && DateTime.UtcNow < LockoutUntil.Value;
    public int RemainingLockoutTime() => IsLockedOut() ? (int)(LockoutUntil!.Value - DateTime.UtcNow).TotalMinutes : 0;
}
```

**Tracking Logic en AuthService**:
```csharp
public async Task<AuthResultDto> LoginAsync(LoginDto dto)
{
    var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
    if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
    {
        TrackFailedAttempt(dto.Email); // Increment counter
        throw new InvalidCredentialsException();
    }
    
    ResetFailedAttempts(dto.Email); // Reset on success
    // Generate tokens...
}
```

**Alternatives Considered**:
- **AspNetCoreRateLimit library**: Rechazado por configuración compleja para caso simple
- **CAPTCHA tras 3 intentos**: Considerado para futuro, no en MVP
- **Exponential backoff**: Preferido fixed lockout por simplicidad (RNF-008 especifica 15 min)

**Best Practices Applied**:
- Cleanup automático de _attempts cada 24h (background service)
- Log de intentos fallidos con correlation ID para auditoría
- Lockout por IP + email (doble validación)
- Considerar Redis para compartir estado entre instancias en producción

---

## Summary of Technology Decisions

| Aspect | Technology | Justification |
|--------|-----------|---------------|
| **JWT Management** | System.IdentityModel.Tokens.Jwt + HS256 | Biblioteca nativa Microsoft, stateless auth, 60min access + 7d refresh |
| **Password Hashing** | BCrypt.Net-Next work factor 12 | Balance seguridad/rendimiento (200-300ms), auto-salt, thread-safe |
| **Database Dev** | EF Core In-Memory | Velocidad setup, testing simplificado, sin dependencias externas |
| **Database Prod** | PostgreSQL + Npgsql.EF | Open source, performance, JSON support, migraciones EF Core |
| **Validation** | FluentValidation 11.9+ | Separación concerns, testing independiente, expresividad |
| **Data Access** | Repository + Unit of Work | Abstracción EF Core, testabilidad, coordinación transacciones |
| **Rate Limiting** | Custom middleware + in-memory | RNF-008 compliance, simplicidad inicial, extensible a Redis |
| **Logging** | Serilog + structured logging | Correlation IDs, filtrado PII, JSON output para ELK stack |
| **Health Checks** | ASP.NET Core Health Checks | /health endpoint con DB check para orquestadores |

---

## Next Steps (Phase 1)

1. **data-model.md**: Diseñar entidades User, RefreshToken con relaciones y validation rules
2. **contracts/**: Generar OpenAPI schemas para 7 endpoints (register, login, refresh, CRUD users)
3. **quickstart.md**: Documentar setup local (.NET 8 SDK, JWT config, run commands)
4. **Update agent context**: Ejecutar `.specify/scripts/powershell/update-agent-context.ps1` para agregar decisiones técnicas a AGENTS.md

---

*Generated by /speckit.plan command - Phase 0 Research*
