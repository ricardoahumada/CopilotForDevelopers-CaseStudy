# Prompt: Validación contra Especificaciones

**Contexto de uso:** Patrón para verificar que código existente cumple con especificaciones.

**Prompt de validación:**

````
Valida el código en src/Application/Services/TareaService.cs
contra la especificación en specs/001-task-api/spec.md

## Checklist de Validación
- [ ] Método CreateTarea implementa validación de negocio
- [ ] UpdateTarea verifica ownership
- [ ] DeleteTarea implementa soft delete
- [ ] GetAllTareas aplica filtros de usuario
- [ ] Maneja excepciones según Error Handling Policy

## Criterios de Aceptación a Verificar
CA-TASK-001: Tarea creada con ID único en < 50ms
CA-TASK-002: No permite crear tareas duplicadas por título
CA-TASK-005: Lista filtrada visible solo para usuarios con acceso

## Reporte de Validación
```
## Cumplimiento
- Total requisitos: X
- Implementados: Y
- Pendientes: Z

## Hallazgos
[Detalles por requisito no implementado]
```
````

**Estructura del reporte:**

| Sección | Contenido |
|---------|-----------|
| Cumplimiento | Métricas de implementación |
| Hallazgos | Detalles de gaps encontrados |
