# Prompt: Uso de Agent Skills Especializados

**Contexto de uso:** Prompt que invoca una Agent Skill específica para testing E2E.

**Prompt invocando Agent Skill:**

```
@agent Usa la skill webapp-testing para crear tests E2E
del flujo de creación de tareas en PortalEmpleo.

## Agent Skill a Usar
- Skill: .github/skills/e2e-testing/
- Template: test-template.spec.ts
- Navegador: Playwright con Chromium

## Escenarios a Testear
1. Usuario logueado puede crear tarea
2. Usuario no logueado recibe 401
3. Datos inválidos muestran errores de validación
4. Tarea aparece en lista inmediatamente

## Datos de Test
- Usuario test: testuser@portalempleo.com
- Permisos: employee
```

**Escenarios de test E2E:**

| Escenario | Resultado esperado |
|-----------|-------------------|
| Usuario logueado crea tarea | 201 Created |
| Usuario no logueado | 401 Unauthorized |
| Datos inválidos | Errores de validación |
| Tarea creada | Visible en lista |

**Beneficio:** Reutiliza templates y procedimientos de la skill para consistencia.
