# Prompt: Generar Tests Unitarios desde Specs

**Contexto de uso:** Prompt para generar tests unitarios completos desde especificaciones formales.

**Prompt para generar tests desde specs:**

````
@test-dev Genera tests unitarios para CreateTareaCommandHandler:

## Especificación de Referencia
- specs/001-task-api/spec.md#create-tarea
- Criterios de aceptación: CA-TASK-001 a CA-TASK-005

## Command a Testear
```csharp
public record CreateTareaCommand : IRequest<TareaResponseDto>
{
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public DateTime? FechaLimite { get; init; }
    public string Prioridad { get; init; } = "media";
}

public class CreateTareaCommandHandler : IRequestHandler<CreateTareaCommand, TareaResponseDto>
{
    private readonly ITareaRepository _repository;
    private readonly ITareaFactory _factory;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTareaCommandHandler> _logger;

    public async Task<TareaResponseDto> Handle(CreateTareaCommand command, CancellationToken ct)
    {
        // Implementation
    }
}
```

## Casos de Prueba Requeridos

### Happy Path
1. Task created successfully with valid input
2. Task has correct default values (estado=pendiente, prioridad=media)
3. CreatedAt is current UTC time

### Validation Tests
4. Empty title throws ValidationException
5. Title too long throws ValidationException
6. Invalid priority throws ValidationException
7. Future fechaLimite is accepted
8. Past fechaLimite is rejected

### Business Rules
9. Duplicate title for same user throws BusinessRuleException
10. User without permissions throws UnauthorizedException

### Edge Cases
11. Null description is handled correctly
12. Long description (2000 chars) is accepted

## Convenciones de Test
- Naming: Method_Input_ExpectedBehavior
- AAA Pattern: Arrange, Act, Assert
- FluentAssertions para assertions
- Moq para mocking
````

**Dónde guardar:** `.specify/templates/test-prompts.md`
