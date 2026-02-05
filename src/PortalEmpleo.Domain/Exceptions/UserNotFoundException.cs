namespace PortalEmpleo.Domain.Exceptions;

/// <summary>
/// Exception thrown when a requested user is not found in the database.
/// </summary>
public class UserNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of UserNotFoundException.
    /// </summary>
    public UserNotFoundException() 
        : base("User not found.")
    {
    }
    
    /// <summary>
    /// Initializes a new instance of UserNotFoundException with a user identifier.
    /// </summary>
    /// <param name="userId">The identifier of the user that was not found.</param>
    public UserNotFoundException(Guid userId) 
        : base($"User with ID '{userId}' not found.")
    {
    }
    
    /// <summary>
    /// Initializes a new instance of UserNotFoundException with a custom message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public UserNotFoundException(string message) 
        : base(message)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of UserNotFoundException with a custom message and inner exception.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public UserNotFoundException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
