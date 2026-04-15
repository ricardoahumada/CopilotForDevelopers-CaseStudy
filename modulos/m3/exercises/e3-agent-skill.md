Crea un agent skill `test-quality-security` para el proyecto PortalEmpleo API.

- Lee a9.agent-skills.md para entender la estructura de un skill.
- El skill debe guiar la generación de tests unitarios específicos para las reglas de seguridad del caso práctico (01.caso-practico.md): JWT, BCrypt, brute-force, soft delete, autorización por rol y validación de entrada.
- Valida el skill invocándolo para generar los tests del `AuthService`: login correcto, password incorrecta, cuenta bloqueada tras 5 intentos y token expirado.


### Qué debería cubrir:

| Área | Escenarios concretos |
|------|----------------------|
| **JWT** | Token expirado, token con firma inválida, claims faltantes, token de refresh usado como access |
| **BCrypt** | Password hasheada en DB, login con password correcta, login con password incorrecta, nunca exponer hash |
| **Autorización** | CANDIDATE no puede publicar ofertas, COMPANY no puede ver postulaciones ajenas, ADMIN puede todo |
| **Brute force** | 5 intentos → cuenta bloqueada, intento 6 rechazado aunque sea correcto |
| **Validación entrada** | Email inválido, password sin mayúscula, longitud excedida, campos obligatorios vacíos |
| **Soft delete** | Usuario `IsActive=false` no puede login, no aparece en listados |
| **Paginación** | `pageSize > 100` → limitado a 100, `page 0` → error |

