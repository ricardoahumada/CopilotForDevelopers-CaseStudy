# Prompt para Tests de Concurrencia

**Contexto de uso:** Este prompt genera tests de acceso concurrente para verificar race conditions y optimistic concurrency.

**Prompt completo:**
```
@test-agent Genera tests de acceso concurrente:

```csharp
[Fact]
public async Task CreateTarea_SimultaneousRequests_AllSucceed()
{
    var concurrentRequests = 10;
    var tasks = new List<Task<Result<TareaResponseDto>>>();
    
    for (int i = 0; i < concurrentRequests; i++)
    {
        tasks.Add(_handler.Handle(
            new CreateTareaCommand { Titulo = $"Concurrent Task {i}" },
            CancellationToken.None));
    }
    
    var results = await Task.WhenAll(tasks);
    
    results.All(r => r.IsSuccess).Should().BeTrue();
    results.Select(r => r.Value.Id).Distinct().Count().Should().Be(concurrentRequests);
}

[Fact]
public async Task UpdateTarea_OptimisticConcurrency_SecondFails()
{
    var tarea = CreateTestTarea();
    await _repository.AddAsync(tarea, CancellationToken.None);
    var versionOriginal = tarea.Version;
    
    var task1 = _handler.Handle(new UpdateTareaCommand { Id = tarea.Id, Titulo = "User 1", Version = versionOriginal }, CancellationToken.None);
    
    tarea.Titulo = "Modified by User 2";
    tarea.Version = Guid.NewGuid();
    await _context.SaveChangesAsync(CancellationToken.None);
    
    var result1 = await task1;
    result1.IsSuccess.Should().BeTrue();
    
    var result2 = await _handler.Handle(new UpdateTareaCommand { Id = tarea.Id, Titulo = "Will Fail", Version = versionOriginal }, CancellationToken.None);
    result2.IsFailure.Should().BeTrue();
    result2.Error.Should().Contain("concurrency");
}
```
```
