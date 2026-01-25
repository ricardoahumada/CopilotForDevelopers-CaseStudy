# Workflow: SDD con Scrum

**Contexto de uso:** Workflow que integra Spec-Driven Development con metodología Scrum.

**Archivo:** `.specify/workflows/scrum-workflow.md`

**Workflow SDD con Scrum:**

```markdown
## Sprint SDD Workflow

### Sprint Planning (Día 1)
1. [ ] Review backlog de user stories
2. [ ] Seleccionar stories para el sprint
3. [ ] Crear specs/ para cada story
4. [ ] Generar plans y tasks

### Daily (Días 2-5)
1. [ ] Tomar task de tasks.md
2. [ ] Implementar según spec
3. [ ] Validar contra criterios de aceptación
4. [ ] Actualizar estado en tasks.md

### Sprint Review (Día 6)
1. [ ] Demo de features completados
2. [ ] Validación contra specs originales
3. [ ] Documentar aprendizajes

### Retrospective (Día 6)
1. [ ] Identificar mejoras en proceso SDD
2. [ ] Actualizar templates si necesario
3. [ ] Compartir mejores prácticas
```

**Integración SDD-Scrum:**

| Fase Scrum | Artefacto SDD |
|------------|---------------|
| Planning | `spec.md`, `plan.md` |
| Daily | `tasks.md` (estado) |
| Review | Validación vs `spec.md` |
| Retro | Mejora de templates |
