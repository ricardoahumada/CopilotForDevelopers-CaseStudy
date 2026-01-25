# Prompt: Validación de Código contra Especificaciones

**Contexto de uso:** Prompt para validar que una implementación cumple con los criterios de aceptación.

**Prompt de validación:**

````
@backend-dev Valida la implementación de CreateTareaAsync contra specs/001-task-api/spec.md:

## Implementación a Validar
```csharp
public async Task<TareaResponseDto> CreateTareaAsync(CreateTareaDto dto, Guid usuarioId, CancellationToken ct)
{
    var tarea = _factory.CreateFromDto(dto, usuarioId);
    await _repository.AddAsync(tarea, ct);
    await _unitOfWork.SaveChangesAsync(ct);
    return _mapper.Map<TareaResponseDto>(tarea);
}
```

## Criterios de Aceptación de spec.md
| ID | Descripción | Implementado | Evidencia |
|----|-------------|--------------|-----------|
| CA-TASK-001 | Tarea creada con ID único en < 50ms | ? | |
| CA-TASK-002 | No permite duplicados por título | ? | |
| CA-TASK-003 | Retorna 201 con tarea creada | ? | |
| CA-TASK-004 | Valida formato de fecha | ? | |

## Checklist de Validación
```
## Validación CA-TASK-001: ID único en < 50ms
✓ Genera GUID nuevo (línea _factory.CreateFromDto)
- Timing: No medido en código
RECOMENDACIÓN: Agregar timing log

## Validación CA-TASK-002: No duplicados
✗ No implementado
El factory no valida duplicados
RECOMENDACIÓN: Agregar verificación de duplicado

## Validación CA-TASK-003: Response 201
✓ Controller debe retornar CreatedAt
El service retorna DTO correctamente

## Validación CA-TASK-004: Formato fecha
✗ Parcial
FluentValidator no valida formato ISO8601
RECOMENDACIÓN: Agregar DateTime validation
```

## Gaps Identificados
1. Missing: Duplicate title validation
2. Missing: Date format validation
3. Missing: Performance measurement

## Acciones Recomendadas
```
1. Agregar ITareaRepository.ExistsByTitleAsync()
2. Agregar FluentValidation regex para fechas
3. Agregar Stopwatch para métricas
```
````

**Resultado esperado:** Reporte de cumplimiento con gaps y recomendaciones.
