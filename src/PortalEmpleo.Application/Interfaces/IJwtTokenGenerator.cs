using PortalEmpleo.Domain.Entities;

namespace PortalEmpleo.Application.Interfaces;

/// <summary>
/// Interface for JWT token generation operations.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT access token for a user.
    /// Token expires in 60 minutes and contains claims: sub, email, role, exp, iss, aud.
    /// </summary>
    /// <param name="user">User entity for which to generate the token.</param>
    /// <returns>JWT access token string.</returns>
    string GenerateAccessToken(User user);
    
    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// Token is stored in database and expires in 7 days.
    /// </summary>
    /// <returns>Random secure refresh token string (64 bytes Base64-encoded).</returns>
    string GenerateRefreshToken();
    
    /// <summary>
    /// Gets the access token expiration time in minutes (60 minutes).
    /// </summary>
    int AccessTokenExpiryMinutes { get; }
    
    /// <summary>
    /// Gets the refresh token expiration time in days (7 days).
    /// </summary>
    int RefreshTokenExpiryDays { get; }
}
