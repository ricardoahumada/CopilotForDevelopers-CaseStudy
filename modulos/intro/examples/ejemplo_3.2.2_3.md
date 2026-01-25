# PRODUCT_SPEC.md - Especificación de Producto

**Contexto de uso:** Este ejemplo muestra un PRODUCT_SPEC.md completo que documenta las funcionalidades, APIs requeridas y criterios de aceptación de un sistema de gestión de tareas.

**Ejemplo completo:**
```markdown
# Sistema de Gestion de Tareas - Especificacion de Producto

## Proposito
Sistema de gestion de tareas para equipos pequenos (5-20 usuarios)

## Funcionalidades Core
1. Crear, editar, eliminar tareas
2. Asignar tareas a miembros del equipo
3. Filtrar y buscar tareas por estado y prioridad
4. Notificaciones por email cuando se asigna una tarea

## APIs Requeridas

### GET /api/tasks
- Descripcion: Lista todas las tareas
- Query params: status (optional), assignedTo (optional)
- Response: Array de objetos Task

### POST /api/tasks
- Descripcion: Crea una nueva tarea
- Body: { title, description, assignedTo, dueDate }
- Response: Objeto Task creado

### PUT /api/tasks/:id
- Descripcion: Actualiza una tarea existente
- Body: Campos a actualizar
- Response: Objeto Task actualizado

### DELETE /api/tasks/:id
- Descripcion: Elimina una tarea
- Response: 204 No Content

## Criterios de Aceptacion
- Tiempo de respuesta < 200ms para APIs
- Soporte para hasta 100 tareas concurrentes
- Cobertura de tests > 80%
```

**Componentes de PRODUCT_SPEC:**

| Componente | Propósito | Ejemplo |
|------------|-----------|---------|
| **Propósito** | Descripción de alto nivel | Sistema para equipos de 5-20 usuarios |
| **Funcionalidades** | Lista de features | CRUD tareas, asignación, filtros |
| **APIs requeridas** | Contratos backend | GET /api/tasks, POST /api/tasks |
| **Criterios de aceptación** | Métricas medibles | <200ms respuesta, >80% coverage |

**Lección aprendida:** PRODUCT_SPEC.md sirve como contrato principal entre product managers y desarrolladores, documentando qué construir y cómo se validará el éxito.
