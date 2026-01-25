# Prompt para Quality Gates Automation

**Contexto de uso:** Este prompt implementa quality gates automatizados en el pipeline CI/CD.

**Prompt completo:**
```
@agent Implementa quality gates automatizados:

## Quality Gates
1. Coverage >= 80%
2. No new code smells high severity
3. Todos los tests pasando
4. Cumplimiento de specs

```yaml
name: Quality Gate Check
on:
  pull_request:
    branches: [main, develop]

jobs:
  quality-gate:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - name: Run Tests with Coverage
        run: |
          dotnet test --collect:"XPlat Code Coverage"
          reportgenerator -reports:*/coverage.cobertura.xml -targetdir:coverage-report
          COVERAGE=$(cat coverage-report/Summary.json | jq '.overallMetrics.lineCoverage')
          if (( $(echo "$COVERAGE < 80" | bc -l) )); then exit 1; fi
      
      - name: Check Code Smells
        run: |
          dotnet run --project tools/CodeSmellDetector/ --project src/ --output smells.json
          HIGH_SMELLS=$(cat smells.json | jq '[.[] | select(.severity == "High")] | length')
          if [ "$HIGH_SMELLS" -gt 0 ]; then exit 1; fi
      
      - name: Verify Spec Compliance
        run: |
          dotnet run --project tools/SpecComplianceValidator/ --specs specs/ --src src/ --output compliance.json
          COMPLIANT=$(cat compliance.json | jq '.isCompliant')
          if [ "$COMPLIANT" = "false" ]; then exit 1; fi
```
```
