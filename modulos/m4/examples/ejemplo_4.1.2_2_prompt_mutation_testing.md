# Prompt para Mutation Testing

**Contexto de uso:** Este prompt configura mutation testing para PortalEmpleo usando Stryker para .NET.

**Prompt completo:**
```
@test-agent Configura mutation testing para PortalEmpleo:

## Herramienta: Stryker para .NET

## Configuración
```json
{
  "stryker-config": {
    "project-file": "PortalEmpleo.Tests.csproj",
    "reporters": ["html", "json"],
    "thresholds": {
      "high": 80,
      "low": 60,
      "break": 0
    }
  }
}
```

## Pipeline
```yaml
name: Mutation Testing

on:
  push:
    paths: ['**.cs', 'tests/**']

jobs:
  mutation-test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Install Stryker
        run: dotnet tool install -g dotnet-stryker
      - name: Run Mutation Tests
        run: dotnet stryker
      - name: Check Score
        run: |
          SCORE=$(cat Reports/mutation-report.json | jq '.mutationScore')
          if (( $(echo "$SCORE < 80" | bc -l) )); then
            exit 1
          fi
```
```
