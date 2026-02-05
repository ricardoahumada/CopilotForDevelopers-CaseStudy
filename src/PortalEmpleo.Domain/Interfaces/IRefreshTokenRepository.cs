using PortalEmpleo.Domain.Entities;

namespace PortalEmpleo.Domain.Interfaces;

/// <summary>
/// Repository interface for RefreshToken entity operations.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Retrieves a refresh token by its token string.
    /// </summary>
    /// <param name="token">The refresh token string.</param>
    /// <returns>RefreshToken entity if found, null otherwise.</returns>
    Task<RefreshToken?> GetByTokenAsync(string token);
    
    /// <summary>
    /// Retrieves all active (not expired, not revoked) refresh tokens for a user.
    /// </summary>
    /// <param name="userId">User's unique identifier.</param>
    /// <returns>Collection of active refresh tokens for the user.</returns>
    Task<IEnumerable<RefreshToken>> GetActiveByUserIdAsync(Guid userId);
    
    /// <summary>
    /// Adds a new refresh token to the repository.
    /// </summary>
    /// <param name="refreshToken">RefreshToken entity to add.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task AddAsync(RefreshToken refreshToken);
    
    /// <summary>
    /// Updates an existing refresh token in the repository.
    /// </summary>
    /// <param name="refreshToken">RefreshToken entity with updated values.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task UpdateAsync(RefreshToken refreshToken);
}
