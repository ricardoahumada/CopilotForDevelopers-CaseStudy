---
name: data-access
description: Generates repositories, DbContext, and Entity Framework Core configuration for PortalEmpleo. Use when implementing the data access layer, creating repository patterns, or configuring EF Core mappings.
license: MIT
metadata:
  author: course-team@netmind.es
  version: "1.0.0"
compatibility: Requires .NET 8 SDK and Entity Framework Core 8 with In-Memory provider
allowed-tools: Read Write Bash(git:*) Bash(dotnet:*)
---

# Data Access Skill - PortalEmpleo

## When to use this skill

Use this skill when implementing data access layer: repositories, DbContext, and EF Core configuration. This skill is automatically triggered when the user mentions:
- Repository pattern
- DbContext
- Entity Framework Core
- Data access layer

## Project Context

- **ORM:** Entity Framework Core 8 with In-Memory provider
- **Architecture:** Clean Architecture
- **Pattern:** Repository Pattern with Unit of Work
- **Database:** In-Memory for development/testing

## Repository Interface Standard

### IRepository Base Interface

```csharp
namespace PortalEmpleo.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> ExistsAsync(Guid id);
}
```

### Specific Repository Interface Example

```csharp
namespace PortalEmpleo.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmailAndRoleAsync(string email, UserRole role);
    Task<IEnumerable<User>> GetCandidatesAsync(int page, int pageSize);
}
```

## Repository Implementation Standard

```csharp
namespace PortalEmpleo.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
    }

    public void Update(User entity)
    {
        _context.Users.Update(entity);
    }

    public void Delete(User entity)
    {
        entity.IsActive = false;
        _context.Users.Update(entity);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Users
            .AnyAsync(u => u.Id == id && u.IsActive);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .ToListAsync();
    }
}
```

## DbContext Configuration

```Empleo.Infrastructurecsharp
namespace Portal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<JobOffer> JobOffers => Set<JobOffer>();
    public DbSet<Application> Applications => Set<Application>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configurations
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Role).HasConversion<string>();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        // JobOffer configurations
        modelBuilder.Entity<JobOffer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.ContractType).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.Company)
                  .WithMany()
                  .HasForeignKey(e => e.CompanyId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Application configurations
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.JobOffer)
                  .WithMany()
                  .HasForeignKey(e => e.JobOfferId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Candidate)
                  .WithMany()
                  .HasForeignKey(e => e.CandidateId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(e => new { e.JobOfferId, e.CandidateId }).IsUnique();
        });
    }
}
```

## Required Elements

### 1. Repository Pattern
- Generic IRepository<T>
- Specific repository interfaces
- Repository implementations

### 2. DbContext
- DbSet properties
- OnModelCreating with configurations
- Relationships and constraints

### 3. Conventions
- Soft delete using IsActive
- Unique indexes for emails
- Cascade delete configuration
- Conversions for enums

## Output Format

Generate:
- I[Entity]Repository.cs interfaces
- [Entity]Repository.cs implementations
- ApplicationDbContext.cs with configurations

## Files to Write

- `src/PortalEmpleo.Domain/Interfaces/IRepository.cs`
- `src/PortalEmpleo.Domain/Interfaces/I[Entity]Repository.cs`
- `src/PortalEmpleo.Infrastructure/Repositories/[Entity]Repository.cs`
- `src/PortalEmpleo.Infrastructure/Data/ApplicationDbContext.cs`
