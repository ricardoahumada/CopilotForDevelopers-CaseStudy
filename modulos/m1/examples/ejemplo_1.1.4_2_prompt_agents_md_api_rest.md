# Prompt: Generar AGENTS.md Básico para API REST

**Contexto de uso:** Prompt para generar un archivo AGENTS.md para un proyecto de API REST con Clean Architecture.

**Prompt:**

```
"Genera un archivo AGENTS.md para un proyecto de API REST con las siguientes características:
- Stack: C# con ASP.NET Core 8, Entity Framework Core, SQL Server
- Patrón arquitectónico: Clean Architecture
- Estructura de carpetas: src/Domain, src/Application, src/Infrastructure, src/API
- Convenciones de código: Microsoft C# Coding Conventions
- Testing: xUnit con Moq
- Incluya secciones de Persona, Comandos, Estructura del Proyecto, Estándares de Código y Límites"
```

**Secciones esperadas en el resultado:**

| Sección | Contenido |
|---------|-----------|
| Persona | Descripción del rol del agente |
| Comandos | dotnet build, dotnet test, etc. |
| Estructura | Descripción de carpetas src/ |
| Estándares | Convenciones Microsoft C# |
| Límites | Always/Ask/Never boundaries |
