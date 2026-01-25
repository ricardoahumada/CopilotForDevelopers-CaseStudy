# Prompt: Generar ADR (Architecture Decision Record)

**Contexto de uso:** Prompt para generar documentos ADR que capturan decisiones arquitectónicas importantes.

**Prompt para generar ADR:**

````
Genera un ADR para la decisión de arquitectura de datos en PortalEmpleo:

## Decisión a Documentar
- Usar Entity Framework Core con SQL Server como ORM primario
- Implementar Repository Pattern con Unit of Work

## Contexto
- Proyecto existente con legacy code en ADO.NET
- Equipo familiarizado con Entity Framework
- Requisito de migración gradual desde sistema legacy
- Necesidad de tests unitarios con mockable repositories

## Opciones Consideradas
1. Continue with ADO.NET (status quo)
2. Dapper + repositories
3. Entity Framework Core + repositories
4. Raw SQL with micro-ORM

## Criterios de Evaluación
- Curva de aprendizaje del equipo
- Testabilidad del código
- Mantenibilidad a largo plazo
- Performance para escenarios de lectura intensiva

## Decisión Seleccionada
Entity Framework Core + repositories

## Justificación
- Team familiarity: 2+ años experiencia EF
- LINQ queries simplifican mantenimiento
- Built-in migration support
- Patrón Repository permite abstracción gradual

## Implicaciones
- Posible overhead en escenarios muy específicos
- Require migration strategy de datos existentes
- Training needed para junior developers

## Formato ADR
```markdown
# ADR-001: Entity Framework Core como ORM Primario

## Estado
Aceptado

## Contexto
[Descripción del problema]

## Decisión
[Descripción de la decisión]

## Consecuencias
### Positivas
- 
### Negativas
-
```
````

**Dónde guardar:** `docs/adr/ADR-001-entity-framework-core.md`
