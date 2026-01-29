
@devops-dev Generar pipeline de testing completamente generativo para PortalEmpleo.

## Paso 1: Leer Artefactos SDD

Leer en orden y extraer:

### 1.1 De .specify/memory/constitution.md
- Technology Standards: .NET version, herramientas
- Testing Requirements: cobertura mínima, framework, tipos de tests
- Estándares de testing

### 1.2 De docs/quality-gates.md
- Test quality gates
- Coverage thresholds por tipo de test
- Criterios de aceptación
- Herramientas de verificación

### 1.3 De specs/devops/devops-spec.md
- Tipos de tests definidos
- Configuraciones de testing
- Requisitos de reportes

### 1.4 De specs/devops/devops-plan.md (CRÍTICO - Estructura del Pipeline)
- **Jobs definidos**: Lista de jobs del pipeline de testing
- **Orden de jobs**: Dependencias entre jobs (needs)
- **Steps por job**: Steps específicos de cada job de test
- **Tipos de tests**: unit, integration, e2e, etc.
- **Paths de tests**: ubicaciones de proyectos de test
- **Schedule**: cron schedule para ejecución
- **Condiciones**: when, if, environment

## Paso 2: Construir Pipeline de Testing Desde specs/devops/devops-plan.md

### 2.1 Definir estructura base desde devops-plan

El pipeline debe seguir EXACTAMENTE la estructura definida en specs/devops/devops-plan.md:

```yaml
# Estructura base del pipeline de testing
name: {TEST_PIPELINE_NAME}

on:
  workflow_dispatch:
  schedule:
    - cron: {SCHEDULE_CRON_DE_DEVOPS_PLAN}
  {OTROS_TRIGGERS}

env:
  DOTNET_VERSION: {DOTNET_VERSION_DESDE_CONSTITUTION}
  COVERAGE_THRESHOLD: {COBERTURA_DESDE_CONSTITUTION}
  {OTRAS_VARIABLES_DE_ENTORNO}

jobs:
  {TEST_JOBS_DEFINIDOS_EN_DEVOPS_PLAN}
```

### 2.2 Generar jobs de testing desde devops-plan

Para cada job de test definido en specs/devops/devops-plan.md:

| Job de Test | Tipo | Dependencias | Path de Tests | Condición |
|-------------|------|--------------|---------------|-----------|
| {TEST_JOB_1_NAME} | {UNIT/INTEGRATION/E2E} | {TEST_JOB_1_NEEDS} | {TEST_PATH_1} | {TEST_CONDITION_1} |
| {TEST_JOB_2_NAME} | {UNIT/INTEGRATION/E2E} | {TEST_JOB_2_NEEDS} | {TEST_PATH_2} | {TEST_CONDITION_2} |
| ... | ... | ... | ... | ... |

### 2.3 Generar steps para cada job de test

Según specs/devops/devops-plan.md, cada job de test tiene estos steps:

```yaml
# Ejemplo de job de test con steps generativos
{TEST_JOB_NAME}:
  name: {TEST_JOB_DISPLAY_NAME}
  runs-on: {RUNS_ON}
  needs: {TEST_NEEDS}

  steps:
    - name: Checkout code
      uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}

    - name: Restore dependencies
      run: dotnet restore

    - name: Run {TEST_TYPE} Tests
      run: |
        dotnet test \
          {TEST_PROJECT_PATH} \
          --no-restore \
          --verbosity normal \
          --collect:"XPlat Code Coverage" \
          --configuration Release \
          /p:CollectCoverage=true \
          /p:CoverageThreshold=${{ env.COVERAGE_THRESHOLD }} \
          /p:CoverageOutputFormat=opencover \
          {OPCIONES_ADICIONALES_DE_DEVOPS_PLAN}

    - name: Upload test results
      uses: actions/upload-artifact@v4
      with:
        name: {ARTIFACT_NAME}
        path: {ARTIFACT_PATH}
```

### 2.4 Definir jobs de coverage y summary

Según specs/devops/devops-plan.md:

```yaml
coverage-report:
  name: Coverage Report
  runs-on: ubuntu-latest
  needs: [{JOBS_DE_TEST}]

  steps:
    - name: Checkout code
      uses: actions/checkout@v4

    - name: Download coverage reports
      uses: actions/download-artifact@v4
      with:
        name: {COVERAGE_ARTIFACT_PATTERN}
        path: {COVERAGE_OUTPUT_PATH}

    - name: Generate Coverage Badge
      run: |
        {BADGE_GENERATION_COMMANDS}

    - name: Upload coverage summary
      uses: actions/upload-artifact@v4
      with:
        name: coverage-summary
        path: {COVERAGE_SUMMARY_PATH}

test-summary:
  name: Test Summary
  runs-on: ubuntu-latest
  needs: [coverage-report]

  steps:
    - name: Generate Test Summary
      run: |
        echo "=== Test Summary ==="
        {SUMMARY_COMMANDS}
        echo "===================="

    - name: Comment on PR
      if: github.event_name == 'pull_request'
      uses: actions/github-script@v7
      with:
        script: |
          github.rest.issues.createComment({
            issue_number: context.issue.number,
            owner: context.repo.owner,
            repo: context.repo.repo,
            body: '## Test Results\n\n{PR_COMMENT_BODY}'
          })
```

### 2.5 Aplicar test quality gates

Integrar los test quality gates desde docs/quality-gates.md:

| Test Gate | Job | Threshold | Acción si Fall |
|-----------|-----|-----------|----------------|
| Unit Tests | {UNIT_JOB} | {UNIT_THRESHOLD} | {UNIT_ACTION} |
| Integration Tests | {INT_JOB} | {INT_THRESHOLD} | {INT_ACTION} |
| Coverage | {COVERAGE_JOB} | ≥{COVERAGE_THRESHOLD}% | {COVERAGE_ACTION} |

## Paso 3: Generar Pipeline de Testing

Generar .github/workflows/tests.yml siguiendo la estructura extraída:

```yaml
{TEST_PIPELINE_YAML_COMPLETO}
```

### Plantilla base del pipeline:

```yaml
name: {TEST_PIPELINE_NAME}

on:
  workflow_dispatch:
  schedule:
    - cron: {SCHEDULE_CRON}
  {OTROS_TRIGGERS}

env:
  DOTNET_VERSION: '{DOTNET_VERSION}'
  COVERAGE_THRESHOLD: {COVERAGE_THRESHOLD}
  {OTRAS_VARIABLES}

{GENERATED_JOBS_FROM_DEVOPS_PLAN}
```

## Paso 4: Validar Coherencia SDD

Verificar que el pipeline de testing generado:

| Verificación | Estado |
|--------------|--------|
| Jobs de test coinciden con devops-plan.md | ✅ |
| Paths de proyectos de test correctos | ✅ |
| Dependencias entre jobs correctas | ✅ |
| Coverage threshold de constitution.md | ✅ |
| Test quality gates de quality-gates.md aplicados | ✅ |
| Schedule de devops-plan.md | ✅ |
| Coverage branches threshold | ✅ |

Artefacto: .github/workflows/tests.yml
