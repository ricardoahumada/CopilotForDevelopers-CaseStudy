# Prompt para Architecture Decision Records

**Contexto de uso:** Este prompt genera ADRs para decisiones de arquitectura del proyecto.

**Prompt completo:**
```
@docs-dev Genera ADR para decisiones de arquitectura:

```markdown
# ADR-001: Uso de JWT para Autenticación

## Estado
**Aceptado** | Propuesto | Deprecated

## Contexto
Necesitamos elegir un mecanismo de autenticación para la API de PortalEmpleo.

## Decisión
Usaremos JWT para autenticación stateless.

### Factores de Decisión
1. **Escalabilidad**: JWT es stateless, no requiere almacenamiento de sesión
2. **Performance**: Eliminamos round-trips a base de datos para validación
3. **Mobile support**: JWT funciona bien con apps móviles

## Consecuencias

### Positivas
- No se requiere servidor de sesiones
- Validación de tokens muy rápida

### Negativas
- Tokens no pueden revocarse inmediatamente
- Requires careful token lifetime management

## Implementación
```json
{
  "Jwt": {
    "SecretKey": "min-32-char-secret-key",
    "Issuer": "PortalEmpleo",
    "Audience": "PortalEmpleo-API",
    "AccessTokenExpirationMinutes": 30
  }
}
```

## Referencias
- [JWT.io](https://jwt.io/)
- [OWASP JWT Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/JSON_Web_Token_for_Python_Cheat_Sheet.html)
```
```
