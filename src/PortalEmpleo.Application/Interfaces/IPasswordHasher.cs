namespace PortalEmpleo.Application.Interfaces;

/// <summary>
/// Interface for password hashing operations using BCrypt.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a plain text password using BCrypt with work factor 12.
    /// </summary>
    /// <param name="password">Plain text password to hash.</param>
    /// <returns>BCrypt hashed password string.</returns>
    string HashPassword(string password);
    
    /// <summary>
    /// Verifies a plain text password against a BCrypt hash.
    /// </summary>
    /// <param name="password">Plain text password to verify.</param>
    /// <param name="hashedPassword">BCrypt hashed password to compare against.</param>
    /// <returns>True if password matches, false otherwise.</returns>
    bool VerifyPassword(string password, string hashedPassword);
}
