# Prompt: Implementar Factory Pattern para Entidades

**Contexto de uso:** Prompt para implementar el patrón Factory para creación de entidades de dominio.

**Prompt para implementar Factory Pattern:**

````
Implementa TareaFactory para creación de entidades Tarea:

## Propósito
Abstraer la lógica de creación de objetos Tarea,
validando reglas de negocio en el constructor.

## Especificación
- specs/001-task-api/spec.md#factory-tarea
- specs/001-task-api/plan.md#domain-logic

## Factory Interface
```csharp
public interface ITareaFactory
{
    Tarea Create(string titulo, Guid creadoPorId, string? descripcion = null);
    Tarea CreateFromDto(CreateTareaDto dto, Guid usuarioId);
}

public class TareaFactory : ITareaFactory
{
    public Tarea Create(string titulo, Guid creadoPorId, string? descripcion = null)
    {
        // Implementación
    }

    public Tarea CreateFromDto(CreateTareaDto dto, Guid usuarioId)
    {
        // Implementación
    }
}
```

## Reglas de Negocio en Creación
1. Titulo requerido, trimming automático
2. Estado inicial: TareaStatus.Pendiente
3. Prioridad: default "media" si no especificada
4. FechaLímite: nullable (sin límite si null)
5. Etiquetas: inicializar lista vacía si null

## Entity Tarea (constraints)
```csharp
public class Tarea : BaseEntity
{
    private Tarea() { } // EF Core constructor

    private Tarea(string titulo, Guid creadoPorId, string? descripcion)
    {
        Id = Guid.NewGuid();
        Titulo = titulo.Trim();
        Descripcion = descripcion?.Trim();
        Estado = TareaStatus.Pendiente;
        Prioridad = TareaPrioridad.Media;
        CreadoPorId = creadoPorId;
        CreadoEn = DateTime.UtcNow;
    }

    public static Tarea Create(string titulo, Guid creadoPorId, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título requerido");

        if (titulo.Length > 200)
            throw new ArgumentException("Título muy largo (max 200)");

        return new Tarea(titulo, creadoPorId, descripcion);
    }
}
```
````

**Beneficios del patrón:**

| Beneficio | Descripción |
|-----------|-------------|
| Encapsulación | Validaciones en un lugar |
| Testabilidad | Factory fácil de mockear |
| Consistencia | Todas las instancias válidas |
