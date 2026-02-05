using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEmpleo.Domain.Entities;

namespace PortalEmpleo.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Core Fluent API configuration for RefreshToken entity.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Configures the RefreshToken entity mappings, constraints, and indexes.
    /// </summary>
    /// <param name="builder">Entity type builder for RefreshToken.</param>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        
        builder.HasKey(rt => rt.Id);
        
        // Token configuration - unique, required, max 500 chars
        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(500);
        builder.HasIndex(rt => rt.Token)
            .IsUnique();
        
        // UserId configuration - required foreign key
        builder.Property(rt => rt.UserId)
            .IsRequired();
        
        // ExpiresAt configuration - required
        builder.Property(rt => rt.ExpiresAt)
            .IsRequired();
        
        // CreatedAt configuration - required with default value
        builder.Property(rt => rt.CreatedAt)
            .IsRequired();
        
        // IsRevoked configuration - required with default value false
        builder.Property(rt => rt.IsRevoked)
            .IsRequired()
            .HasDefaultValue(false);
        
        // RevokedAt configuration - optional
        builder.Property(rt => rt.RevokedAt)
            .IsRequired(false);
        
        // Ignore computed properties (not mapped to database)
        builder.Ignore(rt => rt.IsActive);
    }
}
