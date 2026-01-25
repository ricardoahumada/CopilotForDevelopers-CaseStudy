# Prompt para Application Performance Monitoring

**Contexto de uso:** Este prompt implementa Application Performance Monitoring con telemetría.

**Prompt completo:**
```
@agent Implementa Application Performance Monitoring:

```csharp
public interface ITelemetryTracker
{
    void TrackRequest(string operationName, TimeSpan duration, bool success);
    void TrackDependency(string dependencyName, string command, TimeSpan duration, bool success);
    void TrackEvent(string eventName, Dictionary<string, string>? properties = null);
    void TrackMetric(string metricName, double value);
}

public class TelemetryTracker : ITelemetryTracker
{
    private readonly TelemetryClient _telemetryClient;
    
    public void TrackRequest(string operationName, TimeSpan duration, bool success)
    {
        _telemetryClient.TrackEvent("RequestCompleted", new Dictionary<string, string>
        {
            ["OperationName"] = operationName,
            ["Success"] = success.ToString()
        });
        _telemetryClient.TrackMetric($"RequestDuration.{operationName}", duration.TotalMilliseconds);
    }
    
    public void TrackDependency(string dependencyName, string command, TimeSpan duration, bool success)
    {
        _telemetryClient.TrackDependency(dependencyName, command, duration, success);
    }
    
    public void TrackEvent(string eventName, Dictionary<string, string>? properties)
    {
        _telemetryClient.TrackEvent(eventName, properties);
    }
    
    public void TrackMetric(string metricName, double value)
    {
        _telemetryClient.TrackMetric(metricName, value);
    }
}

// Middleware de tracking
public class TelemetryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITelemetryTracker _tracker;
    
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var path = context.Request.Path.Value ?? "/";
        
        try { await _next(context); }
        finally
        {
            stopwatch.Stop();
            _tracker.TrackRequest($"{context.Request.Method}_{path}", stopwatch.Elapsed, context.Response.StatusCode < 400);
        }
    }
}

// Métricas de negocio
public class BusinessMetrics
{
    private readonly ITelemetryTracker _tracker;
    
    public void OnTareaCreada(string prioridad)
    {
        _tracker.TrackEvent("TareaCreada", new Dictionary<string, string> { ["Prioridad"] = prioridad });
        _tracker.TrackMetric("tareas_creadas", 1);
    }
}

// Alertas
{
  "alerts": [
    { "name": "HighResponseTime", "metric": "request.duration.p95", "threshold": 200, "operator": ">", "severity": "Warning" },
    { "name": "HighErrorRate", "metric": "request.errors.rate", "threshold": 0.05, "operator": ">", "severity": "Critical" }
  ]
}
```
```
