namespace PortalEmpleo.Domain.Exceptions;

/// <summary>
/// Exception thrown when attempting to register a user with an email that already exists.
/// </summary>
public class DuplicateEmailException : Exception
{
    /// <summary>
    /// Initializes a new instance of DuplicateEmailException.
    /// </summary>
    public DuplicateEmailException() 
        : base("A user with this email address already exists.")
    {
    }
    
    /// <summary>
    /// Initializes a new instance of DuplicateEmailException with a custom message.
    /// </summary>
    /// <param name="email">The duplicate email address.</param>
    public DuplicateEmailException(string email) 
        : base($"A user with email '{email}' already exists.")
    {
    }
    
    /// <summary>
    /// Initializes a new instance of DuplicateEmailException with a custom message and inner exception.
    /// </summary>
    /// <param name="email">The duplicate email address.</param>
    /// <param name="innerException">The inner exception.</param>
    public DuplicateEmailException(string email, Exception innerException) 
        : base($"A user with email '{email}' already exists.", innerException)
    {
    }
}
