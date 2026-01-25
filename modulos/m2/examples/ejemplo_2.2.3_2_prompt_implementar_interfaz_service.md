# Prompt: Implementar Interfaz ITareaService

**Contexto de uso:** Prompt para implementar una interfaz de servicio completa.

**Prompt para implementar interfaz:**

````
Implementa la interfaz ITareaService según specs/001-task-api/spec.md:

## Interfaz a Implementar
```csharp
public interface ITareaService
{
    Task<TareaResponseDto> CreateTareaAsync(CreateTareaDto dto, Guid usuarioId, CancellationToken ct);
    Task<TareaResponseDto?> GetTareaByIdAsync(Guid id, Guid usuarioId, CancellationToken ct);
    Task<PagedResult<TareaResponseDto>> GetTareasAsync(TareaFiltersDto filters, Guid usuarioId, int page, int pageSize, CancellationToken ct);
    Task<TareaResponseDto> UpdateTareaAsync(Guid id, UpdateTareaDto dto, Guid usuarioId, CancellationToken ct);
    Task<bool> DeleteTareaAsync(Guid id, Guid usuarioId, CancellationToken ct);
    Task<TareaResponseDto> CompleteTareaAsync(Guid id, Guid usuarioId, CancellationToken ct);
    Task<TareaResponseDto> AssignTareaAsync(Guid id, Guid asignadoAId, Guid usuarioId, CancellationToken ct);
}
```

## Especificación de Contrato
- Cada método valida que el usuario tiene acceso a la tarea
- Excepciones: TareaNotFoundException, UnauthorizedAccessException
- Logging: ILogger<TareaService>
- Transacción: implicit con EF Core ChangeTracker

## Convenciones del Proyecto
- Métodos async con sufijo Async
- CancellationToken último parámetro
- Excepciones específicas del dominio
- Resultado mapeado a DTO

## Formato de Salida
```csharp
public class TareaService : ITareaService
{
    private readonly ITareaRepository _repository;
    private readonly ITareaFactory _factory;
    private readonly IMapper _mapper;
    private readonly ILogger<TareaService> _logger;
    private readonly ICurrentUserService _currentUser;

    public TareaService(
        ITareaRepository repository,
        ITareaFactory factory,
        IMapper mapper,
        ILogger<TareaService> logger,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _factory = factory;
        _mapper = mapper;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<TareaResponseDto> CreateTareaAsync(
        CreateTareaDto dto,
        Guid usuarioId,
        CancellationToken ct)
    {
        _logger.LogInformation("Creating tarea for user {UserId}", usuarioId);
        
        var tarea = _factory.CreateFromDto(dto, usuarioId);
        await _repository.AddAsync(tarea, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        
        _logger.LogInformation("Created tarea {TareaId}", tarea.Id);
        
        return _mapper.Map<TareaResponseDto>(tarea);
    }

    public async Task<TareaResponseDto?> GetTareaByIdAsync(
        Guid id,
        Guid usuarioId,
        CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync(id, ct);
        
        if (tarea == null)
            throw new TareaNotFoundException(id);
        
        if (!HasAccess(tarea, usuarioId))
            throw new UnauthorizedAccessException();
        
        return _mapper.Map<TareaResponseDto>(tarea);
    }

    private bool HasAccess(Tarea tarea, Guid usuarioId)
    {
        return tarea.CreadoPorId == usuarioId || 
               tarea.AsignadoAId == usuarioId;
    }
}
```
````
