# Prompt para Transformar Requisitos a Código

**Contexto de uso:** Este prompt transforma especificaciones funcionales en código C# para módulo de autenticación JWT.

**Prompt completo:**
```
Implementa el módulo de autenticación JWT según specs/001-auth-api/spec.md:

## Especificación de Referencia
- Archivo: specs/001-auth-api/spec.md
- Requisito: REQ-AUTH-001 (Autenticación JWT)
- Criterios de aceptación: CA-AUTH-001 a CA-AUTH-005

## Requisitos Funcionales
1. Usuario envía credenciales (email, password)
2. Sistema valida credenciales contra base de datos
3. Sistema genera JWT con claims personalizados
4. Sistema retorna token con expiración configurada
5. Usuario puede refresh token antes de expiración

## Entidades y Modelos
```csharp
public class Usuario : BaseEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string? Nombre { get; private set; }
    public Guid RolId { get; private set; }
    public bool EstaActivo { get; private set; }
    public DateTime? UltimoLogin { get; private set; }
}

public class RefreshToken : BaseEntity
{
    public Guid UsuarioId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiraEn { get; private set; }
    public bool EstaRevocado { get; private set; }
}
```

## JWT Configuration
```json
{
  "Jwt": {
    "SecretKey": "long-secret-key-min-32-chars",
    "Issuer": "PortalEmpleo",
    "Audience": "PortalEmpleo-API",
    "AccessTokenExpirationMinutes": 30,
    "RefreshTokenExpirationDays": 7
  }
}
```

## Convenciones del Proyecto
- Clean Architecture con Dependency Injection
- FluentValidation para DTOs
- Result pattern para respuestas
- Exception handling con ProblemDetails

## Formato de Salida
```csharp
// DTOs
public class LoginRequestDto
{
    [Required(ErrorMessage = "Email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password es obligatorio")]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public Guid UsuarioId { get; set; }
    public string Email { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
}

// Service Interface
public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto dto, CancellationToken ct);
    Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken, CancellationToken ct);
    Task<Result> LogoutAsync(Guid usuarioId, CancellationToken ct);
    Task<Result> RevokeTokenAsync(string refreshToken, CancellationToken ct);
}

// Service Implementation
public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto dto, CancellationToken ct)
    {
        // 1. Validar credenciales
        var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email, ct);
        if (usuario == null)
            return Result<LoginResponseDto>.Failure("Credenciales inválidas");

        // 2. Verificar password
        if (!VerifyPassword(dto.Password, usuario.PasswordHash))
            return Result<LoginResponseDto>.Failure("Credenciales inválidas");

        // 3. Verificar estado de usuario
        if (!usuario.EstaActivo)
            return Result<LoginResponseDto>.Failure("Usuario inactivo");

        // 4. Generar tokens
        var accessToken = _tokenService.GenerateAccessToken(usuario);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(usuario.Id, ct);

        // 5. Actualizar último login
        usuario.ActualizarUltimoLogin();
        await _usuarioRepository.UpdateAsync(usuario, ct);

        _logger.LogInformation("Usuario {Email} inició sesión", usuario.Email);

        return Result<LoginResponseDto>.Success(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            UsuarioId = usuario.Id,
            Email = usuario.Email,
            Roles = new List<string> { "Employee" }
        });
    }
}
```

## Trazabilidad
| Requisito | Criterio | Implementado en |
|-----------|----------|-----------------|
| REQ-AUTH-001 | Credenciales → JWT | AuthService.LoginAsync |
| REQ-AUTH-002 | Refresh token | AuthService.RefreshTokenAsync |
| REQ-AUTH-003 | Logout | AuthService.LogoutAsync |
```
