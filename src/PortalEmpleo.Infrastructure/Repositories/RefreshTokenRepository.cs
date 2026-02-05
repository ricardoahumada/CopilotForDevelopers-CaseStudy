using Microsoft.EntityFrameworkCore;
using PortalEmpleo.Domain.Entities;
using PortalEmpleo.Domain.Interfaces;
using PortalEmpleo.Infrastructure.Data;

namespace PortalEmpleo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for RefreshToken entity operations.
/// </summary>
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;
    
    /// <summary>
    /// Initializes a new instance of RefreshTokenRepository.
    /// </summary>
    /// <param name="context">Database context.</param>
    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Retrieves a refresh token by its token string.
    /// </summary>
    /// <param name="token">The refresh token string.</param>
    /// <returns>RefreshToken entity if found, null otherwise.</returns>
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }
    
    /// <summary>
    /// Retrieves all active (not expired, not revoked) refresh tokens for a user.
    /// </summary>
    /// <param name="userId">User's unique identifier.</param>
    /// <returns>Collection of active refresh tokens for the user.</returns>
    public async Task<IEnumerable<RefreshToken>> GetActiveByUserIdAsync(Guid userId)
    {
        var now = DateTime.UtcNow;
        return await _context.RefreshTokens
            .Where(rt => rt.UserId == userId 
                      && !rt.IsRevoked 
                      && rt.ExpiresAt > now)
            .ToListAsync();
    }
    
    /// <summary>
    /// Adds a new refresh token to the repository.
    /// </summary>
    /// <param name="refreshToken">RefreshToken entity to add.</param>
    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }
    
    /// <summary>
    /// Updates an existing refresh token in the repository.
    /// </summary>
    /// <param name="refreshToken">RefreshToken entity with updated values.</param>
    public Task UpdateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        return Task.CompletedTask;
    }
}
