using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortalEmpleo.Application.Interfaces;
using PortalEmpleo.Domain.Entities;

namespace PortalEmpleo.Infrastructure.Security;

/// <summary>
/// JWT token generator implementation using HS256 algorithm.
/// Generates access tokens (60 minutes) and refresh tokens (7 days).
/// </summary>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _accessTokenExpiryMinutes;
    private readonly int _refreshTokenExpiryDays;
    
    /// <summary>
    /// Initializes a new instance of JwtTokenGenerator.
    /// </summary>
    /// <param name="configuration">Application configuration containing JWT settings.</param>
    public JwtTokenGenerator(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"] 
            ?? throw new InvalidOperationException("JWT SecretKey not configured.");
        _issuer = configuration["Jwt:Issuer"] 
            ?? throw new InvalidOperationException("JWT Issuer not configured.");
        _audience = configuration["Jwt:Audience"] 
            ?? throw new InvalidOperationException("JWT Audience not configured.");
        _accessTokenExpiryMinutes = int.Parse(configuration["Jwt:AccessTokenExpiryMinutes"] ?? "60");
        _refreshTokenExpiryDays = int.Parse(configuration["Jwt:RefreshTokenExpiryDays"] ?? "7");
        
        // Validate secret key length (minimum 32 characters for HS256)
        if (_secretKey.Length < 32)
        {
            throw new InvalidOperationException("JWT SecretKey must be at least 32 characters long.");
        }
    }
    
    /// <summary>
    /// Generates a JWT access token for a user.
    /// Token expires in 60 minutes and contains claims: sub, email, role, exp, iss, aud.
    /// </summary>
    /// <param name="user">User entity for which to generate the token.</param>
    /// <returns>JWT access token string.</returns>
    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_accessTokenExpiryMinutes),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// Token is a random 64-byte Base64-encoded string.
    /// </summary>
    /// <returns>Random secure refresh token string.</returns>
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
    
    /// <summary>
    /// Gets the access token expiration time in minutes (60 minutes).
    /// </summary>
    public int AccessTokenExpiryMinutes => _accessTokenExpiryMinutes;
    
    /// <summary>
    /// Gets the refresh token expiration time in days (7 days).
    /// </summary>
    public int RefreshTokenExpiryDays => _refreshTokenExpiryDays;
}
