# Prompt: Configurar Testing Framework SDD

**Contexto de uso:** Prompt para generar configuración completa de testing para el proyecto.

**Prompt para configurar testing:**

````
Genera configuración de testing para PortalEmpleo:

## Frameworks
- Unit Testing: xUnit + Moq + FluentAssertions
- Integration Testing: ASP.NET Core TestHost
- E2E: Playwright (Web) / Detox (Mobile)
- Coverage: ReportGenerator + Coverlet

## Estructura de Tests
```
tests/
├── PortalEmpleo.Tests.Unit/
│   ├── Features/
│   │   └── Tasks/
│   │       ├── Commands/
│   │       │   └── CreateTareaCommandHandlerTests.cs
│   │       └── Queries/
│   │           └── GetTareasQueryHandlerTests.cs
│   ├── Application/
│   └── Domain/
├── PortalEmpleo.Tests.Integration/
│   ├── API/
│   │   └── TasksControllerTests.cs
│   └── Data/
│       └── TareaRepositoryTests.cs
└── PortalEmpleo.Tests.E2E/
    ├── SpecFlow/
    │   └── TaskManagement.feature
    └── Playwright/
        └── task.spec.ts
```

## Configuración xUnit
```csharp
public class CreateTareaCommandHandlerTests
{
    private readonly CreateTareaCommandHandler _handler;
    private readonly Mock<ITareaRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly PortalEmpleoDbContext _context;

    public CreateTareaCommandHandlerTests()
    {
        _mockRepo = new Mock<ITareaRepository>();
        _mockMapper = new Mock<IMapper>();
        _options = new DbContextOptionsBuilder<PortalEmpleoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new PortalEmpleoDbContext(_options);
        _handler = new CreateTareaCommandHandler(_mockRepo.Object, _mockMapper.Object, _context);
    }

    [Fact]
    public async Task Handle_ValidTarea_ReturnsTareaResponse()
    {
        // Arrange
        var command = new CreateTareaCommand { Title = "Test Task" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test Task");
    }
}
```
````

**Frameworks por tipo de test:**

| Tipo | Framework | Herramientas |
|------|-----------|--------------|
| Unit | xUnit | Moq, FluentAssertions |
| Integration | TestHost | InMemory DB |
| E2E | Playwright/Detox | SpecFlow |
| Coverage | Coverlet | ReportGenerator |
