using Microsoft.AspNetCore.Mvc;
using PortalEmpleo.Application.DTOs.Auth;
using PortalEmpleo.Application.Services;
using PortalEmpleo.Domain.Exceptions;

namespace PortalEmpleo.Api.Controllers;

/// <summary>
/// Controller for authentication operations (register, login, refresh token).
/// </summary>
[ApiController]
[Route("api/v1/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    
    /// <summary>
    /// Initializes a new instance of AuthController.
    /// </summary>
    /// <param name="authService">Authentication service.</param>
    /// <param name="logger">Logger instance.</param>
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    
    /// <summary>
    /// Registers a new user with CANDIDATE role.
    /// </summary>
    /// <param name="registerDto">Registration data containing email, password, and user details.</param>
    /// <returns>Authentication result with JWT tokens.</returns>
    /// <response code="201">User registered successfully. Returns access token and refresh token.</response>
    /// <response code="400">Validation error. One or more fields are invalid.</response>
    /// <response code="409">Email already exists in the system.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthResultDto>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            
            _logger.LogInformation("User registered successfully with email: {Email}", registerDto.Email);
            
            return CreatedAtAction(
                nameof(Register),
                new { email = registerDto.Email },
                result
            );
        }
        catch (DuplicateEmailException ex)
        {
            _logger.LogWarning("Registration failed - duplicate email: {Email}", registerDto.Email);
            
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "DuplicateEmail",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during user registration for email: {Email}", registerDto.Email);
            
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "InternalServerError",
                Detail = "An unexpected error occurred during registration.",
                Instance = HttpContext.Request.Path
            });
        }
    }
}
