# Prompt para Integration Tests

**Contexto de uso:** Este prompt genera integration tests entre servicios PortalEmpleo.

**Prompt completo:**
```
@test-agent Genera integration tests entre servicios PortalEmpleo:

```csharp
public class TareaIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task FullWorkflow_CreateCompleteAndListTarea()
    {
        // 1. Login
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login",
            new LoginRequestDto { Email = "test@portalempleo.com", Password = "Test123!" });
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var loginData = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginData.AccessToken);
        
        // 2. Create tarea
        var createResponse = await _client.PostAsJsonAsync("/api/v1/tareas",
            new CreateTareaDto { Titulo = "Integration Test Task", Prioridad = "alta" });
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdTarea = await createResponse.Content.ReadFromJsonAsync<TareaResponseDto>();
        createdTarea.Id.Should().NotBe(Guid.Empty);
        
        // 3. Complete tarea
        var completeResponse = await _client.PatchAsync($"/api/v1/tareas/{createdTarea.Id}/complete", null);
        completeResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var completedTarea = await completeResponse.Content.ReadFromJsonAsync<TareaResponseDto>();
        completedTarea.Estado.Should().Be("completada");
        
        // 4. Delete
        var deleteResponse = await _client.DeleteAsync($"/api/v1/tareas/{createdTarea.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
```
```
