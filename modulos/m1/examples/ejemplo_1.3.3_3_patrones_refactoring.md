# Referencia: Patrones de Refactoring Documentados

**Contexto de uso:** Patrones de refactoring documentados para uso consistente en el equipo.

**Archivo:** `.specify/memory/refactoring-patterns.md`

**Patrones de refactoring documentados:**

```markdown
## Refactoring Patterns

### Extract Method
**Cuándo usar:** Método > 20 líneas o múltiples responsabilidades
**Proceso:**
1. Identificar bloque lógico
2. Crear método con nombre descriptivo
3. Mover bloque al nuevo método
4. Reemplazar con llamada
5. Verificar tests

### Replace Conditional with Polymorphism
**Cuándo usar:** Switch/if-else sobre tipo en múltiples lugares
**Proceso:**
1. Identificar jerarquía de tipos
2. Crear interfaz/base class
3. Mover comportamiento a implementaciones
4. Reemplazar condicionales con polimorfismo

### Introduce Parameter Object
**Cuándo usar:** Parámetros relacionados > 3
**Proceso:**
1. Identificar parámetros relacionados
2. Crear clase/record para parámetros
3. Actualizar firma de método
4. Actualizar llamadores
```

**Patrones incluidos:**

| Patrón | Cuándo usar |
|--------|-------------|
| Extract Method | Método > 20 líneas |
| Replace Conditional | Switch/if-else repetidos |
| Introduce Parameter Object | > 3 parámetros relacionados |
