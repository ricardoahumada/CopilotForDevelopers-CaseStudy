# Prompt: Invocación de docs-agent

**Contexto de uso:** Ejemplo de prompt que invoca un agente especializado definido en AGENTS.md.

**Prompt para docs-agent:**

```
"@docs-agent Genera documentación API para el endpoint POST /api/users
siguiendo las convenciones definidas en AGENTS.md.
Incluye:
- Descripción del endpoint
- Parámetros de request
- Schema de request body
- Códigos de respuesta
- Ejemplo de curl
- Notas de autenticación requerida"
```

**Elementos del prompt:**

| Elemento | Descripción |
|----------|-------------|
| `@docs-agent` | Invoca agente especializado |
| Referencia a AGENTS.md | Asegura convenciones del proyecto |
| Lista de secciones | Define estructura esperada |

**Resultado esperado:** Documentación completa del endpoint siguiendo las convenciones del proyecto.
