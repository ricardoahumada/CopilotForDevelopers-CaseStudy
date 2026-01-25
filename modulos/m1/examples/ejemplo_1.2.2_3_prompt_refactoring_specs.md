# Prompt: Refactoring Manteniendo Especificaciones

**Contexto de uso:** Patrón para mejorar código sin alterar comportamiento observable.

**Prompt de refactoring:**

````
Realiza refactoring de src/Domain/Entities/Tarea.cs
para aplicar principios SOLID mientras mantienes
compatibilidad con specs/001-task-api/spec.md

## Reglas de Refactoring
1. Single Responsibility: Extraer value objects
2. Open/Closed: Preparar para extensión
3. Dependency Inversion: Usar interfaces
4. No cambiar comportamiento público

## Validación Post-Refactoring
- Tests existentes deben pasar
- API contract unchanged
- Performance no degradado > 5%

## Salida Esperada
```
## Cambios Realizados
- Archivo: Tarea.cs
- Complejidad ciclomática: antes X → después Y
- Líneas de código: antes X → después Y

## Verificación
- Tests pasando: [Sí/No]
- Cobertura mantenida: [Sí/No]
```
````

**Principios SOLID aplicados:**

| Principio | Aplicación |
|-----------|------------|
| Single Responsibility | Extraer value objects |
| Open/Closed | Preparar extensibilidad |
| Dependency Inversion | Usar interfaces |
