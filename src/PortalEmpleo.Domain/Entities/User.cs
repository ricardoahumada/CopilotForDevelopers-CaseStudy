using PortalEmpleo.Domain.Enums;

namespace PortalEmpleo.Domain.Entities;

/// <summary>
/// Represents a user in the Portal Empleo system.
/// Supports CANDIDATE (job seekers), COMPANY (recruiters), and ADMIN roles.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// User's email address. Must be unique and comply with RFC 5322.
    /// Immutable after creation (cannot be updated via UpdateUserDto).
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// BCrypt hashed password with work factor 12.
    /// Never returned in API responses or logged.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's phone number in E.164 international format (e.g., +1234567890).
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// User's date of birth. Must be at least 16 years old.
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// User's role in the system (CANDIDATE, COMPANY, ADMIN).
    /// </summary>
    public UserRole Role { get; set; }
    
    /// <summary>
    /// Timestamp when the user was created (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Timestamp when the user was last updated (UTC).
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Soft delete flag. When true, user is logically deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Timestamp when the user was soft deleted (UTC). Null if not deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    
    /// <summary>
    /// Navigation property for refresh tokens associated with this user.
    /// </summary>
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    
    /// <summary>
    /// Computed property returning full name (FirstName + LastName).
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
    
    /// <summary>
    /// Domain validation: Check if user meets minimum age requirement (16 years).
    /// </summary>
    public bool IsAgeValid()
    {
        var minDate = DateTime.UtcNow.AddYears(-16);
        return DateOfBirth < minDate;
    }
    
    /// <summary>
    /// Domain behavior: Soft delete the user.
    /// </summary>
    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Domain behavior: Restore a soft deleted user.
    /// </summary>
    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}
