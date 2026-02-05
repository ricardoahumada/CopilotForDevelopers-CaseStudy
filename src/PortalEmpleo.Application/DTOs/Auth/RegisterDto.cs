namespace PortalEmpleo.Application.DTOs.Auth;

/// <summary>
/// Data transfer object for user registration request.
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// User's email address (unique, RFC 5322 format).
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's password (min 8 chars, uppercase, lowercase, digit).
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's phone number in E.164 international format.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// User's date of birth (must be at least 16 years old).
    /// </summary>
    public DateTime DateOfBirth { get; set; }
}
