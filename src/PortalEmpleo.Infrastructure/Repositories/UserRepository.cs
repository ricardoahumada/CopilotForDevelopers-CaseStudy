using Microsoft.EntityFrameworkCore;
using PortalEmpleo.Domain.Entities;
using PortalEmpleo.Domain.Interfaces;
using PortalEmpleo.Infrastructure.Data;

namespace PortalEmpleo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for User entity operations.
/// Applies soft delete filtering automatically via EF Core query filters.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    /// <summary>
    /// Initializes a new instance of UserRepository.
    /// </summary>
    /// <param name="context">Database context.</param>
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// Soft delete filter applied automatically.
    /// </summary>
    /// <param name="id">User's unique identifier.</param>
    /// <returns>User entity if found and not deleted, null otherwise.</returns>
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    /// <summary>
    /// Retrieves a user by their email address.
    /// Soft delete filter applied automatically.
    /// </summary>
    /// <param name="email">User's email address.</param>
    /// <returns>User entity if found and not deleted, null otherwise.</returns>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    /// <summary>
    /// Retrieves all non-deleted users.
    /// Soft delete filter applied automatically.
    /// </summary>
    /// <returns>Collection of all active users.</returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    /// <summary>
    /// Adds a new user to the repository.
    /// </summary>
    /// <param name="user">User entity to add.</param>
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    
    /// <summary>
    /// Updates an existing user in the repository.
    /// </summary>
    /// <param name="user">User entity with updated values.</param>
    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Soft deletes a user (sets IsDeleted = true, DeletedAt = now).
    /// </summary>
    /// <param name="user">User entity to soft delete.</param>
    public Task DeleteAsync(User user)
    {
        user.SoftDelete();
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
}
