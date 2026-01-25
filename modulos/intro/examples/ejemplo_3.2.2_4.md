# API_SPEC.md - Especificación de API

**Contexto de uso:** Este ejemplo muestra un API_SPEC.md que define modelos de datos, endpoints, formatos de request/response y códigos de error para una API de gestión de tareas.

**Ejemplo completo:**
```markdown
# API Specification - Task Management API

## Base URL
/api/v1

## Data Models

### Task
```json
{
  "id": "uuid",
  "title": "string",
  "description": "string",
  "status": "pending | in_progress | completed",
  "assignedTo": "uuid",
  "createdAt": "ISO8601 timestamp",
  "dueDate": "ISO8601 timestamp"
}
```

## Endpoints

### Create Task
```http
POST /tasks
Content-Type: application/json

{
  "title": "Implementar login",
  "description": "Crear pantalla de login con validacion",
  "assignedTo": "user-123",
  "dueDate": "2025-12-31"
}
```

### Response Codes
- 201: Task created successfully
- 400: Validation error
- 404: User not found
```

**Componentes de API_SPEC:**

| Componente | Propósito | Ejemplo |
|------------|-----------|---------|
| **Base URL** | Prefijo de la API | /api/v1 |
| **Data Models** | Schemas de entidades | Task con todos sus campos |
| **Endpoints** | Definición de rutas | POST /tasks |
| **Request format** | Formato de datos de entrada | JSON con title, description, assignedTo |
| **Response codes** | Códigos de estado HTTP | 201, 400, 404 |

**Lección aprendida:** API_SPEC.md proporciona contratos detallados que permiten a los agentes generar implementaciones consistentes con las expectativas del consumidor de la API.
