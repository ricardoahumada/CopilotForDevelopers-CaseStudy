# Diagrama de Relación: Los Cuatro Enfoques

**Contexto de uso:** Este diagrama ilustra cómo los cuatro enfoques (AGENTS.md, Vibe Coding, Spec Kit y Agent Skills) se relacionan y complementan en un proyecto de software.

**Diagrama:**
```text
┌─────────────────────────────────────────────────────────────────┐
│                    PROYECTO DE SOFTWARE                         │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │                     AGENTS.md                             │  │
│  │   • Contexto del proyecto                                 │  │
│  │   • Convenciones de código                                │  │
│  │   • Restricciones arquitectónicas                         │  │
│  │   • Comandos y workflows                                  │  │
│  └───────────────────────────────────────────────────────────┘  │
│                              │                                  │
│                              ▼                                  │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │              VIBE CODING (Fase Exploratoria)              │  │
│  │   • Prototipado rápido de ideas                           │  │
│  │   • Exploración de soluciones                             │  │
│  │   • Validación de conceptos                               │  │
│  └───────────────────────────────────────────────────────────┘  │
│                              │                                  │
│                              ▼                                  │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │              SPEC KIT (SDD - Fase Estructurada)           │  │
│  │   • Especificaciones formales                             │  │
│  │   • Criterios de aceptación                               │  │
│  │   • Plan de implementación                                │  │
│  └───────────────────────────────────────────────────────────┘  │
│                              │                                  │
│                              ▼                                  │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │                    AGENT SKILLS                           │  │
│  │   • Habilidades especializadas                            │  │
│  │   • Templates reutilizables                               │  │
│  │   • Flujos de trabajo específicos                         │  │
│  └───────────────────────────────────────────────────────────┘  │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

**Flujo de integración:**

| Fase | Componente | Propósito |
|------|------------|-----------|
| **1. Contexto** | AGENTS.md | Establece reglas y convenciones |
| **2. Exploración** | Vibe Coding | Prototipado rápido |
| **3. Estructuración** | Spec Kit | Especificaciones formales |
| **4. Ejecución** | Agent Skills | Capacidades especializadas |

**Lección aprendida:** Los cuatro enfoques no son mutuamente excluyentes; se complementan en un flujo que va desde el contexto hasta la ejecución especializada.
