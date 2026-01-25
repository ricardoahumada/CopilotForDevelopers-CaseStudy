# Prompt: Documentación Técnica SDD

**Contexto de uso:** Prompt para generar documentación técnica siguiendo templates del proyecto.

**Prompt para documentar especificación:**

```
@docs-agent Genera documentación técnica para specs/001-task-api/
siguiendo el template en .specify/templates/docs-template.md

## Documentos a Generar
1. README.md - Visión general del feature
2. API.md - Documentación de endpoints
3. DATA_MODEL.md - Esquema de datos

## Incluir
- Diagrama de secuencia para operaciones principales
- Tabla de decisiones de diseño (ADR)
- Matriz de trazabilidad req→test
- Notas de implementación para desarrolladores
```

**Documentos generados:**

| Documento | Contenido |
|-----------|-----------|
| `README.md` | Visión general, propósito, uso |
| `API.md` | Endpoints, request/response |
| `DATA_MODEL.md` | Entidades, relaciones |

**Elementos de documentación:**

- Diagramas de secuencia
- ADR (Architecture Decision Records)
- Matriz de trazabilidad
- Notas de implementación
