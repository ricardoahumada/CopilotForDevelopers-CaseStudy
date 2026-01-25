# Comparación: Enfoque Tradicional vs Spec-Driven Development

**Contexto de uso:** Esta tabla compara las diferencias fundamentales entre Vibe Coding (enfoque tradicional) y Spec-Driven Development, ilustrando cómo cada metodología aborda el proceso de desarrollo de software.

**Comparación:**

| Aspecto | Enfoque tradicional (Vibe Coding) | Spec-Driven Development |
|-----------------------------------|---------------------------|
| **Descubrimiento** | Iterativo | Claridad inicial |
| **Correcciones** | Múltiples necesarias | Única fuente de verdad |
| **Contexto** | Se pierde con el tiempo | Persistente |
| **Implementaciones** | Genéricas | Personalizadas |
| **Resultados** | Impredecibles | Predecibles |

**Explicación de cada aspecto:**

| Aspecto | Vibe Coding | SDD |
|---------|-------------|-----|
| **Descubrimiento** | El desarrollador explora soluciones iterativamente, ajustando prompts hasta obtener resultados aceptables | Se define completamente qué construir antes de generar código, reduciendo iteraciones |
| **Correcciones** | Múltiples ciclos de prompt → generación → ajuste son necesarios | La especificación actúa como contrato, reduciendo necesidad de correcciones |
| **Contexto** | El agente no mantiene contexto entre sesiones, perdiendo decisiones anteriores | Las specs son documentos persistentes que mantienen el contexto del proyecto |
| **Implementaciones** | El código generado tiende a ser genérico, aplicable a cualquier proyecto | Las especificaciones permiten implementaciones personalizadas para el contexto específico |
| **Resultados** | Mismos prompts pueden producir resultados diferentes en distintas sesiones | Las specs garantizan resultados consistentes y reproducibles |

**Lección aprendida:** SDD invierte el modelo tradicional al front-loadear el contexto, resultando en menos iteraciones y mayor predictibilidad.
