# Prompt: API Testing con HttpClient

**Contexto de uso:** Prompt para generar tests de API completos con autenticación y diferentes escenarios.

**Prompt para tests de API:**

````
@test-dev Genera tests de API para PortalEmpleo API:

## Herramienta: HttpClient + FluentAssertions

## Tests Requeridos
1. Happy path: CRUD completo de tarea
2. Autenticación: 401 sin token
3. Autorización: 403 sin permisos
4. Validación: 400 con datos inválidos
5. No encontrado: 404 para ID inexistente

## Formato de Test
```csharp
public class TareasApiTests
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly Guid _testUserId;

    public TareasApiTests()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
        _testUserId = Guid.NewGuid();
    }

    [Fact]
    public async Task CreateTarea_AuthenticatedUser_Returns201()
    {
        // Arrange
        var dto = new CreateTareaDto
        {
            Titulo = "API Test Task",
            Prioridad = "media"
        };

        _client.SetFakeBearerToken(_testUserId);

        // Act
        var response = await _client.PostAsJsonAsync("/api/tareas", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var result = await response.Content.ReadFromJsonAsync<TareaResponseDto>();
        result!.Titulo.Should().Be("API Test Task");
        result.Estado.Should().Be(TareaStatus.Pendiente);
    }

    [Fact]
    public async Task GetTareas_NoAuthHeader_Returns401()
    {
        // Act
        var response = await _client.GetAsync("/api/tareas");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetTareaById_NotFound_Returns404()
    {
        // Arrange
        _client.SetFakeBearerToken(_testUserId);
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/tareas/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("")]
    [InlineData("AB")]
    public async Task CreateTarea_InvalidTitle_Returns400(string invalidTitle)
    {
        // Arrange
        var dto = new CreateTareaDto { Titulo = invalidTitle };
        _client.SetFakeBearerToken(_testUserId);

        // Act
        var response = await _client.PostAsJsonAsync("/api/tareas", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
```
````

**Escenarios cubiertos:**

| Escenario | Status | Descripción |
|-----------|--------|-------------|
| Crear exitoso | 201 | Con auth válido |
| Sin auth | 401 | Sin Bearer token |
| No encontrado | 404 | ID inexistente |
| Validación | 400 | Datos inválidos |
