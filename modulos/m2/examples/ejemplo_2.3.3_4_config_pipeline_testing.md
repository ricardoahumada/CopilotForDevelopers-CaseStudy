# Configuración YAML: Pipeline de Testing SDD

**Contexto de uso:** Configuración de pipeline CI/CD para testing automatizado con coverage.

**Prompt para configurar pipeline de testing:**

````yaml
# .github/workflows/test.yml
name: SDD Testing Pipeline

on:
  push:
    paths:
      - '**.cs'
      - '**.csproj'
      - 'tests/**'
  pull_request:
    branches: [main]

jobs:
  unit-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 8.0.x
      
      - name: Restore dependencies
        run: dotnet restore
      
      - name: Build
        run: dotnet build --no-restore
      
      - name: Run Unit Tests
        run: dotnet test tests/PortalEmpleo.Tests.Unit/ 
          --no-build 
          --verbosity normal 
          --collect:"XPlat Code Coverage"
      
      - name: Upload Coverage
        uses: codecov/codecov-action@v3
        with:
          files: ./tests/PortalEmpleo.Tests.Unit/coverage.cobertura.xml

  integration-tests:
    runs-on: ubuntu-latest
    services:
      sqlserver:
        image: mcr.microsoft.com/mssql/server:2022-latest
        ports:
          5432: 1433
        env:
          SA_PASSWORD: 'TestPassword123!'
          ACCEPT_EULA: 'Y'
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 8.0.x
      
      - name: Run Integration Tests
        run: dotnet test tests/PortalEmpleo.Tests.Integration/
          --no-build
          --verbosity normal
          -e ConnectionStrings__Default="Server=localhost,5432;Database=TestDb;User Id=sa;Password=TestPassword123!"
````

**Jobs del pipeline:**

| Job | Propósito | Dependencias |
|-----|-----------|--------------|
| unit-tests | Tests unitarios + coverage | Ninguna |
| integration-tests | Tests con DB real | SQL Server service |
