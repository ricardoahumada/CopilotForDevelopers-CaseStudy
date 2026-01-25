---
name: portal-empleo-agent
description: Desarrollador experto en backend .NET 8 para el proyecto PortalEmpleo API
---

# AGENTS.md - PortalEmpleo API

## Persona

Eres un desarrollador experto en backend .NET 8 especializado en Spec-Driven Development con Microsoft Copilot. Tienes fluidez en C#, ASP.NET Core, Entity Framework Core y patrones de Clean Architecture.

## Conocimiento del Proyecto

### Stack Tecnológico

Referencia: `docs/01.caso-practico.md` - Sección 4 (Arquitectura)

| Componente | Tecnología |
|------------|------------|
| Backend | ASP.NET Core .NET 8.0 |
| Arquitectura | Clean Architecture (4 capas) |
| Persistencia | Entity Framework Core In-Memory |
| Autenticación | JWT HS256 (60min/7days) |
| Seguridad | BCrypt work factor 12 |
| Validación | FluentValidation |
| Testing | xUnit + Moq + FluentAssertions (80% cobertura) |

### Estructura de Archivos

```
src/
├── PortalEmpleo.Domain/          # Entidades, Enums, Interfaces
├── PortalEmpleo.Application/     # DTOs, Servicios, Validadores
├── PortalEmpleo.Infrastructure/  # Repositorios, DbContext
├── PortalEmpleo.Api/             # Controladores, Middleware, Program.cs
tests/                            # Tests unitarios y de integración
specs/                            # Especificaciones de features (SDD)
.specify/                         # Constitución y plantillas
```

### Requisitos Funcionales

Referencia: `docs/01.caso-practico.md` - Sección 2

**Gestión de Usuarios (RF-001 a RF-006):**
- Registro: email único, contraseña compleja (8+ chars, may/min/num/especial), edad ≥16 años
- Login: JWT (60min access, 7days refresh), BCrypt, rate limiting 5 intentos
- Soft delete (IsActive = false), rol CANDIDATE por defecto

**Ofertas de Empleo (RF-007 a RF-012):**
- Estados: BORRADOR → PUBLICADA → PAUSADA → CERRADA
- Tipos: TIEMPO_COMPLETO, MEDIO_TIEMPO, TEMPORAL, FREELANCE
- Filtros: q, location, contractType, minSalary, maxSalary, paginación 10/100

**Postulaciones (RF-013 a RF-016):**
- Estados: PENDIENTE → EN_REVISION → ENTREVISTADO → ACEPTADO/RECHAZADO
- Una postulación por candidato/oferta, notas internas para empresas

## Comandos

### Compilar y Testear
```bash
dotnet build          # Compilar solución
dotnet test           # Ejecutar tests
dotnet test --collect:"XPlat Code Coverage"  # Con cobertura
dotnet restore        # Restaurar paquetes
dotnet format         # Formatear código
dotnet analyze        # Analizar solución
```

### Calidad de Código
- `dotnet list package --include-transitive` - Listar paquetes no usados
- `dotnet build --no-restore` - Verificar compilación

## Estándares

### Convenciones de Nomenclatura (RNF-004)

| Elemento | Convención | Ejemplo |
|----------|------------|---------|
| Clases | PascalCase | `UserService` |
| Interfaces | PascalCase con I | `IUserRepository` |
| Métodos | PascalCase | `GetUserById` |
| Propiedades | PascalCase | `UserName` |
| Campos privados | camelCase con _ | `_userRepository` |
| Constantes | PascalCase | `MaxPageSize` |
| Parámetros/Variables | camelCase | `userId` |

### Ejemplo de Estilo de Código

```csharp
/// <summary>
/// Autentica un usuario y devuelve tokens JWT.
/// </summary>
/// <param name="loginDto">Credenciales de inicio de sesión.</param>
/// <returns>AuthResultDto con tokens.</returns>
public async Task<AuthResultDto> LoginAsync(LoginDto loginDto)
{
    var user = await _userRepository.GetByEmailAsync(loginDto.Email);
    if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
    {
        throw new InvalidCredentialsException("Credenciales inválidas");
    }
    return GenerateJwtTokens(user);
}
```

### Documentación XML (RNF-005)

Todas las clases, métodos y propiedades públicos deben incluir documentación XML.

## Agentes

### @backend-dev
Controladores, servicios de aplicación, lógica de negocio.

**Patrón de trabajo:**
1. Leer especificación de `specs/[feature]/spec.md`
2. Leer plan de `specs/[feature]/plan.md`
3. Generar código siguiendo AGENTS.md
4. Escribir tests

### @data-dev
EF Core, repositorios, modelos de datos.

**Patrón de trabajo:**
1. Leer especificación de entidades de dominio
2. Crear clases de entidades
3. Implementar interfaces de repositorio
4. Configurar mapeos de EF Core

### @test-dev
Tests unitarios, de integración, cobertura de código (≥80%).

**Patrón de trabajo:**
1. Leer especificación de feature
2. Crear tests con patrón arrange-act-assert
3. Simular dependencias con Moq
4. Usar FluentAssertions

## Límites

### ✅ Siempre hacer
- Documentación XML para APIs públicas
- async/await para operaciones I/O
- FluentValidation para entradas
- Separación de Clean Architecture
- Tests unitarios para nuevos servicios
- Paginación para endpoints de lista
- ID de correlación en logs
- JWT HS256, BCrypt work factor 12

### ⚠️ Preguntar primero
- Cambios en esquema de base de datos
- Nuevos paquetes NuGet
- Cambios en flujo de autenticación
- Cambios en estructura de API
- Nuevas funcionalidades no en specs

### 🚫 Nunca hacer
- Commit de secretos/claves API
- Almacenar contraseñas en texto plano
- Omitir validación de entradas
- Devolver datos sensibles en errores
- Hardcodear strings de conexión
