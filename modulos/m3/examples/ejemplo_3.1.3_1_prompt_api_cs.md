# Prompt para API C#

**Contexto de uso:** Este prompt implementa controller de autenticación en ASP.NET Core con FluentValidation y ProblemDetails.

**Prompt completo:**
```
Implementa el controller de autenticación siguiendo AGENTS.md:

## Especificación
- specs/001-auth-api/spec.md
- Endpoint: POST /api/auth/login

## Convenciones AGENTS.md
- Controller con atributos de documentación
- FluentValidation para DTOs
- ProblemDetails para errores
- CancellationToken en todos los métodos

## Controller Template
```csharp
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<LoginRequestDto> _loginValidator;

    public AuthController(
        IAuthService authService,
        IValidator<LoginRequestDto> loginValidator)
    {
        _authService = authService;
        _loginValidator = loginValidator;
    }

    /// <summary>
    /// Autentica un usuario y retorna tokens JWT
    /// </summary>
    /// <param name="request">Credenciales del usuario</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Tokens de acceso y refresh</returns>
    /// <response code="200">Login exitoso</response>
    /// <response code="401">Credenciales inválidas</response>
    /// <response code="400">Error de validación</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        // Validación
        var validationResult = await _loginValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        // Ejecución
        var result = await _authService.LoginAsync(request, cancellationToken);

        // Respuesta
        if (result.IsFailure)
        {
            return Unauthorized(new ProblemDetails
            {
                Title = "Authentication failed",
                Detail = result.Error,
                Status = StatusCodes.Status401Unauthorized
            });
        }

        return Ok(result.Value);
    }
}
```

## FluentValidation
```csharp
public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password es obligatorio")
            .MinimumLength(8).WithMessage("Password mínimo 8 caracteres");
    }
}
```
```
