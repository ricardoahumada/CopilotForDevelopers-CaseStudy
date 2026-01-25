# Estructura de AGENTS.md con Frontmatter

**Contexto de uso:** Este ejemplo muestra la estructura básica de un archivo AGENTS.md, incluyendo el frontmatter requerido y las secciones principales que debe contener.

**Estructura:**
```markdown
---
name: nombre-agente
description: Descripción en una oración
---

You are an expert [rol] for this project.

## Your role / Persona
## Project knowledge (tech stack, file structure)
## Commands you can use
## Standards / Code style
## Boundaries (Always do ✅, Ask first ⚠️, Never do 🚫)
```

**Secciones del AGENTS.md:**

| Sección | Propósito | Ejemplo |
|---------|-----------|---------|
| **Frontmatter** | Metadatos del agente | name: docs_agent, description: Expert technical writer |
| **Your role** | Define la personalidad y expertise | "You are an expert technical writer" |
| **Project knowledge** | Contexto técnico del proyecto | Tech stack, estructura de archivos |
| **Commands** | Comandos ejecutables | npm test, npm run build |
| **Standards** | Convenciones de código | TypeScript strict, ESLint rules |
| **Boundaries** | Límites de acción | Always do, Ask first, Never do |

**Sistema de Boundaries:**

| Símbolo | Significado | Ejemplo |
|---------|-------------|---------|
| ✅ **Always do** | Acciones permitidas sin preguntar | Write new files to docs/ |
| ⚠️ **Ask first** | Requiere confirmación del usuario | Modificar documentos existentes |
| 🚫 **Never do** | Acciones prohibidas | Modificar código fuente, eliminar docs |

**Lección aprendida:** AGENTS.md bien estructurado proporciona contexto persistente que los agentes pueden consultar en cada sesión, mejorando consistencia y reduciendo errores.
