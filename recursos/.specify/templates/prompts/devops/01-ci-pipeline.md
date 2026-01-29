@devops-dev Generar pipeline CI/CD completamente generativo para PortalEmpleo.

## Paso 1: Leer Artefactos SDD

Leer en orden y extraer:

### 1.1 De .specify/memory/constitution.md
- Technology Standards: .NET version, herramientas
- Testing Requirements: cobertura mínima, framework
- Security Requirements: estándares de seguridad

### 1.2 De docs/quality-gates.md
- Quality gates por categoría (build, test, quality, security, estilo)
- Criterios de aceptación
- Herramientas de verificación
- Acciones si fall (bloquear/advertir)

### 1.3 De specs/devops/devops-spec.md
- Pipeline stages definidos
- Entornos de deployment
- Tipos de gates requeridos

### 1.4 De specs/devops/devops-plan.md (CRÍTICO - Estructura del Pipeline)
- **Jobs definidos**: Lista de jobs del pipeline
- **Orden de jobs**: Dependencias entre jobs (needs)
- **Steps por job**: Steps específicos de cada job
- **Condiciones**: when, if, environment
- **Configuraciones específicas**: paths, herramientas, actions

## Paso 2: Construir Pipeline Desde specs/devops/devops-plan.md

### 2.1 Definir estructura base desde devops-plan

El pipeline debe seguir EXACTAMENTE la estructura definida en specs/devops/devops-plan.md:

```yaml
# Estructura base
name: {PIPELINE_NAME}

on:
  {TRIGGERS_DEFINIDOS_EN_DEVOPS_PLAN}

env:
  DOTNET_VERSION: {DOTNET_VERSION_DESDE_CONSTITUTION}
  COVERAGE_THRESHOLD: {COBERTURA_DESDE_CONSTITUTION}
  {OTRAS_VARIABLES_DE_ENTORNO}

jobs:
  {JOBS_DEFINIDOS_EN_DEVOPS_PLAN}
    name: {JOB_NAME}
    runs-on: {RUNS_ON}
    {CONDITIONS}
    
    steps:
      {STEPS_DEFINIDOS_EN_DEVOPS_PLAN}
```

### 2.2 Generar jobs desde devops-plan

Para cada job definido en specs/devops/devops-plan.md:

| Job | Dependencias | Steps | Condición |
|-----|--------------|-------|-----------|
| {JOB_1_NAME} | {JOB_1_NEEDS} | {JOB_1_STEPS} | {JOB_1_CONDITION} |
| {JOB_2_NAME} | {JOB_2_NEEDS} | {JOB_2_STEPS} | {JOB_2_CONDITION} |
| ... | ... | ... | ... |

### 2.3 Generar steps para cada job

Según specs/devops/devops-plan.md, cada job tiene estos steps:

```yaml
# Ejemplo de job con steps generativos
{JOB_NAME}:
  steps:
    - name: Checkout code
      uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}

    {STEPS_ADICIONALES_DE_DEVOPS_PLAN}

    - name: Upload artifacts
      if: {CONDITION}
      uses: actions/upload-artifact@v4
      with:
        name: {ARTIFACT_NAME}
        path: {ARTIFACT_PATH}
```

### 2.4 Aplicar quality gates

Integrar los quality gates desde docs/quality-gates.md:

| Gate | Job | Condición | Acción si Fall |
|------|-----|-----------|----------------|
| Build | {BUILD_JOB} | {BUILD_CONDITION} | {BUILD_ACTION} |
| Test | {TEST_JOB} | {TEST_CONDITION} | {TEST_ACTION} |
| Quality | {QUALITY_JOB} | {QUALITY_CONDITION} | {QUALITY_ACTION} |
| Security | {SECURITY_JOB} | {SECURITY_CONDITION} | {SECURITY_ACTION} |

## Paso 3: Generar Pipeline CI/CD

Generar .github/workflows/ci.yml siguiendo la estructura extraída:

```yaml
{PIPELINE_YAML_COMPLETO}
```

### Plantilla base del pipeline:

```yaml
name: {PIPELINE_NAME}

on:
  push:
    branches: [{BRANCHES_DESDE_DEVOPS_PLAN}]
  pull_request:
    branches: [{BRANCHES_DESDE_DEVOPS_PLAN}]
  {OTROS_TRIGGERS}

env:
  DOTNET_VERSION: '{DOTNET_VERSION}'
  {OTRAS_VARIABLES}

{JOBS_GENERADOS_DESDE_DEVOPS_PLAN}
```

## Paso 4: Validar Coherencia SDD

Verificar que el pipeline generado:

| Verificación | Estado |
|--------------|--------|
| Jobs coinciden con devops-plan.md | ✅ |
| Dependencias正确 (needs) | ✅ |
| Steps específicos de cada job | ✅ |
| Quality gates de quality-gates.md aplicados | ✅ |
| Variables de constitution.md usadas | ✅ |
| Triggers de devops-spec.md | ✅ |

Artefacto: .github/workflows/ci.yml
