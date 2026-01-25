# Prompt para Lógica de Negocio

**Contexto de uso:** Este prompt implementa motor de validación de reglas de negocio usando Specification Pattern en C#.

**Prompt completo:**
```
Implementa el motor de validación de reglas de negocio para PortalEmpleo:

## Especificación
- specs/003-business-rules/spec.md
- REQ-RULE-001: Validación encadenada
- REQ-RULE-002: Reglas con dependencias

## Reglas de Negocio
1. Usuario solo puede tener 10 tareas activas simultáneamente
2. Tarea con prioridad 'urgente' debe completarse en 24h
3. Usuario con rol 'junior' requiere aprobación para más de 5 tareas completadas/semana
4. Tarea asignada a otro usuario no puede modificarse
5. Manager puede reasignar tareas de su equipo

## Patrón Specification
```csharp
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    string ErrorMessage { get; }
}

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
    Severity Severity { get; }
}

public enum Severity
{
    Info,
    Warning,
    Error
}
```

## Formato de Salida
```csharp
// Rule Base
public abstract class BusinessRule : IBusinessRule
{
    public abstract bool IsBroken();
    public abstract string Message { get; }
    public abstract Severity Severity { get; }
    
    protected readonly PortalEmpleoDbContext _context;
    
    protected BusinessRule(PortalEmpleoDbContext context)
    {
        _context = context;
    }
}

// Rule 1: Límite de tareas activas
public class MaxActiveTasksRule : BusinessRule
{
    private readonly Guid _usuarioId;
    
    public MaxActiveTasksRule(PortalEmpleoDbContext context, Guid usuarioId) 
        : base(context)
    {
        _usuarioId = usuarioId;
    }
    
    public override bool IsBroken()
    {
        var activeTasks = _context.Tareas
            .Count(t => t.CreadoPorId == _usuarioId && 
                       t.Estado != TareaStatus.Completada &&
                       t.Estado != TareaStatus.Cancelada);
        
        return activeTasks >= 10;
    }
    
    public override string Message => 
        "Usuario ha alcanzado el límite de 10 tareas activas";
    
    public override Severity Severity => Severity.Warning;
}

// Rule 2: Urgente deadline
public class UrgentTaskDeadlineRule : BusinessRule
{
    private readonly Tarea _tarea;
    
    public UrgentTaskDeadlineRule(PortalEmpleoDbContext context, Tarea tarea) 
        : base(context)
    {
        _tarea = tarea;
    }
    
    public override bool IsBroken()
    {
        if (_tarea.Prioridad != TareaPrioridad.Urgente)
            return false;
        
        if (_tarea.FechaLimite == null)
            return true;
        
        var horasRestantes = (_tarea.FechaLimite.Value - DateTime.UtcNow).TotalHours;
        return horasRestantes < 0;
    }
    
    public override string Message => 
        "Tarea urgente debe tener fecha límite y completarse en 24h";
    
    public override Severity Severity => Severity.Error;
}

// Rule Engine
public class BusinessRuleEngine
{
    private readonly PortalEmpleoDbContext _context;
    private readonly List<IBusinessRule> _rules;
    
    public BusinessRuleEngine(PortalEmpleoDbContext context)
    {
        _context = context;
        _rules = new List<IBusinessRule>();
    }
    
    public BusinessRuleEngine AddRule(IBusinessRule rule)
    {
        _rules.Add(rule);
        return this;
    }
    
    public RuleValidationResult Validate()
    {
        var brokenRules = _rules
            .Where(r => r.IsBroken())
            .ToList();
        
        return new RuleValidationResult
        {
            IsValid = !brokenRules.Any(),
            BrokenRules = brokenRules,
            Errors = brokenRules
                .Where(r => r.Severity == Severity.Error)
                .Select(r => r.Message)
                .ToList(),
            Warnings = brokenRules
                .Where(r => r.Severity == Severity.Warning)
                .Select(r => r.Message)
                .ToList()
        };
    }
}

// Usage
var ruleEngine = new BusinessRuleEngine(context)
    .AddRule(new MaxActiveTasksRule(context, usuarioId))
    .AddRule(new UrgentTaskDeadlineRule(context, tarea));

var result = ruleEngine.Validate();

if (!result.IsValid)
{
    foreach (var error in result.Errors)
        _logger.LogWarning("Business rule violation: {Error}", error);
}
```
```
