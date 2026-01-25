# Prompt: Generar Reporte de Cobertura Orientado a Specs

**Contexto de uso:** Prompt para generar reporte de cobertura que mapea requisitos a tests y código.

**Prompt para generar reporte de cobertura:**

````
@test-dev Genera reporte de cobertura orientado a specs:

## Estructura del Reporte
```markdown
# Reporte de Cobertura SDD

## Resumen Ejecutivo
- Total requisitos: 15
- Requisitos con tests: 15 (100%)
- Coverage de código: 85%
- Tests pasando: 42/42

## Cobertura por Requisito

### REQ-TASK-001: Crear Tarea
- CA-TASK-001: ✓ Tests: 3, Coverage: 92%
- CA-TASK-002: ✓ Tests: 2, Coverage: 88%
- CA-TASK-003: ✓ Tests: 5, Coverage: 95%
- CA-TASK-004: ✓ Tests: 1, Coverage: 100%

### REQ-TASK-002: Listar Tareas
- CA-TASK-010: ✓ Tests: 4, Coverage: 82%
- CA-TASK-011: ✓ Tests: 3, Coverage: 78%

## Detalle de Coverage
```
Archivo                      | Line Coverage | Branch Coverage
-----------------------------|---------------|-----------------
Tarea.cs                     | 95% (38/40)   | 88% (14/16)
TareaService.cs              | 88% (56/64)   | 82% (28/34)
TareaController.cs           | 100% (24/24)  | 100% (8/8)
CreateTareaValidator.cs      | 100% (12/12)  | 100% (4/4)
-----------------------------|---------------|-----------------
Total                        | 85%           | 78%
```

## Gaps de Coverage
1. Tarea.Complete(): Línea 124 no cubierta (exception path)
2. TareaService.GetTareasAsync(): Línea 89 (null check)

## Recomendaciones
- Agregar test para TaskStatus.Completed → Complete()
- Agregar test para null user context
```
````

**Métricas del reporte:**

| Métrica | Descripción |
|---------|-------------|
| Requisitos con tests | % de requisitos cubiertos |
| Line Coverage | Líneas ejecutadas / total |
| Branch Coverage | Ramas ejecutadas / total |
| Gaps | Código sin coverage |
