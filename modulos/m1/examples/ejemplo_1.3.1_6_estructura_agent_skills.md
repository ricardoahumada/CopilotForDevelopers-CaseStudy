# Estructura: Agent Skills para Proyecto SDD

**Contexto de uso:** Estructura de carpetas para organizar Agent Skills en un proyecto.

**Estructura de Agent Skills:**

```
.github/skills/
├── api-development/
│   ├── SKILL.md
│   └── templates/
│       ├── controller-template.cs
│       ├── dto-template.cs
│       └── validator-template.cs
├── testing/
│   ├── SKILL.md
│   └── templates/
│       ├── unit-test-template.cs
│       └── integration-test-template.cs
├── database/
│   ├── SKILL.md
│   └── scripts/
│       └── migration-template.sql
└── documentation/
    ├── SKILL.md
    └── templates/
        ├── api-doc-template.md
        └── adr-template.md
```

**Skills incluidos:**

| Skill | Propósito | Templates |
|-------|-----------|-----------|
| `api-development` | Desarrollo de APIs | Controller, DTO, Validator |
| `testing` | Testing automatizado | Unit, Integration |
| `database` | Migraciones BD | SQL scripts |
| `documentation` | Documentación técnica | API docs, ADR |

**Ubicación:** `.github/skills/` para compartir con el equipo vía Git.
