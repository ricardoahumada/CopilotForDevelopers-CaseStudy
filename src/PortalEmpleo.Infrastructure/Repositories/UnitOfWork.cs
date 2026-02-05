using PortalEmpleo.Domain.Interfaces;
using PortalEmpleo.Infrastructure.Data;

namespace PortalEmpleo.Infrastructure.Repositories;

/// <summary>
/// Unit of Work implementation coordinating repository transactions.
/// Ensures all changes are committed atomically.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IUserRepository? _userRepository;
    private IRefreshTokenRepository? _refreshTokenRepository;
    
    /// <summary>
    /// Initializes a new instance of UnitOfWork.
    /// </summary>
    /// <param name="context">Database context.</param>
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Repository for User entity operations.
    /// Lazy initialization on first access.
    /// </summary>
    public IUserRepository Users
    {
        get
        {
            _userRepository ??= new UserRepository(_context);
            return _userRepository;
        }
    }
    
    /// <summary>
    /// Repository for RefreshToken entity operations.
    /// Lazy initialization on first access.
    /// </summary>
    public IRefreshTokenRepository RefreshTokens
    {
        get
        {
            _refreshTokenRepository ??= new RefreshTokenRepository(_context);
            return _refreshTokenRepository;
        }
    }
    
    /// <summary>
    /// Saves all changes made in the current unit of work to the database.
    /// </summary>
    /// <returns>Number of entities affected by the operation.</returns>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Disposes the database context.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
