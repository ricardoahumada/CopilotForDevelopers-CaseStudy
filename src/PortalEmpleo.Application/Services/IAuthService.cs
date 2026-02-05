using PortalEmpleo.Application.DTOs.Auth;

namespace PortalEmpleo.Application.Services;

/// <summary>
/// Interface for authentication service operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user with CANDIDATE role.
    /// Validates email uniqueness, hashes password with BCrypt, and generates JWT tokens.
    /// </summary>
    /// <param name="registerDto">Registration data containing email, password, and user details.</param>
    /// <returns>AuthResultDto with access token, refresh token, and expiration timestamp.</returns>
    /// <exception cref="Domain.Exceptions.DuplicateEmailException">Thrown when email already exists.</exception>
    Task<AuthResultDto> RegisterAsync(RegisterDto registerDto);
}
