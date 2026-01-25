# Prompt: Implementar Repository Pattern con Entity Framework

**Contexto de uso:** Prompt para implementar el patrón Repository con Entity Framework Core.

**Prompt para implementar Repository Pattern:**

````
Implementa el repositorio de tareas con Entity Framework Core:

## Especificación
- specs/001-task-api/spec.md#repositorio-tareas
- specs/001-task-api/plan.md#data-access-layer

## Interfaz Requerida
```csharp
public interface ITareaRepository
{
    Task<Tarea?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Tarea>> GetAllAsync(CancellationToken ct);
    Task<List<Tarea>> GetByStatusAsync(TareaStatus status, CancellationToken ct);
    Task<List<Tarea>> GetByAssigneeAsync(Guid usuarioId, CancellationToken ct);
    Task<Tarea> AddAsync(Tarea tarea, CancellationToken ct);
    Task<Tarea> UpdateAsync(Tarea tarea, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}
```

## Entity Tarea
```csharp
public class Tarea : BaseEntity
{
    public string Titulo { get; private set; }
    public string? Descripcion { get; private set; }
    public TareaStatus Estado { get; private set; }
    public TareaPrioridad Prioridad { get; private set; }
    public DateTime? FechaLimite { get; private set; }
    public Guid CreadoPorId { get; private set; }
    public DateTime CreadoEn { get; private set; }
    public DateTime? ActualizadoEn { get; private set; }
}
```

## Requisitos de Implementación
1. Usar AsNoTracking para queries de solo lectura
2. Incluir navegación de UsuarioAsignado en GetById
3. Soft delete pattern (campo IsDeleted)
4. Concurrency token para updates
5. Tracking explícito para creates/updates

## Convenciones
- Métodos públicos virtuales
- Documentación XML
- CancellationToken siempre presente
- Throw EntityNotFoundException cuando no existe

## Formato de Salida
```csharp
public class TareaRepository : ITareaRepository
{
    private readonly PortalEmpleoDbContext _context;

    public TareaRepository(PortalEmpleoDbContext context)
    {
        _context = context;
    }

    public async Task<Tarea?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Tareas
            .Include(t => t.UsuarioAsignado)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted, ct);
    }

    public async Task<Tarea> AddAsync(Tarea tarea, CancellationToken ct)
    {
        tarea.CreadoEn = DateTime.UtcNow;
        await _context.Tareas.AddAsync(tarea, ct);
        return tarea;
    }

    public async Task<Tarea> UpdateAsync(Tarea tarea, CancellationToken ct)
    {
        tarea.ActualizadoEn = DateTime.UtcNow;
        _context.Tareas.Update(tarea);
        return tarea;
    }
}
```
````
