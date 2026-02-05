using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEmpleo.Domain.Entities;

namespace PortalEmpleo.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Core Fluent API configuration for User entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the User entity mappings, constraints, and indexes.
    /// </summary>
    /// <param name="builder">Entity type builder for User.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        
        // Email configuration - unique, required, max 255 chars
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        // PasswordHash configuration - required, max 255 chars
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);
        
        // FirstName configuration - required, max 100 chars
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        // LastName configuration - required, max 100 chars
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        // PhoneNumber configuration - required, max 15 chars (E.164 format)
        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);
        
        // DateOfBirth configuration - required
        builder.Property(u => u.DateOfBirth)
            .IsRequired();
        
        // Role configuration - required, stored as string for readability
        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();
        
        // CreatedAt configuration - required with default value
        builder.Property(u => u.CreatedAt)
            .IsRequired();
        
        // UpdatedAt configuration - required with default value
        builder.Property(u => u.UpdatedAt)
            .IsRequired();
        
        // IsDeleted configuration - required with default value false
        builder.Property(u => u.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        // DeletedAt configuration - optional
        builder.Property(u => u.DeletedAt)
            .IsRequired(false);
        
        // Soft delete global query filter - excludes deleted users from queries
        builder.HasQueryFilter(u => !u.IsDeleted);
        
        // Relationship configuration - one User to many RefreshTokens
        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Ignore computed properties (not mapped to database)
        builder.Ignore(u => u.FullName);
    }
}
