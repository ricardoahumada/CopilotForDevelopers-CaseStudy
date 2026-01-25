# Diagrama: Workflow de Prompts en Dos Niveles

**Contexto de uso:** Arquitectura de dos niveles para organizar prompts en SDD.

**Nivel 1: Templates Globales**

> **Ubicación:** `.specify/templates/`
>
> **Propósito:** Prompts reutilizables en cualquier proyecto o feature
>
> **Ejemplos:**
> - `.specify/templates/design-prompt-template.md`
> - `.specify/templates/code-prompt-template.md`
> - `.specify/templates/test-prompt-template.md`

**Nivel 2: Prompts Específicos por Feature**

> **Ubicación:** `specs/[feature]/`
>
> **Propósito:** Adaptaciones concretas para una feature específica
>
> **Ejemplos:**
> - `specs/001-task-api/design-prompts.md`
> - `specs/001-task-api/implementation-prompts.md`

**Workflow Recomendado:**

```
┌─────────────────────────────────────────────────────────────┐
│  1. FEATURE NUEVA                                           │
│     └─→ Elegir template global de .specify/templates/       │
├─────────────────────────────────────────────────────────────┤
│  2. ADAPTAR                                                 │
│     └─→ Guardar en specs/[feature]/ como [tipo]-prompts.md  │
├─────────────────────────────────────────────────────────────┤
│  3. USAR                                                    │
│     └─→ Copilot ejecuta el prompt adaptado                  │
├─────────────────────────────────────────────────────────────┤
│  4. ITERAR                                                  │
│     └─→ Mejorar y guardar versión final                     │
├─────────────────────────────────────────────────────────────┤
│  5. REUTILIZAR                                              │
│     └─→ Para features similares (copiar y adaptar)          │
└─────────────────────────────────────────────────────────────┘
```

**Beneficios:**

| Beneficio | Descripción |
|-----------|-------------|
| **Trazabilidad** | Saber qué prompt generó cada artefacto de código |
| **Reutilización** | Prompts probados se usan en features similares |
| **Colaboración** | Todo el equipo usa los mismos prompts |
| **Mejora continua** | Iterar y optimizar prompts con el tiempo |
| **Auditoría** | Compliance: demostrar cómo se generó el código |
