using PortalEmpleo.Application.Interfaces;

namespace PortalEmpleo.Infrastructure.Security;

/// <summary>
/// BCrypt password hasher implementation with work factor 12.
/// Provides secure password hashing and verification.
/// </summary>
public class BcryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;
    
    /// <summary>
    /// Hashes a plain text password using BCrypt with work factor 12.
    /// Automatically generates a unique salt for each password.
    /// Hashing time: approximately 200-300ms.
    /// </summary>
    /// <param name="password">Plain text password to hash.</param>
    /// <returns>BCrypt hashed password string.</returns>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }
    
    /// <summary>
    /// Verifies a plain text password against a BCrypt hash.
    /// Uses constant-time comparison to prevent timing attacks.
    /// </summary>
    /// <param name="password">Plain text password to verify.</param>
    /// <param name="hashedPassword">BCrypt hashed password to compare against.</param>
    /// <returns>True if password matches, false otherwise.</returns>
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
