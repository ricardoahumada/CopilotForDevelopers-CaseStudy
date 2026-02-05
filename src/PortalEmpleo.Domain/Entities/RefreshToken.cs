namespace PortalEmpleo.Domain.Entities;

/// <summary>
/// Represents a refresh token for JWT authentication.
/// Allows users to obtain new access tokens without re-authenticating.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Unique identifier for the refresh token.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Foreign key to the User who owns this token.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// The actual refresh token string (random secure string).
    /// Must be unique across all tokens.
    /// </summary>
    public string Token { get; set; } = string.Empty;
    
    /// <summary>
    /// Timestamp when the token expires (UTC). Typically 7 days from creation.
    /// </summary>
    public DateTime ExpiresAt { get; set; }
    
    /// <summary>
    /// Timestamp when the token was created (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Flag indicating if the token has been manually revoked (e.g., logout).
    /// </summary>
    public bool IsRevoked { get; set; }
    
    /// <summary>
    /// Timestamp when the token was revoked (UTC). Null if not revoked.
    /// </summary>
    public DateTime? RevokedAt { get; set; }
    
    /// <summary>
    /// Navigation property to the User who owns this token.
    /// </summary>
    public virtual User User { get; set; } = null!;
    
    /// <summary>
    /// Computed property: Check if token is currently valid (not expired, not revoked).
    /// </summary>
    public bool IsActive => !IsRevoked && ExpiresAt > DateTime.UtcNow;
    
    /// <summary>
    /// Domain behavior: Revoke the token (e.g., on logout or security event).
    /// </summary>
    public void Revoke()
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
    }
}
