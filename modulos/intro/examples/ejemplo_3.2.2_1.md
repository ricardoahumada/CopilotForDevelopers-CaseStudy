# Estructura del Proyecto con Spec Kit

**Contexto de uso:** Esta estructura de directorios muestra cómo organizar un proyecto que utiliza Spec Kit, separando claramente las especificaciones, planes, tareas y código fuente.

**Estructura:**
```
mi-proyecto/
├── AGENTS.md              # Contexto para agentes
├── spec/                  # Especificaciones SDD
│   ├── CONSTITUTION.md    # Principios y restricciones del proyecto
│   ├── PRODUCT_SPEC.md    # Especificacion de producto
│   ├── API_SPEC.md        # Especificacion de API
│   ├── UI_SPEC.md         # Especificacion de interfaz
│   └── quality/
│       ├── TEST_SPEC.md
│       └── PERFORMANCE_SPEC.md
├── plan/                  # Planes tecnicos generados
│   └── ARCHITECTURE_PLAN.md
├── tasks/                 # Tareas de implementacion
│   └── IMPLEMENTATION_TASKS.md
└── src/                   # Codigo generado
    └── ...
```

**Descripción de cada directorio:**

| Directorio | Propósito | Contenido |
|------------|-----------|-----------|
| **AGENTS.md** | Contexto persistente para agentes | Convenciones, stack, restricciones |
| **spec/** | Especificaciones formales | PRODUCT_SPEC, API_SPEC, CONSTITUTION |
| **spec/quality/** | Criterios de calidad | TEST_SPEC, PERFORMANCE_SPEC |
| **plan/** | Planes técnicos | ARCHITECTURE_PLAN.md |
| **tasks/** | Tareas descompuestas | IMPLEMENTATION_TASKS.md |
| **src/** | Código fuente | Implementaciones |

**Flujo de trabajo:**

```
AGENTS.md → spec/ → plan/ → tasks/ → src/
   │           │        │        │       │
   │           ▼        ▼        ▼       ▼
Contexto   Especificación  Plan   Tareas Código
```

**Lección aprendida:** Esta estructura separa claramente las diferentes fases del desarrollo, facilitando la revisión, mantenimiento y trazabilidad entre specs y código.
