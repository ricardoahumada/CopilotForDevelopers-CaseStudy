# Template: Prompt para Nueva Feature

**Contexto de uso:** Template de prompt reutilizable para especificar nuevas features en SDD.

**Archivo:** `.specify/templates/feature-prompt-template.md`

**Template de prompt para nueva feature:**

````markdown
## [Feature Name]

### Especificación de Referencia
- Archivo: specs/[feature-id]/spec.md
- Requisitos: [lista de requisitos]

### Contexto del Proyecto
- Stack: [tecnologías]
- Patrón: [arquitectura]
- Ubicación: [ruta src/]

### Criterios de Aceptación
- [ ] Criterio 1
- [ ] Criterio 2

### Restricciones
- [restricción 1]
- [restricción 2]

### Formato de Salida
```
## Código
[código]

## Trazabilidad
| Requisito | Implementado |
|-----------|--------------|
| REQ-XXX   | [ubicación] |
```
````

**Uso del template:**

1. Copiar template a `specs/[feature]/prompt.md`
2. Completar cada sección
3. Usar con Copilot para generar implementación
