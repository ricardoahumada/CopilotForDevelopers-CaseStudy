# Prompt: Configurar Generación Automática de Esqueletos en AGENTS.md

**Contexto de uso:** Prompt para configurar AGENTS.md para que genere automáticamente esqueletos de features.

**Prompt para configurar generación automática:**

````
@agent Configura AGENTS.md para generación automática de esqueletos:

## Requerimiento
Cuando el agente detecte creación de nuevo feature,
deberá generar automáticamente la estructura completa.

## Patrón de Detección
- Directorio nuevo en specs/
- Nuevo spec.md con feature name
- Trigger: creación de primer archivo en src/[Feature]/

## Template a Usar
- Template: .specify/templates/feature-skeleton.md
- Ubicación: src/[FeatureName]/
- Archivos: controller, service, repository, dto, validator, tests

## Configuración AGENTS.md Adicional
## Auto-Skeleton Generation

When creating a new feature from specs/[feature-name]/spec.md:
1. Read the feature specification
2. Extract domain entities and endpoints
3. Generate feature skeleton using template
4. Create corresponding tests

Command: 
```bash
dotnet new feature-skeleton -n FeatureName -o src/Features/FeatureName```

## Feature Skeleton Template Location
.specify/templates/feature-skeleton/
├── controller-template.cs
├── service-template.cs
├── repository-template.cs
├── dto-template.cs
├── validator-template.cs
└── test-template.cs

````

**Archivos generados automáticamente:**

| Archivo | Propósito |
|---------|-----------|
| `controller-template.cs` | Controller base |
| `service-template.cs` | Service con interface |
| `repository-template.cs` | Repository pattern |
| `dto-template.cs` | DTOs request/response |
| `validator-template.cs` | FluentValidation |
| `test-template.cs` | Tests unitarios |
