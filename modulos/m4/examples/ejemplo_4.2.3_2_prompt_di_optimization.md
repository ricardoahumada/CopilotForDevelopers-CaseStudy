# Prompt para Dependency Injection Optimization

**Contexto de uso:** Este prompt optimiza la configuración de Dependency Injection usando assembly scanning.

**Prompt completo:**
```
@agent Optimiza configuración de Dependency Injection:

## Problema: Registro manual de 35+ servicios

## Refactoring a Assembly Scanning
```csharp
public static class ServiceCollectionExtensions
{
    public static void RegisterAssemblyServices(this IServiceCollection services)
    {
        var assembly = Assembly.Load("PortalEmpleo.Application");
        
        var registrations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.Name.StartsWith('I') && 
                    (i.Name.EndsWith("Service") || i.Name.EndsWith("Repository") || i.Name.EndsWith("Validator"))))
            .Select(i => new { Interface = i, Implementation = assembly.GetTypes()
                .FirstOrDefault(c => c.GetInterfaces().Contains(i) && c.Name == i.Name.Substring(1)) })
            .Where(x => x.Implementation != null);
        
        foreach (var reg in registrations)
            services.AddScoped(reg.Interface, reg.Implementation);
    }
}

// Usage
services.RegisterAssemblyServices();
```
```
