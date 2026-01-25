# skill.yaml - Configuración de Code Review Skill

**Contexto de uso:** Este ejemplo muestra el archivo skill.yaml que define los metadatos, dependencias y capacidades de una skill de revisión de código.

**Ejemplo completo:**
```yaml
name: code-review
version: 1.0.0
description: Habilidades de revisión de código y análisis estático
author: Engineering Team
dependencies:
  - eslint
  - prettier
capabilities:
  - Realizar review de código
  - Identificar code smells
  - Detectar vulnerabilidades de seguridad
  - Verificar cumplimiento de style guide
```

**Campos del skill.yaml:**

| Campo | Propósito | Ejemplo |
|-------|-----------|---------|
| **name** | Identificador único de la skill | code-review |
| **version** | Versión semántica | 1.0.0 |
| **description** | Descripción breve | Habilidades de revisión de código |
| **author** | Creador o equipo responsable | Engineering Team |
| **dependencies** | Herramientas requeridas | eslint, prettier |
| **capabilities** | Lista de capacidades | Realizar review, identificar smells |

**Lección aprendida:** skill.yaml actúa como contrato de la skill, permitiendo a los agentes descubrir y utilizar capacidades especializadas de forma declarativa.
