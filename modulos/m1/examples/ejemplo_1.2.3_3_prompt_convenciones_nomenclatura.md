# Prompt: Convenciones de Nomenclatura

**Contexto de uso:** Prompt que establece convenciones de nomenclatura para el proyecto.

**Prompt con convenciones:**

```
Usa estas convenciones de nomenclatura:

## Clases y Tipos
- Entidades: TareaEntity, UsuarioEntity
- DTOs: TareaCreateDto, TareaResponseDto
- Services: TareaService, ITareaService
- Repositories: TareaRepository, ITareaRepository
- Validators: CreateTareaValidator, UpdateTareaValidator

## Métodos
- CRUD: GetAll(), GetById(id), Create(dto), Update(id, dto), Delete(id)
- Queries: GetTareasPendientes(), GetTareasPorUsuario(usuarioId)
- Commands: CreateTareaCommand, CompleteTareaCommand

## Variables y Parámetros
- Singular para entidades: Tarea tarea
- Plural para colecciones: List<Tarea> tareas
- Prefijo para DTOs: createTareaDto, updateTareaDto

## Archivos
- Mismo nombre que la clase
- Una clase pública por archivo
```

**Tabla de nomenclatura:**

| Tipo | Patrón | Ejemplo |
|------|--------|---------|
| Entidad | `{Nombre}Entity` | `TareaEntity` |
| DTO Request | `{Nombre}CreateDto` | `TareaCreateDto` |
| DTO Response | `{Nombre}ResponseDto` | `TareaResponseDto` |
| Service | `I{Nombre}Service` | `ITareaService` |
| Repository | `I{Nombre}Repository` | `ITareaRepository` |
| Validator | `{Accion}{Nombre}Validator` | `CreateTareaValidator` |
