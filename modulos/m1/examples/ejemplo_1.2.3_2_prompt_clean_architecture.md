# Prompt: Patrones Arquitectónicos Clean Architecture

**Contexto de uso:** Prompt que define constraints arquitectónicos para Clean Architecture.

**Prompt con constraints arquitectónicos:**

```
"Implementa con los siguientes constraints arquitectónicos:

## Clean Architecture Layers
src/
├── Domain/           # Entidades, Value Objects, Enums, Interfaces de repositorio
├── Application/      # Services, DTOs, Interfaces de servicio, Behaviors
├── Infrastructure/   # Repositories, External services, EF Core
└── API/              # Controllers, Filters, Middleware

## Reglas de Dependencia
- Domain no depende de nadie
- Application depende solo de Domain
- Infrastructure depende de Application y Domain
- API depende de Application

## Cross-Cutting Concerns
- Logging: ILogger<T>
- Validation: FluentValidation
- Error Handling: ExceptionHandler middleware
- Caching: IMemoryCache con cache-aside
```

**Diagrama de dependencias:**

```
API → Application → Domain
           ↑
Infrastructure
```

**Reglas clave:**

| Capa | Puede depender de |
|------|-------------------|
| Domain | Ninguna |
| Application | Domain |
| Infrastructure | Application, Domain |
| API | Application |
