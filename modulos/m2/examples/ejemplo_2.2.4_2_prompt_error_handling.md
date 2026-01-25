# Prompt: Implementar Error Handling Según Constitution.md

**Contexto de uso:** Prompt para implementar manejo de errores consistente según los estándares del proyecto.

**Prompt para implementar error handling:**

````
Implementa error handling consistente según constitution.md:

## Requisitos de constitution.md
- Todas las APIs retornan ProblemDetails
- Logging estructurado con ILogger
- No exponer PII en mensajes de error
- Correlation ID para trazabilidad

## Patrones Requeridos

### Exception Handler Global
```csharp
app.UseExceptionHandler(exceptionHandler =>
{
    exceptionHandler.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occurred",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Instance = context.TraceIdentifier
        };
        
        if (exception is not null)
        {
            problem.Detail = exception.Message;
            problem.Extensions["correlationId"] = context.TraceIdentifier;
            
            // Log estructurado
            _logger.LogError(exception, "Unhandled exception: {CorrelationId}", context.TraceIdentifier);
        }
        
        context.Response.StatusCode = problem.Status.Value;
        context.Response.ContentType = "application/problem+json";
        
        await context.Response.WriteAsJsonAsync(problem);
    });
});
```

### Custom Exceptions
```csharp
// Custom Exception Base
public abstract class AppException : Exception
{
    public string ErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public object? ValidationErrors { get; }

    protected AppException(string message, string errorCode, HttpStatusCode statusCode)
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}

// 404 Not Found
public class NotFoundException : AppException
{
    public NotFoundException(string message) 
        : base(message, "NOT_FOUND", HttpStatusCode.NotFound) { }
}

// 400 Bad Request
public class ValidationException : AppException
{
    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation failed", "VALIDATION_ERROR", HttpStatusCode.BadRequest)
    {
        ValidationErrors = errors;
    }
}

// 409 Conflict (business rule)
public class BusinessRuleException : AppException
{
    public BusinessRuleException(string message) 
        : base(message, "BUSINESS_RULE_VIOLATION", HttpStatusCode.Conflict) { }
}

// Usage
if (tarea == null)
    throw new TareaNotFoundException(id);

if (tarea.Estado == TareaStatus.Completada)
    throw new BusinessRuleException("Cannot modify a completed task");
```
````

**Tipos de excepciones:**

| Exception | Status Code | Uso |
|-----------|-------------|-----|
| NotFoundException | 404 | Recurso no encontrado |
| ValidationException | 400 | Datos inválidos |
| BusinessRuleException | 409 | Regla de negocio violada |
