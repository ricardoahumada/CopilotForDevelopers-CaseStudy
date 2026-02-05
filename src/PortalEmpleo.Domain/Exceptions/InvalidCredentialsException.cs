namespace PortalEmpleo.Domain.Exceptions;

/// <summary>
/// Exception thrown when user authentication fails due to invalid credentials.
/// </summary>
public class InvalidCredentialsException : Exception
{
    /// <summary>
    /// Initializes a new instance of InvalidCredentialsException.
    /// </summary>
    public InvalidCredentialsException() 
        : base("Invalid email or password.")
    {
    }
    
    /// <summary>
    /// Initializes a new instance of InvalidCredentialsException with a custom message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public InvalidCredentialsException(string message) 
        : base(message)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of InvalidCredentialsException with a custom message and inner exception.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidCredentialsException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
