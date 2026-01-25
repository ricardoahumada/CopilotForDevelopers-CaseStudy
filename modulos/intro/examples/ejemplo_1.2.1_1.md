# Ciclo Prompt → Generar → Verificar

**Contexto de uso:** Este diagrama representa el flujo básico de trabajo con IA generativa, ilustrando cómo los desarrolladores interactúan con sistemas de IA en un ciclo iterativo de generación y refinamiento de código.

**Flujo básico:**
```
[Prompt] → [Generación] → [Verificación] → [Ajuste] → ...
```

**Explicación del ciclo:**

| Fase | Descripción |
|------|-------------|
| **Prompt** | El desarrollador formula instrucciones en lenguaje natural |
| **Generación** | La IA produce código basado en el prompt |
| **Verificación** | El desarrollador revisa el resultado |
| **Ajuste** | Se realizan correcciones o se itera |

**El "muro de la complejidad":**

El ciclo funciona bien para tareas simples, pero encuentra limitaciones cuando:
- El contexto del proyecto excede la ventana del modelo
- Las dependencias entre componentes requieren coherencia global
- Los cambios en una parte afectan múltiples áreas del código

**Lección aprendida:** Este ciclo iterativo es efectivo para prototipos, pero para proyectos complejos se necesitan metodologías estructuradas como SDD que front-loaden el contexto.
