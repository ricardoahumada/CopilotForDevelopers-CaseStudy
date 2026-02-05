namespace PortalEmpleo.Domain.Interfaces;

/// <summary>
/// Unit of Work pattern interface for coordinating repository transactions.
/// Ensures all changes are committed atomically.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Repository for User entity operations.
    /// </summary>
    IUserRepository Users { get; }
    
    /// <summary>
    /// Repository for RefreshToken entity operations.
    /// </summary>
    IRefreshTokenRepository RefreshTokens { get; }
    
    /// <summary>
    /// Saves all changes made in the current unit of work to the database.
    /// </summary>
    /// <returns>Number of entities affected by the operation.</returns>
    Task<int> SaveChangesAsync();
}
