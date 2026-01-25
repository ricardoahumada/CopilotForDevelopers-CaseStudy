# Template: Unit Test con xUnit y FluentAssertions

**Contexto de uso:** Este template define la estructura para tests unitarios usando xUnit y FluentAssertions.

**Template Spec Kit:**

```markdown
# Template: Unit Test con xUnit y FluentAssertions

## Estructura
```csharp
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.Unit.Features.[Feature];

public class [Entity]ServiceTests
{
    private readonly [Entity]Service _service;
    private readonly Mock<I[Entity]Repository> _mockRepository;
    private readonly Mock<ILogger<[Entity]Service>> _mockLogger;

    public [Entity]ServiceTests()
    {
        _mockRepository = new Mock<I[Entity]Repository>();
        _mockLogger = new Mock<ILogger<[Entity]Service>>();
        _service = new [Entity]Service(_mockRepository.Object, _mockLogger.Object);
    }

    #region Happy Path Tests

    [Fact]
    public async Task [MethodName]_ValidInput_ReturnsExpectedResult()
    {
        // Arrange
        var input = new [InputType] { /* valid data */ };
        var expected = new [OutputType] { /* expected result */ };

        _mockRepository.Setup(r => r.[MethodName](It.IsAny<[InputType]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _service.[MethodName](input, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected);
    }

    #endregion

    #region Validation Tests

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task [MethodName]_InvalidInput_ThrowsValidationException([param] string invalidInput)
    {
        // Arrange
        var input = new [InputType] { Property = invalidInput };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => 
            _service.[MethodName](input, CancellationToken.None));
    }

    #endregion
}
```

## Convenciones
- AAA Pattern: Arrange, Act, Assert
- FluentAssertions para assertions
- Moq para mocking
- Naming: Method_Input_ExpectedBehavior
```

**Integración con Spec Kit:**

```bash
# 1. Verificar tasks.md
# TASK-005: Escribir tests para AuthService

# 2. Seleccionar y personalizar template
specs/001-auth-api/test-prompts.md → Template: unit-test-template.md

# 3. Generar tests
@test-dev Aplica template unit-test-template.md
          para AuthService
          según specs/001-auth-api/spec.md

# 4. Validar cobertura
/specKit.analyze --coverage 80 --template unit-test-template.md
```

**Resultado generado:**

```csharp
// tests/PortalEmpleo.Tests.Unit/Features/Auth/AuthServiceTests.cs
public class AuthServiceTests
{
    private readonly AuthService _service;
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<AuthService>> _mockLogger;

    public AuthServiceTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<AuthService>>();
        _service = new AuthService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsAuthResult()
    {
        // Arrange
        var dto = new LoginRequestDto { Email = "test@test.com", Password = "Password123!" };
        var expected = new AuthResponse { AccessToken = "jwt-token" };

        _mockRepository.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User { Email = "test@test.com" });

        // Act
        var result = await _service.LoginAsync(dto, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Login_NullEmail_ThrowsValidationException(string email)
    {
        // Arrange
        var dto = new LoginRequestDto { Email = email };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => 
            _service.LoginAsync(dto, CancellationToken.None));
    }
}
```
