# Workflow de Uso Integrado (5 Pasos)

**Contexto de uso:** Este ejemplo describe el flujo de trabajo completo cuando se utilizan los cuatro enfoques de forma integrada, desde la configuración inicial hasta la validación final.

**Flujo de ejecución:**
```
1. Agente lee AGENTS.md
   → Entiende estructura y convenciones del proyecto

2. Agente revisa prototipos de Vibe Coding
   → Obtiene contexto de decisiones iniciales

3. Agente consulta spec/API_SPEC.md
   → Conoce exactamente qué APIs construir

4. Agente utiliza skills/api-generator
   → Genera código siguiendo el spec

5. Agente revisa con skills/code-review
   → Verifica calidad y consistencia

6. Repetir hasta cumplir criterios de aceptación
```

**Pasos detallados:**

| Paso | Componente | Acción | Resultado |
|------|------------|--------|-----------|
| **1** | AGENTS.md | Lee contexto y convenciones | Entiende proyecto |
| **2** | Vibe Coding | Revisa prototipos | Conoce decisiones |
| **3** | spec/API_SPEC.md | Consulta specs | Define qué construir |
| **4** | skills/api-generator | Genera código | Implementa features |
| **5** | skills/code-review | Valida calidad | Verifica estándares |
| **6** | Iteración | Repite hasta cumplir | Código listo |

**Lección aprendida:** El workflow integrado maximiza la efectividad al combinar contexto, exploración, especificación y especialización en un ciclo controlado.
