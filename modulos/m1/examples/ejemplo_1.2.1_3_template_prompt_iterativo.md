# Template: Prompt Iterativo para Refinamiento

**Contexto de uso:** Plantilla para refinar prompts basándose en resultados anteriores y especificaciones.

**Plantilla de prompt iterativo:**

```
"Basándote en la especificación en specs/001-auth-api/spec.md y el feedback anterior:

## Feedback Anterior
[Descripción de qué no funcionó]

## Especificación de Referencia
- Requisito: [ID del requisito]
- Criterios de aceptación: [lista]

## Refinamiento Solicitado
[Cambios específicos o adiciones]

## Validación Requerida
[Cómo verificar que el resultado es correcto]"
```

**Ciclo de mejora continua SDD:**

```mermaid
graph TD
    A[Definir especificación] --> B[Crear prompt inicial]
    B --> C[Generar código]
    C --> D[Validar contra spec]
    D --> E{¿Cumple criterios?}
    E -->|No| F[Identificar gaps]
    F --> G[Refinar prompt]
    G --> B
    E -->|Sí| H[Documentar decisión]
```

**Beneficio:** Permite mejora incremental hasta cumplir todos los criterios de aceptación.
