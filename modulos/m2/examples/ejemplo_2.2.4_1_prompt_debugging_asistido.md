# Prompt: Debugging Asistido por Copilot

**Contexto de uso:** Prompt para diagnosticar y corregir errores de código con contexto completo.

**Prompt para debugging asistido:**

````
@agent Diagnostica y corrige el error en el siguiente código:

## Código con Error
```csharp
public async Task<List<Tarea>> GetTareasPendientes(Guid usuarioId)
{
    return await _context.Tareas
        .Where(t => t.Estado == TareaStatus.Pendiente)
        .Where(t => t.AsignadoAId == usuarioId)
        .ToListAsync();
}
```

## Error Reported
System.InvalidOperationException: A second operation started on this context before a previous operation completed.

## Stack Trace
at TareaService.GetTareasPendientes in line 45
at TasksController.GetPendientes in line 78

## Contexto
- Método llamado desde API controller
- Controller no usa await
- EF Core con InMemory database en tests

## Especificación Relevante
- specs/001-task-api/spec.md#get-pendientes
- CA-TASK-015: Tiempo respuesta < 100ms

## Diagnóstico y Solución
```
## Diagnóstico

### Problema Identificado
El contexto de EF Core está siendo usado concurrentemente sin await.

### Causa Raíz
En el controller:
```csharp
var tareas = _service.GetTareasPendientes(userId); // Falta await
```

### Solución

1. Controller (corrigido):
```csharp
[HttpGet("pendientes")]
public async Task<IActionResult> GetPendientes()
{
    var tareas = await _service.GetTareasPendientes(userId);
    return Ok(tareas);
}
```

2. Alternativa: ConfigureAwait(false) en repository
```csharp
public async Task<List<Tarea>> GetTareasPendientes(Guid usuarioId)
{
    return await _context.Tareas
        .Where(t => t.Estado == TareaStatus.Pendiente)
        .Where(t => t.AsignadoAId == usuarioId)
        .ToListAsync().ConfigureAwait(false);
}
```

### Recomendación SDD
- Usar await en todos los métodos async
- ConfigureAwait(false) en library code
- Verificar que controller no retorna Task sin await
```
````

**Dónde guardar:** `.specify/templates/debugging-prompts.md`
