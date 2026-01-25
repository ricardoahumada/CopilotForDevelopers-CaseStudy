# Prompt: Generar Tests de Aceptación BDD (SpecFlow)

**Contexto de uso:** Prompt para generar tests de aceptación en formato Gherkin con SpecFlow.

**Prompt para generar tests de aceptación:**

````
@test-dev Genera tests de aceptación BDD para el feature de creación de tareas:

## Especificación de Referencia
- specs/001-task-api/spec.md#acceptance-criteria

## Criterios de Aceptación
CA-TASK-001: Usuario autenticado puede crear tarea con título válido
CA-TASK-002: Sistema rechaza tarea con título duplicado
CA-TASK-003: Sistema rechaza tarea con datos inválidos
CA-TASK-004: Usuario recibe respuesta 201 con tarea creada
CA-TASK-005: Tarea aparece en lista inmediatamente

## Formato: SpecFlow Feature File
```gherkin
Feature: Gestión de Tareas
    Como usuario autenticado
    Quiero crear nuevas tareas
    Para organizar mi trabajo

    Background:
        Given el usuario "testuser@portalempleo.com" está autenticado
        And el usuario tiene acceso al endpoint POST /api/tareas

    Scenario: Crear tarea exitosa
        When el usuario envía una solicitud POST a /api/tareas con:
            | Campo       | Valor            |
            | titulo      | "Revisar informe"|
            | prioridad   | "alta"           |
            | fechaLimite | "2025-12-31"     |
        Then el sistema responde con código 201
        And la respuesta incluye el ID de la tarea creada
        And la tarea tiene estado "pendiente"

    Scenario: Intento de tarea con título duplicado
        Given el usuario tiene una tarea con título "Revisar informe"
        When el usuario envía una solicitud POST a /api/tareas con:
            | Campo  | Valor             |
            | titulo | "Revisar informe" |
        Then el sistema responde con código 409
        And la respuesta incluye el mensaje "Tarea duplicada"

    Scenario Outline: Validación de campos obligatorios
        When el usuario envía una solicitud POST a /api/tareas con:
            | Campo   | Valor     |
            | <campo> | <valor>   |
        Then el sistema responde con código 400
        And la respuesta incluye error de validación para "<campo>"

        Examples:
            | campo     | valor |
            | titulo    | ""    |
            | titulo    | null  |
            | titulo    | "A"   |
            | prioridad | "inv" |

    Scenario: Tarea aparece en lista inmediatamente
        When el usuario crea una tarea con título "Nueva tarea"
        And el usuario envía una solicitud GET a /api/tareas
        Then la respuesta incluye la tarea "Nueva tarea"
```
````

**Escenarios cubiertos:**

| Escenario | Criterio |
|-----------|----------|
| Crear tarea exitosa | CA-TASK-001, CA-TASK-004 |
| Título duplicado | CA-TASK-002 |
| Validación campos | CA-TASK-003 |
| Tarea en lista | CA-TASK-005 |
