# Estructura de Agent Skills

**Contexto de uso:** Este ejemplo muestra la estructura de directorios de un sistema de Agent Skills, organizando las habilidades en carpetas modulares con sus respectivos componentes.

**Estructura:**
```text
skills/
├── code-review/
│   ├── skill.yaml
│   ├── README.md
│   └── prompts/
│       ├── review-code.md
│       └── security-check.md
├── database/
│   ├── skill.yaml
│   ├── README.md
│   └── queries/
│       ├── basic-queries.sql
│       └── migration-template.sql
└── testing/
    ├── skill.yaml
    ├── README.md
    └── templates/
        ├── test-template.ts
        └── mock-data.json
```

**Descripción de componentes:**

| Componente | Propósito | Ejemplo |
|------------|-----------|---------|
| **skill.yaml** | Metadatos y configuración de la skill | name, version, description, capabilities |
| **README.md** | Documentación de uso | Cómo utilizar la skill |
| **prompts/** | Prompts especializados | review-code.md, security-check.md |
| **queries/** | Plantillas SQL | basic-queries.sql, migration-template.sql |
| **templates/** | Plantillas de código | test-template.ts, mock-data.json |

**Organización por dominio:**

| Skill | Dominio | Componentes |
|-------|---------|-------------|
| **code-review** | Calidad de código | Prompts de revisión y seguridad |
| **database** | Operaciones BD | Queries y migraciones |
| **testing** | Pruebas | Templates y mock data |

**Lección aprendida:** La estructura modular de Agent Skills permite reutilizar capacidades especializadas entre proyectos y equipos.
