# Referencia: Entregables del Módulo 2 para PortalEmpleo

**Contexto de uso:** Lista de artefactos SDD a generar al completar el Módulo 2.

**Entregables del Módulo 2 (Caso Práctico):**

| Artefacto | Ubicación | Descripción |
|-----------|-----------|-------------|
| `specs/001-auth/spec.md` | `specs/001-auth/` | Especificación funcional de autenticación (16 RF) |
| `specs/002-jobs/spec.md` | `specs/002-jobs/` | Especificación funcional de ofertas (16 RF) |
| `specs/001-auth/plan.md` | `specs/001-auth/` | Plan técnico con arquitectura y decisiones |
| `specs/002-jobs/plan.md` | `specs/002-jobs/` | Plan técnico de ofertas |
| `specs/[feature]/tasks.md` | `specs/[feature]/` | Tareas descompuestas para implementación |

**Conexión con el caso práctico:**

El caso práctico PortalEmpleo (ubicado en `labs/01.caso-practico.md`) define 16 requisitos funcionales (RF-001 a RF-016) en las secciones 2.1-2.3. Este ejercicio transforma esos requisitos en specs formales usando `/speckit.specify` y `/speckit.plan`.

**Artefactos del Módulo 1 necesarios:**

- `.specify/memory/constitution.md` - Principios y estándares
- `AGENTS.md` - Contexto del proyecto
- `.specify/templates/` - Templates de prompts
