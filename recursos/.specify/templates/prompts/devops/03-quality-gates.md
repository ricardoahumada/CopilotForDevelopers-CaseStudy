Generar quality gates para PortalEmpleo basándote en los artefactos fundacionales del proyecto.

## Paso 1: Leer Artefactos Fundacionales

Leer en orden:
1. .specify/memory/constitution.md - Extraer:
   - Testing Requirements (cobertura mínima)
   - Security Requirements (JWT, BCrypt, rate limiting)
   - Performance Requirements (tiempos de respuesta)
   - Coding Standards (convenciones)

2. .specify/memory/conventions.md - Extraer:
   - Herramientas de build y test
   - Estándares de código
   - Formato de documentación
   - Patrones de validación

3. docs/01.caso-practico.md - Extraer:
   - RNF-006 (Autenticación JWT)
   - RNF-007 (Hasheo de Contraseñas)
   - RNF-008 (Validación de Entrada)

## Paso 2: Generar Quality Gates

Generar el documento docs/quality-gates.md con la siguiente estructura:

```markdown
# Quality Gates - PortalEmpleo API

## Referencias de Artefactos SDD

| Artefacto | Sección | Criterios Extraídos |
|-----------|---------|---------------------|
| constitution.md | Testing Requirements | {COBERTURA_MÍNIMA}% cobertura |
| constitution.md | Security Requirements | {REQUISITOS_DE_SEGURIDAD} |
| conventions.md | Estándares | {ESTÁNDARES_DE_CÓDIGO} |
| caso-practico.md | RNF-006, 007, 008 | {RNFs_RELACIONADOS} |

## 1. Quality Gate: Build

| Regla | Criterio | Fuente | Acción si Fall |
|-------|----------|--------|----------------|
| Compilación exitosa | `dotnet build` sin errores | convention.md | Bloquear merge |
| Restore exitoso | `dotnet restore` sin errores | convention.md | Bloquear merge |
| Versión de .NET | {DOTNET_VERSION} | constitution.md | Bloquear merge |

## 2. Quality Gate: Tests

| Regla | Criterio | Fuente | Acción si Fall |
|-------|----------|--------|----------------|
| Todos los tests pasan | 100% passing | constitution.md | Bloquear merge |
| Cobertura de código | ≥{COBERTURA}% | constitution.md | Bloquear merge |
| Cobertura de ramas | ≥{COBERTURA_RAMAS}% | convention.md | Advertir |

## 3. Quality Gate: Análisis Estático

| Regla | Criterio | Fuente | Acción si Fall |
|-------|----------|--------|----------------|
| Bugs | 0 blockers, 0 critical | convention.md | Bloquear merge |
| Code Smells | < {SMELLS_LÍMITE} major | convention.md | Advertir |

## 4. Quality Gate: Seguridad

| Regla | Criterio | Fuente | Acción si Fall |
|-------|----------|--------|----------------|
| Dependencias vulnerables | 0 conocidas | constitution.md (Security) | Bloquear merge |
| Secrets en código | 0 detectados | constitution.md (Security) | Bloquear merge |
| JWT válido | HS256, claims requeridos | RNF-006 | Bloquear merge |
| Password hasheado | BCrypt work factor {WORK_FACTOR} | RNF-007 | Bloquear merge |

## 5. Quality Gate: Estilo de Código

| Regla | Criterio | Fuente | Acción si Fall |
|-------|----------|--------|----------------|
| Formato | `dotnet format` passes | convention.md | Advertir |
| Documentación XML | Métodos públicos documentados | constitution.md | Advertir |

## Matriz de Quality Gates

| Gate | Develop | Feature Branch | Release | Production |
|------|---------|----------------|---------|------------|
| Build | ✅ | ✅ | ✅ | ✅ |
| Tests | ✅ | ⚠️ (≥70%) | ✅ | ✅ |
| Análisis Estático | ✅ | ⚠️ (warnings) | ✅ | ✅ |
| Seguridad | ✅ | ✅ | ✅ | ✅ |
| Estilo | ⚠️ | ❌ | ✅ | ✅ |

**Leyenda:** ✅ Obligatorio | ⚠️ Reducido | ❌ No requerido

## Herramientas de Verificación

| Herramienta | Propósito | Fuente |
|-------------|-----------|--------|
| {HERRAMIENTA_BUILD} | Compilación | convention.md |
| {HERRAMIENTA_TEST} | Ejecución de tests | constitution.md |
| SonarCloud | Análisis estático | convention.md |
| {HERRAMIENTA_SECURITY} | Vulnerabilidades | constitution.md |

## Proceso de Waiver

En casos excepcionales, se puede solicitar waiver de un quality gate:

| Gate | Condición | Aprobador |
|------|-----------|-----------|
| Cobertura | Feature incompleta | Tech Lead |
| Bugs | False positive | Senior Dev |
| Documentación | Refactorización | Tech Lead |

Proceso de Waiver:
1. Crear issue explicando la excepción
2. Documentar razón técnica
3. Obtener aprobación de Tech Lead
4. Crear seguimiento para resolver

## Checklist de Pre-Merge

Antes de hacer merge a develop/main:

- [ ] `dotnet build` compila sin errores
- [ ] `dotnet test` pasa con ≥{COBERTURA}% cobertura
- [ ] Sin bugs críticos en análisis estático
- [ ] Sin vulnerabilidades de seguridad
- [ ] Code review aprobado
- [ ] Documentación actualizada
- [ ] Tests añadidos/modificados
- [ ] Convenciones seguidas
```

## Paso 3: Validar Coherencia SDD

Verificar que los quality gates generados son coherentes con:

- ✅ Cobertura ≥{COBERTURA}% coincide con constitution.md
- ✅ Herramientas coinciden con conventions.md
- ✅ Requisitos de seguridad coinciden con RNF-006, 007, 008

Artefacto: docs/quality-gates.md
