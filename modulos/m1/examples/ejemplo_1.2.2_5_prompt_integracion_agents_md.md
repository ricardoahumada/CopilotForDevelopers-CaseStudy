# Prompt: Integración con AGENTS.md

**Contexto de uso:** Ejemplo de prompt que referencia explícitamente las convenciones de AGENTS.md.

**Prompt que referencia AGENTS.md:**

```
@api-agent Implementa el endpoint POST /api/tareas
siguiendo las convenciones del proyecto definidas en AGENTS.md:
- Estándares de código: Microsoft C# Conventions
- Patrón: Controller → Service → Repository
- Error handling: ProblemDetails format
- Testing: xUnit con InMemory database

Especificación técnica: specs/001-task-api/spec.md
Sección: 2.1.1 POST /api/tareas
```

**Elementos del prompt:**

| Elemento | Referencia |
|----------|------------|
| `@api-agent` | Agente especializado |
| Estándares de código | AGENTS.md |
| Patrón arquitectónico | AGENTS.md |
| Error handling | AGENTS.md |
| Testing | AGENTS.md |
| Especificación | `specs/001-task-api/spec.md` |

**Beneficio:** Asegura que el código generado siga todas las convenciones del proyecto.
