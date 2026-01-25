# Template: Extract Method Refactoring

**Contexto de uso:** Este template define el patrón de refactoring para extraer métodos largos en métodos más pequeños.

**Template Spec Kit:**

```markdown
# Template: Extract Method Refactoring

## Cuándo usar
Métodos > 30 líneas o múltiples responsabilidades.

## Patrón de Refactoring
```csharp
// ORIGINAL (40+ líneas)
public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto dto, CancellationToken ct)
{
    // Validación (10 líneas)
    // Extracción de usuario (8 líneas)
    // Verificación de password (5 líneas)
    // Generación de tokens (10 líneas)
    // Actualización de usuario (5 líneas)
    // Logging (3 líneas)
}

// REFACTORIZADO
public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto dto, CancellationToken ct)
{
    var usuarioResult = await ValidateAndGetUserAsync(dto.Email, ct);
    if (usuarioResult.IsFailure) return Result<LoginResponseDto>.Failure(usuarioResult.Error);
    
    var tokenResult = await GenerateTokensForUserAsync(usuarioResult.Value, ct);
    await UpdateUserLoginAsync(usuarioResult.Value, ct);
    LogLoginSuccess(usuarioResult.Value.Email);
    return tokenResult;
}

private async Task<Result<Usuario>> ValidateAndGetUserAsync(string email, CancellationToken ct) { /* ... */ }
private async Task<Result<LoginResponseDto>> GenerateTokensForUserAsync(Usuario usuario, CancellationToken ct) { /* ... */ }
private async Task UpdateUserLoginAsync(Usuario usuario, CancellationToken ct) { /* ... */ }
private void LogLoginSuccess(string email) { /* ... */ }
```

## Métricas Objetivo
- Complejidad ciclomática: < 15
- Líneas por método: < 25
- Responsabilidad única: 1 método = 1 tarea
```

**Integración con Spec Kit:**

```bash
# 1. Analizar código actual
/specKit.analyze --complexity --output complexity-report.json

# 2. Identificar métodos problemáticos
# complexity-report.json → AuthService.LoginAsync: Complexity 42

# 3. Aplicar template de refactoring
@backend-dev Aplica template extract-method-template.md
             a AuthService.LoginAsync
             según specs/001-auth-api/refactoring-prompts.md

# 4. Validar refactoring
/specKit.analyze --complexity --compare complexity-report.json
```

**Resultado generado:**

```csharp
// src/Application/Services/AuthService.cs (refactorizado)
public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto dto, CancellationToken ct)
{
    var usuarioResult = await ValidateAndGetUserAsync(dto.Email, dto.Password, ct);
    if (usuarioResult.IsFailure) 
        return Result<LoginResponseDto>.Failure(usuarioResult.Error);
    
    var tokenResult = await GenerateTokensForUserAsync(usuarioResult.Value, ct);
    await UpdateUserLoginAsync(usuarioResult.Value, ct);
    LogLoginSuccess(usuarioResult.Value.Email);
    return tokenResult;
}

private async Task<Result<Usuario>> ValidateAndGetUserAsync(string email, string password, CancellationToken ct)
{
    var usuario = await _repo.GetByEmailAsync(email, ct);
    if (usuario == null) 
        return Result<Usuario>.Failure("Credenciales inválidas");
    if (!usuario.EstaActivo) 
        return Result<Usuario>.Failure("Usuario inactivo");
    if (!VerifyPassword(password, usuario.PasswordHash)) 
        return Result<Usuario>.Failure("Credenciales inválidas");
    return usuario;
}

private async Task<Result<LoginResponseDto>> GenerateTokensForUserAsync(Usuario usuario, CancellationToken ct)
{
    var accessToken = _tokenService.GenerateAccessToken(usuario);
    var refreshToken = await _tokenService.GenerateRefreshTokenAsync(usuario.Id, ct);
    return Result<LoginResponseDto>.Success(new LoginResponseDto 
    { 
        AccessToken = accessToken, 
        RefreshToken = refreshToken.Token 
    });
}

private async Task UpdateUserLoginAsync(Usuario usuario, CancellationToken ct)
{
    usuario.UltimoLogin = DateTime.UtcNow;
    await _repo.UpdateAsync(usuario, ct);
}

private void LogLoginSuccess(string email)
{
    _logger.LogInformation("Usuario autenticado: {Email}", email);
}
```

**Métricas de mejora:**
| Métrica | Antes | Después |
|---------|-------|---------|
| Complejidad ciclomática | 42 | 8 |
| Líneas de código | 45 | 12 |
| Número de métodos | 1 | 5 |
