# Estructura: Entregables en /artifacts/module2/

**Contexto de uso:** Estructura de carpetas esperada para los entregables del Módulo 2.

**Entregables esperados en `/artifacts/module2/`:**

```
artifacts/module2/
├── specs/
│   ├── 001-auth/
│   │   ├── spec.md           # Criterios de aceptación: CA-AUTH-001 a CA-AUTH-005
│   │   ├── plan.md           # Decisiones arquitectónicas
│   │   └── tasks.md          # TASK-AUTH-001 a TASK-AUTH-010
│   └── 002-jobs/
│       ├── spec.md           # Criterios de aceptación: CA-JOBS-001 a CA-JOBS-012
│       ├── plan.md
│       └── tasks.md
└── templates/
    └── spec-template.md      # Template reutilizable
```

**Artefactos por feature:**

| Feature | spec.md | plan.md | tasks.md |
|---------|---------|---------|----------|
| 001-auth | CA-AUTH-001 a 005 | ADRs auth | TASK-AUTH-001 a 010 |
| 002-jobs | CA-JOBS-001 a 012 | ADRs jobs | TASK-JOBS-001 a 015 |

**Véase también:**

- Sección 5.2 del caso práctico para entregables específicos del Módulo 2
- Sección 2.1 para generación de diagramas y ADRs
- Sección 2.2 para implementación de patrones
