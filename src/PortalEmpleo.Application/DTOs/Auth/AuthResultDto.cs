namespace PortalEmpleo.Application.DTOs.Auth;

/// <summary>
/// Data transfer object for authentication result.
/// Contains JWT access token, refresh token, and expiration timestamp.
/// </summary>
public class AuthResultDto
{
    /// <summary>
    /// JWT access token (valid for 60 minutes).
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;
    
    /// <summary>
    /// Refresh token for obtaining new access tokens (valid for 7 days).
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
    
    /// <summary>
    /// Access token expiration timestamp (UTC).
    /// </summary>
    public DateTime ExpiresAt { get; set; }
}
