# Prompt para Performance Testing

**Contexto de uso:** Este prompt genera suite de performance tests para PortalEmpleo API usando NBomber para .NET.

**Prompt completo:**
```
@test-agent Genera suite de performance tests para PortalEmpleo API:

## Requisitos No Funcionales
- Tiempo de respuesta API: < 200ms (p95)
- Throughput: > 1000 requests/segundo

## Herramienta: NBomber para .NET

```csharp
[Fact]
public async Task Login_Under_200ms_p95()
{
    var scenario = Scenario.Create("login_scenario", async context =>
    {
        var stopwatch = Stopwatch.StartNew();
        var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/login",
            new LoginRequestDto { Email = "test@portalempleo.com", Password = "Test123!" });
        stopwatch.Stop();
        return Response.Ok(latencyMs: stopwatch.ElapsedMilliseconds);
    })
    .WithWarmUpDuration(TimeSpan.FromSeconds(30))
    .WithDuration(TimeSpan.FromMinutes(5));

    var result = NBomberRunner.RegisterScenarios(scenario).Run();
    var p95Latency = result.SimulationStats.GetStepStats("login_scenario").Data.Latency.Percentile95;
    p95Latency.Should().BeLessThan(200);
}
```
```
