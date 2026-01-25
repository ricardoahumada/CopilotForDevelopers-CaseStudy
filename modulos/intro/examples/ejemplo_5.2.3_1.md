# Prompt de Code Review Assistant

**Contexto de uso:** Este ejemplo muestra un prompt especializado para la skill de code review, definiendo el proceso, criterios de evaluación y formato de salida.

**Ejemplo completo:**
```markdown
# Code Review Assistant

## Tarea
Revisa el código proporcionado siguiendo las mejores prácticas.

## Proceso
1. Verifica legibilidad y naming conventions
2. Identifica posibles bugs o edge cases
3. Revisa manejo de errores
4. Verifica cobertura de tests
5. Proporciona sugerencias de mejora

## Formato de Salida
```markdown
## Resumen
- Archivos revisados: X
- Issues encontrados: Y
- Severidad: [Alta/Media/Baja]

## Detalle por Archivo
### archivo.ts
- [ ] Issue 1
- [ ] Issue 2
```
```

**Componentes del prompt:**

| Componente | Propósito | Ejemplo |
|------------|-----------|---------|
| **Tarea** | Define el objetivo | Revisar código siguiendo mejores prácticas |
| **Proceso** | Pasos a seguir | 5 pasos de revisión |
| **Formato de Salida** | Estructura del resultado | Resumen + Detalle por archivo |

**Criterios de revisión:**

| Criterio | Descripción |
|----------|-------------|
| **Legibilidad** | Naming conventions, claridad del código |
| **Bugs/Edge cases** | Posibles errores no manejados |
| **Manejo de errores** | Try-catch, validaciones, logging |
| **Cobertura de tests** | Tests existentes y su calidad |
| **Sugerencias** | Mejoras propuestas |

**Lección aprendida:** Los prompts de skills deben ser estructurados y específicos, definiendo tanto el proceso como el formato de salida esperado.
