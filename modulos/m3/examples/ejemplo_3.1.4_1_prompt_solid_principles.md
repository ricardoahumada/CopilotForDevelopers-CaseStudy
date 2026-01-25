# Prompt para SOLID Principles

**Contexto de uso:** Este prompt refactoriza AuthService aplicando principios SOLID en C#.

**Prompt completo:**
```
Refactoriza AuthService aplicando principios SOLID:

## Problema Actual
```csharp
// Violates SRP, ISP, DIP
public class AuthService : IAuthService
{
    private readonly PortalEmpleoDbContext _context;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly PasswordHasher _passwordHasher;
    private readonly EmailService _emailService;
    private readonly CacheService _cache;
    
    public async Task<LoginResult> Login(string email, string password)
    {
        // Login logic
        // Token generation
        // Password verification
        // Email sending
        // Caching
    }
}
```

## Aplicación de SOLID

### Single Responsibility Principle (SRP)
- AuthService: solo autenticación
- TokenService: generación de tokens
- PasswordService: hash y verificación
- EmailService: envío de emails (separado)

### Interface Segregation Principle (ISP)
- Interfaces pequeñas y específicas
- IAuthService: solo Login
- ITokenService: solo tokens
- IPasswordService: solo passwords

### Dependency Inversion Principle (DIP)
- Depender de abstracciones, no concreciones
- Injectar interfaces, no clases concretas

## Formato de Salida
```csharp
// SRP: AuthService - solo autenticación
public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(
        LoginRequestDto dto, 
        CancellationToken ct);
}

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IPasswordService passwordService,
        ITokenService tokenService,
        ILogger<AuthService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(
        LoginRequestDto dto, 
        CancellationToken ct)
    {
        // 1. Get user
        var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email, ct);
        if (usuario == null)
            return Result<LoginResponseDto>.Failure("Credenciales inválidas");

        // 2. Verify password (delegado)
        if (!_passwordService.VerifyPassword(dto.Password, usuario.PasswordHash))
            return Result<LoginResponseDto>.Failure("Credenciales inválidas");

        // 3. Generate tokens (delegado)
        var accessToken = _tokenService.GenerateAccessToken(usuario);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(usuario.Id, ct);

        _logger.LogInformation("User {Email} logged in", usuario.Email);

        return Result<LoginResponseDto>.Success(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        });
    }
}

// ISP: Interfaces pequeñas
public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}

public interface ITokenService
{
    string GenerateAccessToken(Usuario usuario);
    Task<RefreshToken> GenerateRefreshTokenAsync(Guid usuarioId, CancellationToken ct);
}

// DIP: Depender de abstracciones
public class AuthServiceTests
{
    private readonly Mock<IUsuarioRepository> _mockRepo;
    private readonly Mock<IPasswordService> _mockPassword;
    private readonly Mock<ITokenService> _mockToken;
    
    public AuthServiceTests()
    {
        _mockRepo = new Mock<IUsuarioRepository>();
        _mockPassword = new Mock<IPasswordService>();
        _mockToken = new Mock<ITokenService>();
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsSuccess()
    {
        // Arrange
        var service = new AuthService(
            _mockRepo.Object,
            _mockPassword.Object,
            _mockToken.Object,
            Mock.Of<ILogger<AuthService>>());

        // Test
        var result = service.LoginAsync(...);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
```
```
