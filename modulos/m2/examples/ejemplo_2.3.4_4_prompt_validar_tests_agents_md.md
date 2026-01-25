# Prompt: Validar Tests contra AGENTS.md

**Contexto de uso:** Prompt para validar que los tests cumplen con las convenciones definidas en AGENTS.md.

**Prompt para validar tests contra AGENTS.md:**

````
@test-dev Valida que los tests cumplen con AGENTS.md:

## Requisitos de AGENTS.md para Tests
- Naming: Method_Input_ExpectedBehavior
- AAA Pattern: Arrange, Act, Assert
- FluentAssertions para assertions
- No assertions en Arrange/Act
- Tests independientes (no orden)

## Checklist de Validación
```
## Validación Naming Convention

✓ CreateTarea_ValidInput_ReturnsTareaDto
✓ CreateTarea_EmptyTitle_ThrowsValidationException
✗ test1() - Rename to: CreateTarea_DuplicateTitle_ThrowsBusinessException
✗ test2() - Rename to: GetTarea_NotFound_ThrowsNotFoundException

## Validación AAA Pattern

✓ Arrange: Setup mocks
✓ Act: Call service method
✓ Assert: Verify results

✗ CreateTarea_InvalidInput_ThrowsValidationException
  Problem: Assertion in Arrange section
  Fix: Move assertion to Assert section

## Validación FluentAssertions

✓ result.Should().Be(expected)
✓ response.StatusCode.Should().Be(HttpStatusCode.OK)
✗ Assert.Equal(actual, expected) - Use FluentAssertions

## Validación Independencia

✓ Each test creates its own mocks
✓ Each test uses unique test database
✗ Tests share mockEmailService - Create new instance per test
```

## Reporte de Cumplimiento AGENTS.md
```
## AGENTS.md Compliance Report

### Tests Unitarios
- Total tests: 45
- Cumplen naming: 43/45 (96%)
- Cumplen AAA: 45/45 (100%)
- Usan FluentAssertions: 40/45 (89%)
- Independientes: 44/45 (98%)

### Issues Encontrados
1. 2 tests con naming incorrecto (renamed)
2. 5 tests usan Assert nativo en lugar de FluentAssertions
3. 1 test comparte estado con otro test

### Acciones Correctivas
```csharp
// Antes (incorrecto)
Assert.Equal(expected, actual);

// Después (correcto)
actual.Should().Be(expected);
```
```
````

**Validaciones realizadas:**

| Validación | Criterio | Compliance |
|------------|----------|------------|
| Naming | Method_Input_Expected | 96% |
| AAA Pattern | 3 secciones claras | 100% |
| FluentAssertions | Sin Assert nativo | 89% |
| Independencia | Sin estado compartido | 98% |
