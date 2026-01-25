# Prompt: Transformar Spec a Código - Endpoint POST /api/tareas

**Contexto de uso:** Prompt completo para implementar un endpoint desde especificaciones formales.

**Prompt para transformar spec a código:**

````
Implementa el endpoint POST /api/tareas basado en specs/001-task-api/spec.md:

## Especificación de Referencia
- Sección: 2.1.1 POST /api/tareas
- Requisito: REQ-TASK-001
- Criterios de aceptación: CA-TASK-001, CA-TASK-002, CA-TASK-003

## Especificación del Endpoint
```http
POST /api/tareas
Content-Type: application/json
Authorization: Bearer <token>

Request Body:
{
  "titulo": "string (1-200 chars)",
  "descripcion": "string (0-2000 chars)",
  "fechaLimite": "ISO8601 datetime",
  "prioridad": "baja|media|alta|urgente",
  "etiquetas": ["string"]
}

Response (201 Created):
{
  "id": "uuid",
  "titulo": "string",
  "descripcion": "string",
  "fechaLimite": "ISO8601 datetime",
  "prioridad": "string",
  "estado": "pendiente",
  "creadoPor": "uuid",
  "creadoEn": "ISO8601 datetime"
}
```

## Reglas de Negocio
1. Título requerido, 1-200 caracteres
2. Prioridad default: media
3. Estado inicial: pendiente
4. creadoEn = now UTC
5. Validar que usuario tiene permisos

## Contexto del Proyecto
- Controller: src/API/Controllers/TareasController.cs
- Service: src/Application/Services/TareaService.cs
- Repository: src/Infrastructure/Data/Repositories/TareaRepository.cs
- DTOs: src/Application/DTOs/Tareas/
- Validator: src/Application/Validators/CreateTareaValidator.cs

## Convenciones
- XML docs para APIs públicas
- FluentValidation para DTOs
- ProblemDetails para errores
- CancellationToken en todos los métodos async

## Trazabilidad
| Requisito | Implementado en |
|-----------|-----------------|
| REQ-TASK-001 | TareaService.CreateTarea |
| CA-TASK-001 | CreateTareaValidator |
| CA-TASK-002 | TareaService con tracking |
| CA-TASK-003 | ProblemDetails response |
````

**Dónde guardar:** `specs/001-task-api/implementation-prompts.md`
