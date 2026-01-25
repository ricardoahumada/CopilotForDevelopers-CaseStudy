# Estructura: Carpetas para Proyecto Spec Kit

**Contexto de uso:** Estructura de directorios requerida para proyectos Spec-Driven Development con Spec Kit.

**Estructura de carpetas:**

```
mi-proyecto-sdd/
├── .specify/                    # Configuración de Spec Kit
│   ├── memory/
│   │   └── constitution.md     # Principios y restricciones del proyecto
│   ├── scripts/
│   │   ├── check-prerequisites.sh
│   │   ├── common.sh
│   │   └── create-new-feature.sh
│   └── templates/
│       ├── CLAUDE-template.md
│       ├── plan-template.md
│       ├── spec-template.md
│       └── tasks-template.md
├── specs/                       # Especificaciones por característica
│   └── 001-nombre-caracteristica/
│       ├── plan.md
│       ├── spec.md
│       └── tasks.md
├── .github/
│   ├── agents/                  # Agentes personalizados
│   │   └── *.md
│   └── skills/                  # Agent Skills del proyecto
│       └── */
│           └── SKILL.md
├── AGENTS.md                    # Configuración global del proyecto
├── src/                         # Código fuente
└── CLAUDE.md                    # Configuración adicional para agentes
```

**Descripción de carpetas principales:**

| Carpeta | Propósito |
|---------|-----------|
| `.specify/memory/` | Memoria persistente del proyecto (constitución) |
| `.specify/templates/` | Plantillas para specs, plans y tasks |
| `specs/` | Especificaciones organizadas por feature |
| `.github/agents/` | Agentes especializados del proyecto |
| `.github/skills/` | Agent Skills reutilizables |
