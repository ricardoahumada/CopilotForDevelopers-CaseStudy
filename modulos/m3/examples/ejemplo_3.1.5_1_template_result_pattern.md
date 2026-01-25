# Template: Result Pattern

**Contexto de uso:** Este template define el patrón Result para gestión de errores en operaciones que pueden éxito o fallo.

**Template Spec Kit:**

```markdown
# Template: Result Pattern

## Cuándo usar
Cuando una operación puede成功 (éxito) o失敗 (fallo) de forma recoverable.

## Formato
```csharp
public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; private set; }
    
    private Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value) => 
        new(true, value, null);
    
    public static Result<T> Failure(string error) => 
        new(false, default, error);
    
    public TResult Match<TResult>(
        Func<T, TResult> onSuccess,
        Func<string, TResult> onFailure)
        => IsSuccess ? onSuccess(Value!) : onFailure(Error!);
}
```

## Convenciones
- Namespace: Application.Common.Results
- Error: solo getter, seteado en constructor privado
```

**Integración con Spec Kit:**

```bash
# 1. Seleccionar template
specs/001-auth-api/code-prompts.md → Template: result-pattern-template.md

# 2. Personalizar para el dominio
# Cambiar T por el tipo específico (Usuario, Token, etc.)

# 3. Generar código
@backend-dev Implementa Result<Usuario> siguiendo result-pattern-template.md
```

**Ejemplo de uso:**

```csharp
// Result<Usuario> para operaciones de autenticación
public class AuthResult : Result<Usuario>
{
    public static AuthResult Success(Usuario usuario) => 
        new(true, usuario, null);
    
    public static AuthResult Failure(string error) => 
        new(false, null, error);
}

// Usage
var result = await _authService.LoginAsync(request, ct);

return result.Match(
    onSuccess: usuario => Ok(MapToDto(usuario)),
    onFailure: error => BadRequest(new { error })
);
```
