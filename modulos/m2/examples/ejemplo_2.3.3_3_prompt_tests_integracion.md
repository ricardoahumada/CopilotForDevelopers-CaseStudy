# Prompt: Tests de Integración con WebApplicationFactory

**Contexto de uso:** Prompt para generar tests de integración para controllers ASP.NET Core.

**Prompt para tests de integración:**

````
@test-dev Genera tests de integración para TareasController:

## Especificación
- specs/001-task-api/spec.md#integration-tests
- Endpoint: POST /api/tareas

## Test Setup
```csharp
public class TareasControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public TareasControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PortalEmpleoDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<PortalEmpleoDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PortalEmpleoDbContext>();
                db.Database.EnsureCreated();
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Post_Tarea_Returns201Created()
    {
        // Arrange
        var dto = new CreateTareaDto
        {
            Titulo = "Integration Test Task",
            Prioridad = "alta"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/tareas", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var tarea = await response.Content.ReadFromJsonAsync<TareaResponseDto>();
        tarea.Should().NotBeNull();
        tarea!.Titulo.Should().Be("Integration Test Task");
        tarea.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Get_Tareas_Returns200OkWithList()
    {
        // Arrange
        await CreateTareaAsync("Task 1");
        await CreateTareaAsync("Task 2");

        // Act
        var response = await _client.GetAsync("/api/tareas");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var tareas = await response.Content.ReadFromJsonAsync<List<TareaResponseDto>>();
        tareas.Should().HaveCount(2);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    public async Task Post_InvalidTarea_Returns400BadRequest(string titulo)
    {
        // Arrange
        var dto = new CreateTareaDto { Titulo = titulo };

        // Act
        var response = await _client.PostAsJsonAsync("/api/tareas", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private async Task CreateTareaAsync(string titulo)
    {
        var dto = new CreateTareaDto { Titulo = titulo };
        await _client.PostAsJsonAsync("/api/tareas", dto);
    }
}
```
````

**Tests incluidos:**

| Test | Status Code | Propósito |
|------|-------------|-----------|
| Post_Tarea_Returns201Created | 201 | Happy path |
| Get_Tareas_Returns200OkWithList | 200 | Lista |
| Post_InvalidTarea_Returns400 | 400 | Validación |
