# Template: Formato de Respuesta Estructurado

**Contexto de uso:** Plantilla para solicitar respuestas estructuradas que faciliten la validación automatizada.

**Prompt con formato estructurado:**

````
"Genera la implementación siguiendo este formato de respuesta:

## Código Implementado
```csharp
[código completo]
```

## Trazabilidad
| Requisito SDD | Implementado |
|---------------|--------------|
| REQ-AUTH-001 | [línea/método] |
| REQ-AUTH-002 | [línea/método] |

## Validación
- [ ] Cumple criterio de aceptación 1
- [ ] Cumple criterio de aceptación 2

## Archivos Modificados
- src/Domain/ValueObjects/Email.cs
- tests/Domain.Tests/EmailValidatorTests.cs
````

**Secciones del formato:**

| Sección | Propósito |
|---------|-----------|
| Código Implementado | Código generado |
| Trazabilidad | Mapeo requisito → implementación |
| Validación | Checklist de criterios |
| Archivos Modificados | Lista de archivos afectados |

**Beneficio:** Facilita revisión y validación contra especificaciones.
