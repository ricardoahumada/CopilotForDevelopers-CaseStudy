using PortalEmpleo.Domain.Entities;

namespace PortalEmpleo.Domain.Interfaces;

/// <summary>
/// Repository interface for User entity operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// Applies soft delete filter (excludes deleted users).
    /// </summary>
    /// <param name="id">User's unique identifier.</param>
    /// <returns>User entity if found and not deleted, null otherwise.</returns>
    Task<User?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Retrieves a user by their email address.
    /// Applies soft delete filter (excludes deleted users).
    /// </summary>
    /// <param name="email">User's email address.</param>
    /// <returns>User entity if found and not deleted, null otherwise.</returns>
    Task<User?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Retrieves all non-deleted users.
    /// Applies soft delete filter.
    /// </summary>
    /// <returns>Collection of all active users.</returns>
    Task<IEnumerable<User>> GetAllAsync();
    
    /// <summary>
    /// Adds a new user to the repository.
    /// </summary>
    /// <param name="user">User entity to add.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task AddAsync(User user);
    
    /// <summary>
    /// Updates an existing user in the repository.
    /// </summary>
    /// <param name="user">User entity with updated values.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task UpdateAsync(User user);
    
    /// <summary>
    /// Soft deletes a user (sets IsDeleted = true, DeletedAt = now).
    /// Does not physically remove the user from the database.
    /// </summary>
    /// <param name="user">User entity to soft delete.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteAsync(User user);
}
