# Prompt: Función Compleja - Filtrado Avanzado de Tareas

**Contexto de uso:** Prompt para implementar método de filtrado con paginación y ordenamiento.

**Prompt para función compleja:**

````
Implementa el método de filtrado avanzado de tareas:

## Especificación
- specs/001-task-api/spec.md#get-tareas-filtros
- Criterios: CA-TASK-010, CA-TASK-011

## Firma Requerida
```csharp
public async Task<PagedResult<TareaResponseDto>> GetTareasAsync(
    TareaFiltersDto filters,
    int page = 1,
    int pageSize = 20,
    string sortBy = "CreadoEn",
    string sortOrder = "desc",
    CancellationToken ct = default)
```

## Filtros Soportados
```csharp
public class TareaFiltersDto
{
    public string? Search { get; set; }       // Buscar en título y descripción
    public TareaStatus? Estado { get; set; }
    public TareaPrioridad? Prioridad { get; set; }
    public Guid? AsignadoAId { get; set; }
    public Guid? CreadoPorId { get; set; }
    public DateTime? FechaLimiteDesde { get; set; }
    public DateTime? FechaLimiteHasta { get; set; }
    public List<string>? Etiquetas { get; set; }
}
```

## Reglas de Negocio
1. Usuario solo ve tareas que:
   - Ha creado, o
   - Están asignadas a él, o
   - Son públicas (no implementado aún)
2. Paginación: pageSize max 100
3. Búsqueda: ignore case, partial match
4. Sorting: solo campos válidos de Tarea

## Contexto del Proyecto
- Repository: ITareaRepository con IQueryable
- Mapper: IMapper configurado
- User context: ICurrentUserService.GetCurrentUserId()
- Specifications: specification pattern para filtros

## Formato de Salida
```csharp
public async Task<PagedResult<TareaResponseDto>> GetTareasAsync(
    TareaFiltersDto filters,
    int page = 1,
    int pageSize = 20,
    string sortBy = "CreadoEn",
    string sortOrder = "desc",
    CancellationToken ct = default)
{
    // Validar paginación
    pageSize = Math.Min(pageSize, 100);
    
    // Obtener usuario actual
    var currentUserId = _currentUserService.GetCurrentUserId();
    
    // Query base con filtro de seguridad
    var query = _repository.GetQueryable()
        .Where(t => !t.IsDeleted)
        .Where(t => t.CreadoPorId == currentUserId || 
                   t.AsignadoAId == currentUserId);
    
    // Aplicar filtros
    if (!string.IsNullOrWhiteSpace(filters.Search))
    {
        var searchTerm = filters.Search.ToLower();
        query = query.Where(t => 
            t.Titulo.ToLower().Contains(searchTerm) ||
            t.Descripcion != null && t.Descripcion.ToLower().Contains(searchTerm));
    }
    
    if (filters.Estado.HasValue)
        query = query.Where(t => t.Estado == filters.Estado.Value);
    
    // Sorting
    query = ApplySorting(query, sortBy, sortOrder);
    
    // Pagination
    var total = await query.CountAsync(ct);
    var items = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(ct);
    
    // Map y return
    var dtos = _mapper.Map<List<TareaResponseDto>>(items);
    return new PagedResult<TareaResponseDto>(dtos, total, page, pageSize);
}
```
````
