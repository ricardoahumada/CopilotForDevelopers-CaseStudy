# Prompt: Generar AGENTS.md con Múltiples Agentes

**Contexto de uso:** Prompt para crear AGENTS.md con agentes especializados por rol.

**Prompt para generar AGENTS.md con múltiples agentes:**

```
Genera un archivo AGENTS.md para un proyecto ASP.NET Core 8 con:
- Agente @backend-dev para desarrollo de APIs C#
- Agente @frontend-dev para React TypeScript
- Agente @test-dev para generación de tests
- Agente @docs-dev para documentación técnica

Incluye para cada agente:
- Persona/rol específico
- Stack tecnológico
- Comandos disponibles
- Estándares de código
- Límites (Always/Ask/Never)
```

**Estructura de cada agente:**

| Sección | Contenido |
|---------|-----------|
| Persona | Descripción del rol |
| Stack | Tecnologías específicas |
| Commands | Comandos CLI disponibles |
| Standards | Convenciones de código |
| Boundaries | Always/Ask/Never rules |

**Ubicación:** 
- AGENTS.md global en raíz del proyecto
- `.github/agents/` para agentes especializados
