# Configuración: Autocompletado Contextual en AGENTS.md

**Contexto de uso:** Configuración de AGENTS.md para proporcionar autocompletado inteligente basado en patrones del proyecto.

**Configuración en AGENTS.md para autocompletado contextual:**

````markdown
## Autocompletado Contextual

When providing code completions, follow these project-specific rules:

### C# Patterns
- Always use `var` for variable declarations when type is obvious
- Use target-typed `new()` where available
- Prefix private fields with `_`
- Use expression-bodied members for single-line members

### Example Completion
```csharp
// User types:
public async Task<Tarea?> G

// Copilot suggests:
public async Task<Tarea?> GetByIdAsync(Guid id, CancellationToken ct = default)
{
    return await _context.Tareas
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == id, ct);
}
```

### Validation Pattern
```csharp
// User types:
if (id ==

// Copilot suggests:
if (id == Guid.Empty)
    throw new ArgumentException("ID cannot be empty", nameof(id));
```

### Error Handling Pattern
```csharp
// User types:
catch (Exception ex)

// Copilot suggests:
catch (Exception ex)
{
    _logger.LogError(ex, "Error in {MethodName}", nameof(MethodName));
    throw;
}
```
````

**Patrones de autocompletado:**

| Trigger | Sugerencia |
|---------|------------|
| `public async Task<T?> G` | GetByIdAsync completo |
| `if (id ==` | Validación de Guid.Empty |
| `catch (Exception ex)` | Logging + rethrow |
