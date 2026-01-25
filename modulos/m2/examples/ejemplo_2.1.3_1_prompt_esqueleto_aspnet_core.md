# Prompt: Esqueleto de Proyecto ASP.NET Core con SDD

**Contexto de uso:** Prompt para generar estructura completa de proyecto ASP.NET Core 8 siguiendo Clean Architecture.

**Prompt para generar esqueleto de proyecto C#:**

````
"Genera estructura completa de proyecto ASP.NET Core 8 para PortalEmpleo API:

## Especificación de Referencia
- Feature: Task Management API
- Spec: specs/001-task-api/spec.md

## Requisitos Técnicos
- .NET 8, C# 12
- Clean Architecture
- Entity Framework Core 8
- SQL Server 2022
- xUnit para testing
- FluentValidation

## Estructura Requerida
```
src/
├── PortalEmpleo.API/
│   ├── Controllers/
│   ├── Middleware/
│   ├── Filters/
│   ├── Program.cs
│   └── appsettings.json
├── PortalEmpleo.Application/
│   ├── Common/
│   │   ├── Behaviors/
│   │   ├── Exceptions/
│   │   └── Mapping/
│   ├── Features/
│   │   └── Tasks/
│   │       ├── Commands/
│   │       ├── Queries/
│   │       └── DTOs/
│   ├── Interfaces/
│   └── Services/
├── PortalEmpleo.Domain/
│   ├── Common/
│   ├── Entities/
│   ├── Enums/
│   ├── Exceptions/
│   ├── Interfaces/
│   └── ValueObjects/
├── PortalEmpleo.Infrastructure/
│   ├── Data/
│   │   ├── Configurations/
│   │   ├── Context/
│   │   └── Repositories/
│   ├── Identity/
│   ├── Persistence/
│   └── Services/
└── PortalEmpleo.Tests/
    ├── Unit/
    └── Integration/
```

## Archivos a Incluir
1. Program.cs con configuración completa
2. Controllers base (BaseController.cs)
3. Program.cs con Dependency Injection
4. csproj files con dependencias correctas
5. appsettings.json con connection strings

## Convenciones
- Namespaces: PortalEmpleo.[Layer]
- Clases parciales para controllers
- FluentValidation validators en mismo namespace que DTO
- DbContext en Infrastructure.Data
"""
````

**Dónde guardar templates:** `.specify/templates/project-skeletons/`
