# Prompt para Extract Method Refactoring

**Contexto de uso:** Este prompt realiza extract method refactoring en AuthService para reducir complejidad.

**Prompt completo:**
```
@agent Realiza extract method refactoring en AuthService:

## Código Original (60+ líneas)
```csharp
public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto dto, CancellationToken ct)
{
    var usuario = await _repo.GetByEmailAsync(dto.Email, ct);
    if (usuario == null) return Result<LoginResponseDto>.Failure("Invalid");
    if (!usuario.EstaActivo) return Result<LoginResponseDto>.Failure("Inactive");
    if (!VerifyPassword(dto.Password, usuario.PasswordHash)) return Result<LoginResponseDto>.Failure("Invalid");
    var token = _tokenService.GenerateAccessToken(usuario);
    var refreshToken = await _tokenService.GenerateRefreshTokenAsync(usuario.Id, ct);
    usuario.UltimoLogin = DateTime.UtcNow;
    await _repo.UpdateAsync(usuario, ct);
    _logger.LogInformation("User logged in", usuario.Email);
    return Result<LoginResponseDto>.Success(new LoginResponseDto { AccessToken = token, RefreshToken = refreshToken.Token });
}
```

## Refactoring Aplicado
```csharp
public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto dto, CancellationToken ct)
{
    var usuarioResult = await ValidateAndGetUserAsync(dto.Email, ct);
    if (usuarioResult.IsFailure) return Result<LoginResponseDto>.Failure(usuarioResult.Error);
    
    var tokenResult = await GenerateTokensForUserAsync(usuarioResult.Value, ct);
    await UpdateUserLoginAsync(usuarioResult.Value, ct);
    LogLoginSuccess(usuarioResult.Value.Email);
    return tokenResult;
}

private async Task<Result<Usuario>> ValidateAndGetUserAsync(string email, CancellationToken ct)
{
    var usuario = await _repo.GetByEmailAsync(email, ct);
    if (usuario == null) return Result<Usuario>.Failure("Invalid");
    if (!usuario.EstaActivo) return Result<Usuario>.Failure("Inactive");
    return usuario;
}
```
```
