# Prompt: Invocación de test-agent

**Contexto de uso:** Ejemplo de prompt que invoca el agente de testing especializado.

**Prompt para test-agent:**

```
"@test-agent Escribe tests unitarios para la clase EmailValidator
usando xUnit y Moq, siguiendo el template en .specify/templates/test-template.md.
Cobertura requerida:
- Emails válidos
- Emails inválidos por formato
- Emails que exceden longitud
- Casos edge (null, empty, whitespace)"
```

**Cobertura de tests requerida:**

| Caso | Descripción |
|------|-------------|
| Emails válidos | Formato correcto RFC 5322 |
| Inválidos por formato | Sin @, múltiples @, etc. |
| Exceden longitud | > 254 caracteres |
| Casos edge | null, empty, whitespace |

**Referencias utilizadas:**

- Template: `.specify/templates/test-template.md`
- Framework: xUnit con Moq
