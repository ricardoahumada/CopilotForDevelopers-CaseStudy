# Prompt para Matriz de Trazabilidad

**Contexto de uso:** Este prompt genera matriz de trazabilidad entre criterios de aceptación y código implementado.

**Prompt completo:**
```
@agent Genera matriz de trazabilidad para specs/001-auth-api/spec.md:

## Criterios de Aceptación
| ID | Descripción |
|----|-------------|
| CA-AUTH-001 | Login con credenciales válidas retorna JWT |
| CA-AUTH-002 | Password incorrecto retorna 401 |
| CA-AUTH-003 | Token expira en tiempo configurado |
| CA-AUTH-004 | Refresh token renueva access token |
| CA-AUTH-005 | Usuario inactivo no puede autenticarse |

## Implementación Actual
```
## Matriz de Trazabilidad

### CA-AUTH-001: Login con credenciales válidas
- Archivo: AuthService.cs:45-78
- Test: AuthServiceTests.Login_ValidCredentials_ReturnsToken
- Estado: ✓ Implementado y testeado

### CA-AUTH-002: Password incorrecto
- Archivo: AuthService.cs:52-55
- Test: AuthServiceTests.Login_InvalidPassword_Returns401
- Estado: ✓ Implementado y testeado

### CA-AUTH-003: Token expira
- Archivo: TokenService.cs:23-45
- Config: appsettings.json:Jwt:AccessTokenExpirationMinutes
- Test: TokenServiceTests.GeneratedToken_ContainsExpiration
- Estado: ✓ Implementado y testeado

### CA-AUTH-004: Refresh token
- Archivo: AuthService.cs:80-112
- Archivo: RefreshTokenService.cs
- Test: AuthServiceTests.RefreshToken_RenewsAccessToken
- Estado: ✓ Implementado y testeado

### CA-AUTH-005: Usuario inactivo
- Archivo: AuthService.cs:57-61
- Test: AuthServiceTests.Login_InactiveUser_Returns403
- Estado: ✓ Implementado y testeado

## Cobertura
- Total criterios: 5
- Implementados: 5 (100%)
- Testeados: 5 (100%)
- Coverage: 92%
```
```
