# Prompt para Generación de Documentación API

**Contexto de uso:** Este prompt genera documentación API desde código C# con comentarios XML.

**Prompt completo:**
```
@docs-dev Genera documentación API desde código C#:

```csharp
/// <summary>
/// API Controller para gestión de tareas del sistema PortalEmpleo.
/// </summary>
/// <remarks>
/// Provides CRUD operations for task management including:
/// - Creating new tasks with validation
/// - Listing tasks with filtering and pagination
/// - Updating and completing tasks
/// </remarks>
[ApiController]
[Route("api/v1/tareas")]
[Produces("application/json")]
public class TareasController : ControllerBase
{
    /// <summary>
    /// Obtiene lista paginada de tareas
    /// </summary>
    /// <param name="query">Parámetros de filtrado y paginación</param>
    /// <returns>Lista paginada de tareas</returns>
    /// <response code="200">Lista retornada exitosamente</response>
    /// <response code="400">Parámetros inválidos</response>
    /// <example>
    /// GET /api/v1/tareas?estado=pendiente&prioridad=alta&page=1&pageSize=10
    /// </example>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TareaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetTareasQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }
    
    /// <summary>
    /// Crea una nueva tarea
    /// </summary>
    /// <param name="command">Datos de la tarea</param>
    /// <returns>Tarea creada con ID asignado</returns>
    /// <response code="201">Tarea creada exitosamente</response>
    /// <response code="400">Datos inválidos</response>
    [HttpPost]
    [ProducesResponseType(typeof(TareaResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateTareaCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return result.Match(CreatedAtAction, Problem);
    }
}
```

```yaml
name: Generate API Documentation
on:
  push:
    paths: ['src/API/**/*.cs', 'src/Application/DTOs/**/*.cs']
jobs:
  generate-docs:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Generate OpenAPI Spec
        run: dotnet run --project tools/OpenApiGenerator/ --output docs/openapi.json
      - name: Generate Markdown Docs
        run: dotnet run --project tools/MarkdownGenerator/ --input docs/openapi.json --output docs/api/
```
```
