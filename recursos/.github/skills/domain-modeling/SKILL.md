---
name: domain-modeling
description: Generates domain entities and enums for PortalEmpleo following Clean Architecture principles. Use when creating domain models, entity classes, or enum definitions for the job portal application.
license: MIT
metadata:
  author: course-team@netmind.es
  version: "1.0.0"
compatibility: Requires .NET 8 SDK and C# development environment
allowed-tools: Read Write Bash(git:*) Bash(dotnet:*)
---

# Domain Modeling Skill - PortalEmpleo

## When to use this skill

Use this skill when creating domain entities and enums for PortalEmpleo. This skill is automatically triggered when the user mentions:
- Creating entity classes
- Domain models
- Enum definitions
- Clean Architecture domain layer

## Project Context

- **Architecture:** Clean Architecture (Domain layer)
- **Entity Base:** Standard C# class with Id
- **Enums:** Used for status and type fields
- **Convention:** PascalCase for enums, camelCase for properties

## Entity Standard

### Base Entity Structure

```csharp
namespace PortalEmpleo.Domain.Entities;

/// <summary>
/// Entidad base con identificador y metadatos
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### User Entity

```csharp
namespace PortalEmpleo.Domain.Entities;

/// <summary>
/// Usuario del sistema PortalEmpleo
/// </summary>
public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? Phone { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

### JobOffer Entity

```csharp
namespace PortalEmpleo.Domain.Entities;

/// <summary>
/// Oferta de empleo publicada por una empresa
/// </summary>
public class JobOffer : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public decimal? Salary { get; set; }
    public ContractType ContractType { get; set; }
    public JobOfferStatus Status { get; set; } = JobOfferStatus.Borrador;
    public Guid CompanyId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

### Application Entity

```csharp
namespace PortalEmpleo.Domain.Entities;

/// <summary>
/// Postulación de candidato a oferta de empleo
/// </summary>
public class Application : BaseEntity
{
    public Guid JobOfferId { get; set; }
    public Guid CandidateId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pendiente;
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
```

## Enum Standards

### UserRole Enum

```csharp
namespace PortalEmpleo.Domain.Enums;

/// <summary>
/// Roles de usuario en el sistema
/// </summary>
public enum UserRole
{
    Candidate = 0,
    Company = 1,
    Admin = 2
}
```

### JobOfferStatus Enum

```csharp
namespace PortalEmpleo.Domain.Enums;

/// <summary>
/// Estados del ciclo de vida de una oferta
/// </summary>
public enum JobOfferStatus
{
    Borrador = 0,
    Publicada = 1,
    Pausada = 2,
    Cerrada = 3
}
```

### ContractType Enum

```csharp
namespace PortalEmpleo.Domain.Enums;

/// <summary>
/// Tipos de contrato disponibles
/// </summary>
public enum ContractType
{
    TiempoCompleto = 0,
    MedioTiempo = 1,
    Temporal = 2,
    Freelance = 3
}
```

### ApplicationStatus Enum

```csharp
namespace PortalEmpleo.Domain.Enums;

/// <summary>
/// Estados de una postulación
/// </summary>
public enum ApplicationStatus
{
    Pendiente = 0,
    EnRevision = 1,
    Entrevistado = 2,
    Aceptado = 3,
    Rechazado = 4
}
```

## Required Elements

### 1. Entity Class
- Inherit from BaseEntity
- XML documentation with `<summary>`
- Public properties with types
- Default values where appropriate
- Navigation properties (if needed)

### 2. Enums
- Public enum declaration
- XML documentation
- Descriptive member names
- Integer values for database mapping

### 3. Conventions
- PascalCase for class and enum names
- camelCase for properties
- String.Empty for default strings
- Guid for Id (primary key)
- DateTime.UtcNow for CreatedAt

## Output Format

Generate:
- Entity classes in Domain/Entities/
- Enums in Domain/Enums/
- Complete C# files with XML documentation

## Files to Write

- `src/PortalEmpleo.Domain/Entities/User.cs`
- `src/PortalEmpleo.Domain/Entities/JobOffer.cs`
- `src/PortalEmpleo.Domain/Entities/Application.cs`
- `src/PortalEmpleo.Domain/Enums/UserRole.cs`
- `src/PortalEmpleo.Domain/Enums/JobOfferStatus.cs`
- `src/PortalEmpleo.Domain/Enums/ContractType.cs`
- `src/PortalEmpleo.Domain/Enums/ApplicationStatus.cs`
