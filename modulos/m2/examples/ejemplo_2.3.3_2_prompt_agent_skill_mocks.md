# Prompt: Crear Agent Skill para Mocks

**Contexto de uso:** Prompt para crear una Agent Skill especializada en generación de mocks para tests.

**Prompt para configurar Agent Skill de testing:**

````
@agent Crea Agent Skill para generación de mocks en tests:

## Ubicación
.github/skills/testing-mocks/

## SKILL.md
```markdown
---
name: testing-mocks
description: Generate mocks and stubs for unit tests. Use when writing or updating unit tests for C# ASP.NET Core projects.
---

# Testing Mocks Skill

## When to use

Use this skill when you need to:
- Create mock objects for dependencies in unit tests
- Set up mock behavior for repository, service, or factory patterns
- Verify mock interactions
- Create in-memory databases for integration tests

## Mocking Patterns

### Repository Mock
```csharp
var mockRepo = new Mock<ITareaRepository>();
mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
    .ReturnsAsync((Guid id, CancellationToken _) => 
        new Tarea { Id = id, Titulo = "Test" });

mockRepo.Setup(r => r.AddAsync(It.IsAny<Tarea>(), It.IsAny<CancellationToken>()))
    .Returns((Tarea t, CancellationToken _) => Task.FromResult(t));
```

### Service Mock
```csharp
var mockService = new Mock<ITareaService>();
mockService.Setup(s => s.CreateTareaAsync(
        It.IsAny<CreateTareaDto>(),
        It.IsAny<Guid>(),
        It.IsAny<CancellationToken>()))
    .ReturnsAsync((CreateTareaDto d, Guid u, CancellationToken _) =>
        new TareaResponseDto { Id = Guid.NewGuid(), Titulo = d.Titulo });
```

### Logger Mock
```csharp
var mockLogger = new Mock<ILogger<TareaService>>();
mockLogger.Setup(
    l => l.Log(
        LogLevel.Information,
        It.IsAny<EventId>(),
        It.IsAny<It.IsAnyType>(),
        It.IsAny<Exception>(),
        It.IsAny<Func<It.IsAnyType, Exception?, string>>()));
```

### In-Memory Database
```csharp
var options = new DbContextOptionsBuilder<PortalEmpleoDbContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
    .Options;
var context = new PortalEmpleoDbContext(options);
```
````

**Patrones de mock incluidos:**

| Patrón | Uso |
|--------|-----|
| Repository Mock | Mockear acceso a datos |
| Service Mock | Mockear servicios |
| Logger Mock | Mockear logging |
| In-Memory DB | Tests de integración |
