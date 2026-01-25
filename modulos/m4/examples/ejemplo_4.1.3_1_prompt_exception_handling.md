# Prompt para Tests de Manejo de Errores

**Contexto de uso:** Este prompt genera tests exhaustivos de exception handling para el sistema de autenticación.

**Prompt completo:**
```
@test-agent Genera tests exhaustivos de exception handling:

## Escenarios a Testear
1. Usuario no encontrado → 401
2. Password incorrecto → 401
3. Usuario inactivo → 403
4. Rate limiting excedido → 429
5. Base de datos no disponible → 503

```csharp
[Fact]
public async Task Login_UsuarioNoExistente_Returns401()
{
    var dto = new LoginRequestDto { Email = "nonexistent@portalempleo.com", Password = "any" };
    _mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync((Usuario?)null);
    
    var result = await _handler.Handle(new LoginCommand(dto), CancellationToken.None);
    
    result.IsFailure.Should().BeTrue();
    result.Error.Should().Contain("Credenciales inválidas");
}

[Fact]
public async Task Login_RateLimitExceeded_Returns429()
{
    var attempts = 0;
    _mockRateLimit.Setup(rl => rl.IsRateLimited(It.IsAny<string>()))
        .Returns(() => ++attempts > 5);
    
    for (int i = 0; i < 7; i++)
        await _handler.Handle(new LoginCommand(_validDto), CancellationToken.None);
    
    _mockRateLimit.Verify(rl => rl.IsRateLimited(It.IsAny<string>()), Times.AtLeast(6));
}
```
```
