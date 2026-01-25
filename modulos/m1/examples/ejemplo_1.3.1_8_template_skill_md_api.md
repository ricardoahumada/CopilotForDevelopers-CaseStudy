# Template: SKILL.md para Desarrollo de APIs

**Contexto de uso:** Contenido completo de una Agent Skill para desarrollo de APIs.

**Contenido de SKILL.md:**

````markdown
---
name: api-development
description: Develop REST APIs following Clean Architecture. Use when creating endpoints, services, or repositories in ASP.NET Core 8 projects.
---

# API Development Skill

## When to use this skill

Use this skill when you need to:
- Create new REST API endpoints
- Implement CRUD operations
- Add validation to DTOs
- Create or modify repositories
- Add error handling middleware

## Procedures

### Creating a New Endpoint

1. **Review specification**: Check `specs/[feature]/spec.md` for requirements
2. **Check existing code**: Look for related entities in `src/Domain/Entities/`
3. **Create DTOs**: Use [DTO template](./templates/dto-template.cs) in `src/Application/DTOs/`
4. **Create Validator**: Use [FluentValidator template](./templates/validator-template.cs)
5. **Create/Update Service**: Implement in `src/Application/Services/`
6. **Create Controller**: Use [Controller template](./templates/controller-template.cs)
7. **Add Tests**: Use [Integration test template](./templates/integration-test-template.cs)

### Error Handling

Always use ProblemDetails format:

```csharp
ValidationProblemDetails problem = new ValidationProblemDetails(
    new Dictionary<string, string[]>())
{
    Title = "Invalid request",
    Status = StatusCodes.Status400BadRequest,
    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
};
```

### Repository Pattern

```csharp
public interface ITareaRepository
{
    Task<Tarea?> GetByIdAsync(Guid id);
    Task AddAsync(Tarea tarea);
    Task UpdateAsync(Tarea tarea);
    Task DeleteAsync(Guid id);
}
```

## Best Practices

- Use async/await consistently
- Include cancellation tokens
- Log operations with ILogger<T>
- Return appropriate status codes
- Validate input with FluentValidation
````

**Patrones de código incluidos:**

| Patrón | Descripción |
|--------|-------------|
| ProblemDetails | Manejo de errores estándar |
| Repository | Acceso a datos |
| Async/Await | Operaciones asíncronas |
