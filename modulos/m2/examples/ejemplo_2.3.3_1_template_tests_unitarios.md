# Template: Tests Unitarios para Features

**Contexto de uso:** Template base para generar tests unitarios de cualquier feature siguiendo convenciones SDD.

**Prompt para generar template de test:**

````
@test-dev Genera template para tests unitarios de features:

## Template Base
```csharp
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace PortalEmpleo.Tests.Unit.Features.[FeatureName];

public class [FeatureName]ServiceTests
{
    private readonly [FeatureName]Service _service;
    private readonly Mock<I[FeatureName]Repository> _mockRepository;
    private readonly Mock<I[FeatureName]Factory> _mockFactory;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<[FeatureName]Service>> _mockLogger;
    private readonly PortalEmpleoDbContext _context;

    public [FeatureName]ServiceTests()
    {
        _mockRepository = new Mock<I[FeatureName]Repository>();
        _mockFactory = new Mock<I[FeatureName]Factory>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<[FeatureName]Service>>();

        var options = new DbContextOptionsBuilder<PortalEmpleoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new PortalEmpleoDbContext(options);

        _service = new [FeatureName]Service(
            _mockRepository.Object,
            _mockFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _context);
    }

    #region Happy Path Tests

    [Fact]
    public async Task [MethodName]_ValidInput_ReturnsExpectedResult()
    {
        // Arrange
        var input = new [InputType] { /* valid data */ };
        var expected = new [OutputType] { /* expected result */ };

        _mockFactory.Setup(f => f.Create(It.IsAny<[InputType]>()))
            .Returns(new [EntityType]());
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<[EntityType]>(), It.IsAny<CancellationToken>()))
            .Returns(([EntityType] e, CancellationToken _) => Task.FromResult(e));
        _mockMapper.Setup(m => m.Map<[OutputType]>(It.IsAny<[EntityType]>()))
            .Returns(expected);

        // Act
        var result = await _service.[MethodName](input, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expected);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<[EntityType]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region Validation Tests

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("AB")] // Too short
    public async Task [MethodName]_InvalidInput_ThrowsValidationException(string invalidInput)
    {
        // Arrange
        var input = new [InputType] { [PropertyName] = invalidInput };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => 
            _service.[MethodName](input, CancellationToken.None));
    }

    #endregion

    #region Business Rule Tests

    [Fact]
    public async Task [MethodName]_DuplicateEntity_ThrowsBusinessException()
    {
        // Arrange
        _mockRepository.Setup(r => r.ExistsAsync(It.IsAny<[SearchCriteria]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessRuleException>(() => 
            _service.[MethodName](input, CancellationToken.None));
        
        exception.Message.Should().Contain("duplicate");
    }

    #endregion
}
```
````

**Secciones del template:**

| Sección | Propósito |
|---------|-----------|
| Happy Path | Tests de éxito |
| Validation | Tests de validación |
| Business Rules | Reglas de negocio |
| Edge Cases | Casos límite |
