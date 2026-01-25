# Referencia: Ubicación de Prompts por Fase SDD

**Contexto de uso:** Tabla de referencia para saber dónde guardar los prompts según la fase de desarrollo.

**Dónde guardar los prompts para cada fase SDD:**

| Fase SDD | Dónde añadir el prompt | Ejemplo |
|----------|------------------------|---------|
| **2.1 Diseño** | En `plan.md` o crear `specs/[feature]/design-prompts.md` | Prompts para diagramas, ADRs, arquitectura |
| **2.2 Desarrollo** | En `tasks.md` (columna "Prompt") o `.specify/templates/code-prompts.md` | Prompts para generar código desde specs |
| **2.3 Testing** | En `.specify/templates/test-prompts.md` | Prompts para tests unitarios, integración, BDD |

**Integración con Spec Kit:**

1. **Antes de implementar:** Revisar `tasks.md` y seleccionar la tarea actual
2. **Copiar prompt:** Tomar el prompt correspondiente de la ubicación indicada
3. **Ejecutar:** Usar el prompt con el agente apropiado (`@backend-dev`, `@test-dev`)
4. **Documentar:** Guardar el código generado en la ruta especificada en `tasks.md`
5. **Validar:** Usar `/speckit.analyze` para verificar coherencia con specs
