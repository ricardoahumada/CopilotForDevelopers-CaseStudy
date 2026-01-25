# Prompt para Clean Architecture

**Contexto de uso:** Este prompt implementa estructura Clean Architecture para PortalEmpleo en C#.

**Prompt completo:**
```
Implementa estructura Clean Architecture para PortalEmpleo:

## Capas
```
src/
├── Domain/           # Enterprise business rules
├── Application/      # Application business rules
├── Infrastructure/   # Frameworks and drivers
└── API/              # Interface adapters
```

## Principios
- Inner circles no conocen outer circles
- Dependencies apuntan hacia adentro
- Outer circles pueden implementar interfaces de inner circles

## Formato de Salida
```csharp
// Domain: Entities (Business Logic)
namespace PortalEmpleo.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
}

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();
    
    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other) return false;
        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }
}

// Application: Use Cases
namespace PortalEmpleo.Application.Features.Tareas.Commands.CreateTarea;

public record CreateTareaCommand : IRequest<TareaResponseDto>
{
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public DateTime? FechaLimite { get; init; }
    public string Prioridad { get; init; } = "media";
}

public class CreateTareaCommandHandler 
    : IRequestHandler<CreateTareaCommand, Result<TareaResponseDto>>
{
    private readonly ITareaFactory _factory;
    private readonly ITareaRepository _repository;
    private readonly IMapper _mapper;

    public async Task<Result<TareaResponseDto>> Handle(
        CreateTareaCommand request, 
        CancellationToken ct)
    {
        var tarea = _factory.CreateFromDto(request, GetCurrentUserId());
        await _repository.AddAsync(tarea, ct);
        return _mapper.Map<TareaResponseDto>(tarea);
    }
}

// Infrastructure: Repositories
namespace PortalEmpleo.Infrastructure.Data.Repositories;

public class TareaRepository : ITareaRepository
{
    private readonly PortalEmpleoDbContext _context;

    public async Task<Tarea> AddAsync(Tarea tarea, CancellationToken ct)
    {
        await _context.Tareas.AddAsync(tarea, ct);
        return tarea;
    }
}

// API: Controllers
namespace PortalEmpleo.API.Controllers;

[ApiController]
[Route("api/v1/tareas")]
public class TareasController : BaseController
{
    private readonly IMediator _mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateTareaCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match(Ok, Problem);
    }
}
```
```
