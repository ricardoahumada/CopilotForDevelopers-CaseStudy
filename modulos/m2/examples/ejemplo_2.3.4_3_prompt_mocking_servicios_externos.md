# Prompt: Mocking de Servicios Externos

**Contexto de uso:** Prompt para crear mocks de servicios externos como email, notificaciones y APIs de terceros.

**Prompt para mocking de servicios externos:**

````
@test-dev Genera mocks para servicios externos:

## Servicios a Mockear
1. IEmailService - Envío de emails
2. INotificationService - Notificaciones push
3. IExternalApiService - APIs de terceros

## Mock de EmailService
```csharp
public class MockEmailService : IEmailService
{
    public List<EmailMessage> SentEmails { get; } = new();
    public int SendCallCount => SentEmails.Count;

    public Task SendEmailAsync(EmailMessage message, CancellationToken ct)
    {
        SentEmails.Add(message);
        return Task.CompletedTask;
    }

    public Task SendTareaCreadaEmailAsync(TareaCreatedEvent evt, CancellationToken ct)
    {
        SentEmails.Add(new EmailMessage
        {
            To = "user@test.com",
            Subject = $"Nueva tarea: {evt.TareaId}",
            Body = $"Se ha creado la tarea"
        });
        return Task.CompletedTask;
    }
}

// Usage en test
var mockEmailService = new MockEmailService();
var service = new TareaService(
    _repository, _factory, _mapper, _logger, mockEmailService);

await service.CreateTareaAsync(dto, userId, ct);

// Assert
mockEmailService.SendCallCount.Should().Be(1);
mockEmailService.SentEmails[0].Subject.Should().Contain("Nueva tarea");
```

## Mock de HttpClient para APIs externas
```csharp
public class MockHttpMessageHandler : HttpMessageHandler
{
    public HttpRequestMessage? LastRequest { get; private set; }
    private readonly HttpResponseMessage _response;

    public MockHttpMessageHandler(HttpResponseMessage response)
    {
        _response = response;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastRequest = request;
        return Task.FromResult(_response);
    }
}

var handler = new MockHttpMessageHandler(new HttpResponseMessage
{
    StatusCode = HttpStatusCode.OK,
    Content = new StringContent("{\"status\":\"success\"}")
});

var httpClient = new HttpClient(handler);
var service = new ExternalPaymentService(httpClient);
```
````

**Mocks disponibles:**

| Mock | Uso | Verificaciones |
|------|-----|----------------|
| MockEmailService | Simular envío email | SentEmails, SendCallCount |
| MockHttpMessageHandler | APIs externas | LastRequest, Response |
