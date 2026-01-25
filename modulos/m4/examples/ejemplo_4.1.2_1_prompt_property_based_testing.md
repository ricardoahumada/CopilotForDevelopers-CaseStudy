# Prompt para Property-Based Testing

**Contexto de uso:** Este prompt genera property-based tests para el sistema de autenticación usando FsCheck para C#.

**Prompt completo:**
```
@test-agent Genera property-based tests para el sistema de autenticación:

## Framework: FsCheck para C# o Hypothesis para Python

## Propiedades a Validar
1. Login con credenciales válidas siempre retorna token JWT válido
2. Password hasheado nunca puede revertirse al texto plano
3. Tokens generados tienen expiración consistente con configuración

## Formato de Salida
```csharp
using FsCheck;
using FluentAssertions;
using Xunit;

public class AuthenticationPropertyTests
{
    [Property]
    public void Login_ValidCredentials_AlwaysReturnsValidJwt(string email, string password)
    {
        // Arrange
        var dto = new LoginRequestDto
        {
            Email = ValidEmail(email),
            Password = ValidPassword(password)
        };
        
        // Act
        var result = _authService.LoginAsync(dto, CancellationToken.None).Result;
        
        // Assert - Property
        result.IsSuccess.Should().BeTrue();
        result.Value.AccessToken.Should().NotBeNullOrEmpty();
        var tokenParts = result.Value.AccessToken.Split('.');
        tokenParts.Length.Should().Be(3);
    }

    [Property]
    public void DifferentPasswords_GenerateDifferentHashes(string password1, string password2)
    {
        var hash1 = _passwordService.HashPassword(password1);
        var hash2 = _passwordService.HashPassword(password2);
        
        if (password1 != password2)
            hash1.Should().NotBe(hash2);
    }
}
```
```
