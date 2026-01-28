---
name: unit-testing
description: Generates unit tests using xUnit, Moq, and FluentAssertions for PortalEmpleo services. Use when creating tests for business logic, service classes, or any application layer code requiring validation.
license: MIT
metadata:
  author: course-team@netmind.es
  version: "1.0.0"
compatibility: Requires .NET 8 SDK, xUnit, Moq, and FluentAssertions NuGet packages
allowed-tools: Read Write Bash(git:*) Bash(dotnet:*)
---

# Unit Testing Skill - PortalEmpleo

## When to use this skill

Use this skill when creating unit tests for services and business logic in PortalEmpleo. This skill is automatically triggered when the user mentions:
- Unit tests
- xUnit
- Test cases
- Mocking with Moq
- FluentAssertions

## Project Context

- **Test Framework:** xUnit
- **Mocking:** Moq
- **Assertions:** FluentAssertions
- **Target:** Service classes in Application layer
- **Coverage Target:** >= 80%

## Test Class Structure

```csharp
using PortalEmpleo.Application.Services;
using PortalEmpleo.Application.Services.DTOs;
using FluentAssertions;
using Moq;
using Xunit;

namespace PortalEmpleo.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtServiceMock = new Mock<IJwtService>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _authService = new AuthService(
            _userRepositoryMock.Object,
            _jwtServiceMock.Object,
            _passwordServiceMock.Object);
    }
}
```

## Test Method Pattern (AAA)

```csharp
[Fact]
public async Task RegisterAsync_WithValidData_ReturnsAuthResult()
{
    // Arrange
    var registerDto = new RegisterDto
    {
        Email = "test@example.com",
        Password = "SecurePassword123!",
        Name = "Test User",
        BirthDate = new DateTime(1990, 1, 1)
    };

    var user = new User
    {
        Id = Guid.NewGuid(),
        Email = registerDto.Email,
        Name = registerDto.Name
    };

    _userRepositoryMock
        .Setup(r => r.GetByEmailAsync(registerDto.Email))
        .ReturnsAsync((User?)null);

    _userRepositoryMock
        .Setup(r => r.AddAsync(It.IsAny<User>()))
        .Callback<User>(u => u.Id = user.Id);

    _jwtServiceMock
        .Setup(j => j.GenerateAccessToken(It.IsAny<User>()))
        .Returns("access-token");

    _jwtServiceMock
        .Setup(j => j.GenerateRefreshToken(It.IsAny<User>()))
        .Returns("refresh-token");

    // Act
    var result = await _authService.RegisterAsync(registerDto);

    // Assert
    result.Should().NotBeNull();
    result.AccessToken.Should().Be("access-token");
    result.RefreshToken.Should().Be("refresh-token");
    _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
}
```

## Common Test Scenarios

### Success Cases

```csharp
[Fact]
public async Task LoginAsync_WithCorrectCredentials_ReturnsTokens()
{
    // Arrange
    var loginDto = new LoginDto
    {
        Email = "user@example.com",
        Password = "CorrectPassword123!"
    };

    var user = new User
    {
        Id = Guid.NewGuid(),
        Email = loginDto.Email,
        PasswordHash = "$2a$12$..."
    };

    _userRepositoryMock
        .Setup(r => r.GetByEmailAsync(loginDto.Email))
        .ReturnsAsync(user);

    _passwordServiceMock
        .Setup(p => p.VerifyPassword(loginDto.Password, user.PasswordHash))
        .Returns(true);

    _jwtServiceMock
        .Setup(j => j.GenerateAccessToken(user))
        .Returns("valid-token");

    // Act
    var result = await _authService.LoginAsync(loginDto);

    // Assert
    result.Should().NotBeNull();
    result.AccessToken.Should().Be("valid-token");
}
```

### Failure Cases

```csharp
[Fact]
public async Task LoginAsync_WithInvalidEmail_ReturnsNull()
{
    // Arrange
    var loginDto = new LoginDto
    {
        Email = "nonexistent@example.com",
        Password = "anypassword"
    };

    _userRepositoryMock
        .Setup(r => r.GetByEmailAsync(loginDto.Email))
        .ReturnsAsync((User?)null);

    // Act
    var result = await _authService.LoginAsync(loginDto);

    // Assert
    result.Should().BeNull();
    _jwtServiceMock.Verify(j => j.GenerateAccessToken(It.IsAny<User>()), Times.Never);
}

[Fact]
public async Task RegisterAsync_WithDuplicateEmail_ThrowsException()
{
    // Arrange
    var registerDto = new RegisterDto
    {
        Email = "existing@example.com",
        Password = "SecurePassword123!"
    };

    var existingUser = new User { Email = registerDto.Email };

    _userRepositoryMock
        .Setup(r => r.GetByEmailAsync(registerDto.Email))
        .ReturnsAsync(existingUser);

    // Act & Assert
    var action = () => _authService.RegisterAsync(registerDto);
    await action.Should().ThrowAsync<InvalidOperationException>();
}
```

## Required Elements

### 1. Test Class
- Public class with Tests suffix
- Constructor with mock initialization
- IDisposable for cleanup (if needed)

### 2. Test Methods
- [Fact] or [Theory] attribute
- Descriptive name: MethodName_Scenario_ExpectedResult
- Arrange-Act-Assert structure
- Async support with Task return type

### 3. Mocking
- Mock<T> for dependencies
- Setup for return values
- Verify interactions with Times.Once/Times.Never

### 4. Assertions (FluentAssertions)
- Should().NotBeNull()
- Should().Be(expectedValue)
- Should().BeEquivalentTo()
- Should().ThrowAsync<T>()

## Output Format

Generate:
- Complete test class file
- Multiple test methods per service
- Mock setup for dependencies
- FluentAssertions for assertions

## Files to Write

- `tests/PortalEmpleo.Tests/[Service]Tests.cs`

## Coverage Target

- Minimum 80% code coverage
- Test all public methods
- Include edge cases and error scenarios
