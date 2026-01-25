# Prompt para Simplificación de Lógica

**Contexto de uso:** Este prompt simplifica la lógica de validación usando Specification Pattern.

**Prompt completo:**
```
@agent Simplifica la lógica de validación con Specification Pattern:

## Lógica Original (compleja y anidada)
## Refactoring con Specification Pattern

```csharp
// Specifications individuales
public class TituloValidoSpecification : ISpecification<Tarea>
{
    public bool IsSatisfiedBy(Tarea tarea)
    {
        return !string.IsNullOrWhiteSpace(tarea.Titulo) &&
               tarea.Titulo.Length >= 1 && tarea.Titulo.Length <= 200;
    }
    public string ErrorMessage => "Título debe tener entre 1 y 200 caracteres";
}

public class PrioridadValidaSpecification : ISpecification<Tarea>
{
    public bool IsSatisfiedBy(Tarea tarea)
    {
        return tarea.Prioridad != null && Enum.IsDefined(typeof(TareaPrioridad), tarea.Prioridad);
    }
    public string ErrorMessage => "Prioridad inválida";
}

// Composite Validator
public class TareaValidator : IValidator<Tarea>
{
    private readonly List<ISpecification<Tarea>> _specs = new()
    {
        new TituloValidoSpecification(),
        new PrioridadValidaSpecification(),
        new FechaLimiteValidaSpecification()
    };
    
    public ValidationResult Validate(Tarea entity)
    {
        var errors = _specs.Where(s => !s.IsSatisfiedBy(entity))
            .Select(s => s.ErrorMessage).ToList();
        return new ValidationResult(errors);
    }
}
```
```
