# Prompt: Validación de Comportamiento contra Specs

**Contexto de uso:** Prompt para generar matriz de trazabilidad spec→test y verificar cobertura.

**Prompt de validación de comportamiento:**

````
@test-dev Valida que la implementación cumple con specs/001-task-api/spec.md:

## Matriz de Trazabilidad Spec→Test

| Requisito | Criterio Aceptación | Test Case | Estado |
|-----------|---------------------|-----------|--------|
| REQ-TASK-001 | CA-TASK-001 | CreateTarea_ValidInput_Success | ✓ |
| REQ-TASK-001 | CA-TASK-002 | CreateTarea_DuplicateTitle_Throws | ✓ |
| REQ-TASK-001 | CA-TASK-003 | CreateTarea_InvalidInput_Throws | ✓ |
| REQ-TASK-001 | CA-TASK-004 | CreateTarea_Returns201 | ✓ |

## Verificación Automática
```csharp
[Fact]
public void Verify_Trazabilidad_Completa()
{
    // Obtener todos los requisitos de spec.md
    var specs = ParseSpec("specs/001-task-api/spec.md");
    
    // Obtener todos los tests
    var tests = GetAllTestMethods(Assembly.GetExecutingAssembly());
    
    // Verificar cobertura
    var requisitosCubiertos = tests
        .SelectMany(t => t.GetAttributesOfType<RequisitoAttribute>())
        .Select(a => a.RequisitoId)
        .Distinct();
    
    var requisitosEnSpec = specs.Select(s => s.Id);
    
    // Assert cobertura 100%
    requisitosEnSpec.Should().BeSubsetOf(requisitosCubiertos);
}

[Fact]
public void Verify_CriteriosAceptacion_Cumplidos()
{
    var criterios = ParseSpecCriteria("specs/001-task-api/spec.md");
    var testResults = GetTestResults();
    
    foreach (var criterio in criterios)
    {
        var test = FindTestForCriterio(criterio.Id);
        testResults[test.DisplayName].Should().Be(TestOutcome.Passed,
            $"Criterio {criterio.Id} no cumplido");
    }
}
```
````

**Beneficio:** Asegura cobertura 100% de requisitos antes de release.
