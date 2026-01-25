# Referencia: Comandos Principales de Spec Kit

**Contexto de uso:** Tabla de comandos del CLI de Spec Kit y su propósito.

**Comandos principales de Spec Kit:**

| Comando | Descripción |
|---------|-------------|
| `/speckit.constitution` | Crea o actualiza la Constitución del proyecto |
| `/speckit.specify` | Genera especificación detallada a partir de descripción de alto nivel |
| `/speckit.clarify` | Analiza especificación y formula preguntas para identificar brechas |
| `/speckit.plan` | Genera plan de implementación técnica |
| `/speckit.tasks` | Descompone el plan en tareas discretas |
| `/speckit.implement` | Guía la implementación de tareas |
| `/speckit.analyze` | Verifica coherencia entre especificación, plan, tareas y Constitución |
| `/speckit.checklist` | Genera listas de comprobación de validación |

**Flujo de trabajo típico:**

```
/speckit.constitution → /speckit.specify → /speckit.plan → /speckit.tasks → /speckit.implement
```

**Artefactos generados:**

| Comando | Artefacto |
|---------|-----------|
| `/speckit.constitution` | `.specify/memory/constitution.md` |
| `/speckit.specify` | `specs/[feature]/spec.md` |
| `/speckit.plan` | `specs/[feature]/plan.md` |
| `/speckit.tasks` | `specs/[feature]/tasks.md` |
