# Prompt: Documentar Convenciones de Nomenclatura

**Contexto de uso:** Prompt para generar documento completo de convenciones de nomenclatura por lenguaje.

**Prompt para documentar convenciones de nomenclatura:**

````
Genera documento de convenciones de nomenclatura para PortalEmpleo:

## Lenguajes
- C# (Backend)
- TypeScript (Frontend/Mobile)
- Python (Data/ML)
- SQL (Database)

## C# Naming Conventions

### Clases y Tipos
```csharp
// Entidades: Entity suffix
public class TareaEntity { }

// DTOs: Purpose + DTO suffix
public class TareaCreateDto { }
public class TareaResponseDto { }

// Commands/Queries (MediatR)
public record CreateTareaCommand : IRequest<TareaResponseDto> { }
public record GetTareasQuery : IRequest<List<TareaResponseDto>> { }

// Services: Interface + Implementation
public interface ITareaService { }
public class TareaService : ITareaService { }

// Validators: Entity + Validator suffix
public class CreateTareaValidator : AbstractValidator<CreateTareaDto> { }

// Exceptions: Entity + Exception suffix
public class TareaNotFoundException : NotFoundException { }
```

### Métodos
```csharp
// CRUD: estándar
Tarea GetById(Guid id);
List<Tarea> GetAll();
Tarea Create(TareaCreateDto dto);
Tarea Update(Guid id, TareaUpdateDto dto);
bool Delete(Guid id);

// Queries: Get + Entity + Condition
Tarea GetById(Guid id);
List<Tarea> GetByStatus(TareaStatus status);
List<Tarea> GetByAssignee(Guid usuarioId);

// Commands: Verb + Entity + Context
CreateTareaCommand CreateTarea(CreateTareaDto dto);
CompleteTareaCommand CompleteTarea(CompleteTareaDto dto);
```

### Variables y Parámetros
```csharp
// Singulares para entidades
Tarea tarea = new Tarea();

// Plural para colecciones
List<Tarea> tareas = await repository.GetAll();

// Prefijo para DTOs de input
TareaCreateDto createTareaDto = request.CreateTareaDto;

// Parámetros descriptivos
public async Task<Tarea> GetTareaById(Guid tareaId, CancellationToken ct) { }
```

## TypeScript Naming Conventions

### Componentes: PascalCase
```typescript
// Componentes React
TaskListScreen.tsx
TaskCardComponent.tsx
```

### Hooks: camelCase con prefijo use
```typescript
useTasks.ts
useAuth.ts
useTaskFiltering.ts
```

### Servicios: PascalCase con sufijo
```typescript
TasksApiService.ts
AuthService.ts
```

### Interfaces: PascalCase con prefijo I
```typescript
ITarea.ts
ITaskRepository.ts
```
````

**Dónde guardar:** `.specify/memory/constitution.md` (sección Coding Standards)
