using PortalEmpleo.Application.DTOs.Auth;
using PortalEmpleo.Application.Interfaces;
using PortalEmpleo.Domain.Entities;
using PortalEmpleo.Domain.Enums;
using PortalEmpleo.Domain.Exceptions;
using PortalEmpleo.Domain.Interfaces;

namespace PortalEmpleo.Application.Services;

/// <summary>
/// Service implementation for authentication operations.
/// Handles user registration, login, and token management.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    /// <summary>
    /// Initializes a new instance of AuthService.
    /// </summary>
    /// <param name="unitOfWork">Unit of work for database operations.</param>
    /// <param name="passwordHasher">Password hashing service.</param>
    /// <param name="jwtTokenGenerator">JWT token generation service.</param>
    public AuthService(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    /// <summary>
    /// Registers a new user with CANDIDATE role.
    /// Validates email uniqueness, hashes password with BCrypt, and generates JWT tokens.
    /// </summary>
    /// <param name="registerDto">Registration data containing email, password, and user details.</param>
    /// <returns>AuthResultDto with access token, refresh token, and expiration timestamp.</returns>
    /// <exception cref="DuplicateEmailException">Thrown when email already exists.</exception>
    public async Task<AuthResultDto> RegisterAsync(RegisterDto registerDto)
    {
        // Validate email uniqueness
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            throw new DuplicateEmailException(registerDto.Email);
        }
        
        // Hash password using BCrypt with work factor 12
        var passwordHash = _passwordHasher.HashPassword(registerDto.Password);
        
        // Create new user entity with CANDIDATE role
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = registerDto.Email,
            PasswordHash = passwordHash,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PhoneNumber = registerDto.PhoneNumber,
            DateOfBirth = registerDto.DateOfBirth,
            Role = UserRole.CANDIDATE,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        
        // Add user to repository
        await _unitOfWork.Users.AddAsync(user);
        
        // Generate JWT tokens
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshTokenString = _jwtTokenGenerator.GenerateRefreshToken();
        
        // Create refresh token entity
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshTokenString,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtTokenGenerator.RefreshTokenExpiryDays),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };
        
        // Add refresh token to repository
        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
        
        // Save all changes to database
        await _unitOfWork.SaveChangesAsync();
        
        // Return authentication result
        return new AuthResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenString,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtTokenGenerator.AccessTokenExpiryMinutes)
        };
    }
}
