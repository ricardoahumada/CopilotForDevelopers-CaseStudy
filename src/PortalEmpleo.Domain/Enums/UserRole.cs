namespace PortalEmpleo.Domain.Enums;

/// <summary>
/// Represents the roles available in the Portal Empleo system.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Job seeker / professional looking for employment opportunities.
    /// </summary>
    CANDIDATE = 0,
    
    /// <summary>
    /// Company recruiter / employer posting job offers.
    /// </summary>
    COMPANY = 1,
    
    /// <summary>
    /// System administrator with full access.
    /// </summary>
    ADMIN = 2
}
