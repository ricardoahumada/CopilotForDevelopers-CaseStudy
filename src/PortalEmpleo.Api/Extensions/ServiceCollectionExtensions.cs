using Microsoft.EntityFrameworkCore;
using PortalEmpleo.Application.Interfaces;
using PortalEmpleo.Application.Services;
using PortalEmpleo.Domain.Interfaces;
using PortalEmpleo.Infrastructure.Data;
using PortalEmpleo.Infrastructure.Repositories;
using PortalEmpleo.Infrastructure.Security;

namespace PortalEmpleo.Api.Extensions;

/// <summary>
/// Extension methods for configuring dependency injection services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers database context and repositories in dependency injection container.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register ApplicationDbContext with In-Memory Database for Development
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        
        if (environment == "Development")
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("PortalEmpleoDev"));
        }
        else
        {
            // PostgreSQL connection for Production (requires CONNECTION_STRING environment variable)
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
        
        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
    
    /// <summary>
    /// Registers application services (AuthService, security services) in dependency injection container.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register authentication service
        services.AddScoped<IAuthService, AuthService>();
        
        // Register security services
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return services;
    }
}
