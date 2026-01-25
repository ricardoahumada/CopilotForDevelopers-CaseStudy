# Prompt: Documentar Patrones de Organización de Código

**Contexto de uso:** Prompt para documentar los patrones arquitectónicos usados en el proyecto.

**Prompt para documentar patrones arquitectónicos:**

````
Documenta los patrones de organización de código para PortalEmpleo:

## Patrón: Feature-First (C#)

### Estructura
```
src/Features/
└── Tasks/
    ├── Tasks.csproj
    ├── Controllers/
    ├── Services/
    ├── Repositories/
    ├── DTOs/
    ├── Validators/
    ├── Entities/
    └── Tests/
```

## Patrón: Component-Based (React Native)

### Estructura
```
src/components/
├── FeatureName/
│   ├── ComponentName.tsx
│   ├── ComponentName.styles.ts
│   ├── ComponentName.test.tsx
│   └── index.ts
└── common/
```

## Patrón: DDD Layered

### Estructura
```
src/
├── Domain/           # Enterprise logic
│   ├── Entities/
│   ├── ValueObjects/
│   ├── Enums/
│   ├── Interfaces/
│   └── Events/
├── Application/      # Use cases
│   ├── Services/
│   ├── DTOs/
│   ├── Interfaces/
│   └── Behaviors/
├── Infrastructure/   # Frameworks
│   ├── Data/
│   ├── Identity/
│   └── Services/
└── API/              # Entry points
    ├── Controllers/
    ├── Middleware/
    └── Filters/
```

## Reglas de Dependencia
- Domain → No dependencias
- Application → Solo Domain
- Infrastructure → Application y Domain
- API → Application
````

**Patrones disponibles:**

| Patrón | Uso | Lenguaje |
|--------|-----|----------|
| Feature-First | Microservicios, módulos | C# |
| Component-Based | UI components | React/RN |
| DDD Layered | Clean Architecture | Multi |
