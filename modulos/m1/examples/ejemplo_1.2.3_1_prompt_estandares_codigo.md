# Prompt: Estándares de Código Detallados

**Contexto de uso:** Prompt que especifica estándares de código C# con patrones de ejemplo.

**Prompt con estándares detallados:**

````
Implementa el nuevo endpoint siguiendo estos estándares:

## Estándares de Código C#
- Nomenclatura: PascalCase para clases/métodos, camelCase para variables
- Visibilidad: private explícito para campos
- Async: suffix 'Async' para métodos asíncronos
- Nullable: habilitar annotations warnings
- Documentación: XML comments para APIs públicas

## Patrones Requeridos
```csharp
// Controller pattern
[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly ITareaService _service;
    public TareasController(ITareaService service) => _service = service;
}

// Service pattern
public interface ITareaService { }
public class TareaService : ITareaService { }
```

## Validation
- FluentValidation para DTOs
- ModelState automático en controller
````

**Convenciones de nomenclatura:**

| Elemento | Convención | Ejemplo |
|----------|------------|---------|
| Clases | PascalCase | `TareaService` |
| Métodos | PascalCase | `GetByIdAsync` |
| Variables | camelCase | `tareaId` |
| Campos privados | _camelCase | `_service` |
| Async | Sufijo Async | `CreateAsync` |
