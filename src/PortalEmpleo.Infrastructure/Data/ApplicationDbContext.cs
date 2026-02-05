using Microsoft.EntityFrameworkCore;
using PortalEmpleo.Domain.Entities;

namespace PortalEmpleo.Infrastructure.Data;

/// <summary>
/// Entity Framework Core database context for Portal Empleo application.
/// Manages User and RefreshToken entities with Fluent API configurations.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of ApplicationDbContext.
    /// </summary>
    /// <param name="options">DbContext configuration options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    /// <summary>
    /// DbSet for User entities.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;
    
    /// <summary>
    /// DbSet for RefreshToken entities.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    
    /// <summary>
    /// Configures entity mappings using Fluent API.
    /// </summary>
    /// <param name="modelBuilder">Model builder for entity configuration.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    /// <summary>
    /// Override SaveChangesAsync to automatically update timestamps.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Number of entities affected.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is User && (e.State == EntityState.Added || e.State == EntityState.Modified));
        
        foreach (var entry in entries)
        {
            var user = (User)entry.Entity;
            
            if (entry.State == EntityState.Added)
            {
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                user.UpdatedAt = DateTime.UtcNow;
            }
        }
        
        var tokenEntries = ChangeTracker.Entries()
            .Where(e => e.Entity is RefreshToken && e.State == EntityState.Added);
        
        foreach (var entry in tokenEntries)
        {
            var token = (RefreshToken)entry.Entity;
            token.CreatedAt = DateTime.UtcNow;
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}
