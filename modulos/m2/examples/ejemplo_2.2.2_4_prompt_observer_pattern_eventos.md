# Prompt: Implementar Observer Pattern - Eventos de Dominio

**Contexto de uso:** Prompt para implementar sistema de eventos de dominio con MediatR.

**Prompt para implementar Observer Pattern:**

````
Implementa el sistema de eventos de dominio para PortalEmpleo:

## Especificación
- specs/001-task-api/spec.md#eventos-dominio
- specs/001-task-api/plan.md#domain-events

## Eventos de Dominio Requeridos
```csharp
public class TareaCreatedEvent : DomainEvent
{
    public Guid TareaId { get; }
    public Guid CreadoPorId { get; }
    public DateTime CreadoEn { get; }
}

public class TareaCompletedEvent : DomainEvent
{
    public Guid TareaId { get; }
    public Guid CompletadoPorId { get; }
    public DateTime CompletadoEn { get; }
}

public class TareaDeletedEvent : DomainEvent
{
    public Guid TareaId { get; }
    public Guid EliminadoPorId { get; }
}
```

## Base DomainEvent
```csharp
public abstract class DomainEvent
{
    public Guid EventId { get; private set; }
    public DateTime OccurredAt { get; private set; }
    public string? CorrelationId { get; set; }
}
```

## MediatR Integration
```csharp
// Domain event → MediatR notification
public class TareaCreatedEvent : INotification
{
    public Guid TareaId { get; }
    public Guid CreadoPorId { get; }
    public DateTime CreadoEn { get; }
}
```

## Handlers Requeridos
1. TareaCreatedEventHandler → Enviar email de confirmación
2. TareaCompletedEventHandler → Actualizar métricas
3. TareaDeletedEventHandler → Archivar en histórico

## Formato de Salida
```csharp
// Entity with Event Publishing
public class Tarea : BaseEntity
{
    private readonly List<INotification> _domainEvents = new();
    
    public IReadOnlyList<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void Create(Guid creadoPorId)
    {
        // ... creation logic
        
        var @event = new TareaCreatedEvent(Id, creadoPorId);
        _domainEvents.Add(@event);
    }

    public void Complete(Guid completadoPorId)
    {
        Estado = TareaStatus.Completada;
        
        var @event = new TareaCompletedEvent(Id, completadoPorId);
        _domainEvents.Add(@event);
    }
}

// Handler
public class TareaCreatedEventHandler : INotificationHandler<TareaCreatedEvent>
{
    private readonly IEmailService _emailService;
    
    public async Task Handle(TareaCreatedEvent notification, CancellationToken ct)
    {
        await _emailService.SendTareaCreadaEmailAsync(notification);
    }
}
```
````
