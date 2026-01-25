# Prompt: Estructura de Proyecto ASP.NET Core con SDD

**Contexto de uso:** Prompt para generar la estructura completa de un proyecto ASP.NET Core 8 con Spec-Driven Development.

**Prompt para generar estructura de proyecto:**

```
Genera la estructura de proyecto ASP.NET Core 8 con SDD:

## Requisitos
- Clean Architecture con spec-kit
- AGENTS.md con agente @backend-dev
- Agent Skills para API development y testing
- Templates para controllers, DTOs, validators
- Integración con SQL Server

## Estructura a Generar
src/
├── Domain/
│   ├── Entities/
│   ├── ValueObjects/
│   ├── Enums/
│   └── Interfaces/
├── Application/
│   ├── DTOs/
│   ├── Interfaces/
│   ├── Services/
│   └── Behaviors/
├── Infrastructure/
│   ├── Data/
│   │   ├── Configurations/
│   │   └── Repositories/
│   └── Services/
└── API/
    ├── Controllers/
    ├── Middleware/
    ├── Filters/
    └── Program.cs

specs/
templates/
.github/
  ├── agents/
  └── skills/
```

**Capas de Clean Architecture:**

| Capa | Contenido |
|------|-----------|
| Domain | Entidades, Value Objects, Interfaces |
| Application | DTOs, Services, Behaviors |
| Infrastructure | Repositories, External Services |
| API | Controllers, Middleware, Filters |
