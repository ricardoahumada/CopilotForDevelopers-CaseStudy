# Comandos Bash: Workflow Spec Kit para PortalEmpleo

**Contexto de uso:** Comandos de Spec Kit para generar specs, plans y tasks del caso práctico.

**Workflow de trabajo:**

```bash
# 1. Ejecutar /speckit.specify para autenticación
/speckit.specify --input labs/01.caso-practico.md --output specs/001-auth/spec.md --rf RF-001 RF-002 RF-003

# 2. Ejecutar /speckit.specify para ofertas
/speckit.specify --input labs/01.caso-practico.md --output specs/002-jobs/spec.md --rf RF-007 RF-008 RF-009

# 3. Ejecutar /speckit.plan para autenticación
/speckit.plan --spec specs/001-auth/spec.md --output specs/001-auth/plan.md

# 4. Ejecutar /speckit.plan para ofertas
/speckit.plan --spec specs/002-jobs/spec.md --output specs/002-jobs/plan.md

# 5. Generar tasks desde plans
/speckit.tasks --plan specs/001-auth/plan.md --output specs/001-auth/tasks.md
/speckit.tasks --plan specs/002-jobs/plan.md --output specs/002-jobs/tasks.md
```

**Flujo de comandos:**

| Paso | Comando | Input | Output |
|------|---------|-------|--------|
| 1 | `/speckit.specify` | caso-practico.md | spec.md |
| 2 | `/speckit.plan` | spec.md | plan.md |
| 3 | `/speckit.tasks` | plan.md | tasks.md |
