# Prompt: Tests de Integración con Base de Datos Real

**Contexto de uso:** Prompt para generar tests de integración contra SQL Server real con transacciones.

**Prompt para tests de base de datos:**

````
@test-dev Genera tests de integración con base de datos:

## Requisitos
- Tests contra SQL Server real (no in-memory)
- Uso de transactions para isolation
- Cleanup después de cada test

## Formato de Test
```csharp
public class TareaRepositoryIntegrationTests : IDisposable
{
    private readonly PortalEmpleoDbContext _context;
    private readonly TareaRepository _repository;
    private readonly Guid _testUserId;

    public TareaRepositoryIntegrationTests()
    {
        _testUserId = Guid.NewGuid();
        var options = new DbContextOptionsBuilder<PortalEmpleoDbContext>()
            .UseSqlServer("Server=.;Database=PortalEmpleoTest;Integrated Security=true")
            .Options;
        _context = new PortalEmpleoDbContext(options);
        _context.Database.Migrate();
        _repository = new TareaRepository(_context);
        
        // Start transaction
        _context.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _context.Database.RollbackTransaction();
        _context.Dispose();
    }

    [Fact]
    public async Task AddAsync_NewTarea_PersistsToDatabase()
    {
        // Arrange
        var tarea = TareaFactory.Create("Test Task", _testUserId, "Description");

        // Act
        var result = await _repository.AddAsync(tarea, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        // Assert
        var savedTarea = await _context.Tareas
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tarea.Id);
        
        savedTarea.Should().NotBeNull();
        savedTarea!.Titulo.Should().Be("Test Task");
    }

    [Fact]
    public async Task GetByIdAsync_ExistingTarea_ReturnsTarea()
    {
        // Arrange
        var tarea = TareaFactory.Create("Get Test", _testUserId);
        await _repository.AddAsync(tarea, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        // Act
        var result = await _repository.GetByIdAsync(tarea.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Titulo.Should().Be("Get Test");
    }

    [Fact]
    public async Task DeleteAsync_ExistingTarea_MarksAsDeleted()
    {
        // Arrange
        var tarea = TareaFactory.Create("Delete Test", _testUserId);
        await _repository.AddAsync(tarea, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        // Act
        var result = await _repository.DeleteAsync(tarea.Id, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        
        var deletedTarea = await _context.Tareas
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(t => t.Id == tarea.Id);
        
        deletedTarea!.IsDeleted.Should().BeTrue();
    }
}
```
````

**Estrategia de isolation:**

| Estrategia | Beneficio |
|------------|-----------|
| Transaction | Rollback automático |
| BeginTransaction | Aislamiento entre tests |
| Dispose | Cleanup garantizado |
