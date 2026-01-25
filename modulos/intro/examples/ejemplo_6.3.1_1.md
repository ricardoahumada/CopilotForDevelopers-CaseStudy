# Estructura del Proyecto - Uso Integrado

**Contexto de uso:** Este ejemplo muestra la estructura de un proyecto que utiliza los cuatro enfoques de forma integrada, organizando AGENTS.md, specs, skills y código fuente.

**Estructura:**
```text
mi-proyecto/
├── AGENTS.md              # Contexto y convenciones
├── spec/                  # Especificaciones SDD
│   ├── PRODUCT_SPEC.md
│   └── API_SPEC.md
├── skills/                # Habilidades especializadas
│   ├── api-generator/
│   └── notification-templates/
└── src/
    └── ...                # Código generado
```

**Organización por componente:**

| Componente | Ubicación | Propósito |
|------------|-----------|-----------|
| **AGENTS.md** | Raíz | Contexto persistente para agentes |
| **spec/** | spec/ | Especificaciones formales |
| **skills/** | skills/ | Habilidades modulares |
| **src/** | src/ | Código de producción |

**Relación entre componentes:**

```
AGENTS.md
    │
    ├──► spec/PRODUCT_SPEC.md ──► skills/api-generator ──► src/
    │         │
    └──► spec/API_SPEC.md ──────► skills/notifications ──► src/
```

**Lección aprendida:** La estructura integrada permite que los agentes consulten contexto (AGENTS.md), especificaciones (spec/) y skills (skills/) para generar código de calidad en src/.
