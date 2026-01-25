# Prompt: Generación desde Especificación spec.md

**Contexto de uso:** Patrón para transformar especificaciones formales en código funcional.

**Estructura del patrón:**

```
1. Referenciar la especificación fuente
2. Proporcionar contexto técnico del proyecto
3. Especificar restricciones arquitectónicas
4. Definir formato de salida
5. Solicitar trazabilidad a requisitos
```

**Prompt de generación desde spec.md:**

````
Genera la implementación completa del endpoint GET /api/tareas/{id}
según la especificación en specs/001-task-api/spec.md#endpoint-get-tarea

## Especificación de Referencia
- Sección: 2.1.3 GET /api/tareas/{id}
- Criterios de aceptación: CA-TASK-003, CA-TASK-004

## Contexto Técnico
- Controller: src/API/Controllers/TareasController.cs
- Service: src/Application/Services/TareaService.cs
- Repository: src/Infrastructure/Data/Repositories/TareaRepository.cs
- Response DTO: src/API/DTOs/TareaResponseDto.cs

## Requisitos de Implementación
1. Obtener tarea por ID con Include de UsuarioAsignado
2. Retornar 404 si no existe
3. Retornar 401 si no tiene permisos
4. Cache headers para GET

## Formato de Salida
```csharp
// Endpoint completo
[Controller action]

// Tests requeridos
[Test]
public void GetTarea_Existe_ReturnsOkWithTarea()
```
````

**Beneficio:** Genera código trazable a requisitos específicos.
