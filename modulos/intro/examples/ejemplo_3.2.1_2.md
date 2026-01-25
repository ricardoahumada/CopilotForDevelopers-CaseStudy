# Especificación de API - Gestión de Usuarios

**Contexto de uso:** Este ejemplo muestra una especificación de API escrita manualmente, definiendo el modelo de datos, endpoints y contratos de request/response.

**Especificación:**
```markdown
# API de Gestion de Usuarios

## Base URL
/api/v1/users

## Modelo de Datos

### User
```json
{
  "id": "uuid",
  "email": "string(email)",
  "name": "string",
  "role": "admin | customer | vendor",
  "createdAt": "ISO8601",
  "isActive": "boolean"
}
```

## Endpoints

### Crear Usuario
```http
POST /users
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "name": "Nombre Usuario",
  "role": "customer"
}
```

### Respuestas
- 201: Usuario creado exitosamente
- 400: Error de validacion
- 409: Email ya existe
```

**Componentes de la especificación:**

| Componente | Descripción | Ejemplo |
|------------|-------------|---------|
| **Base URL** | Prefijo de todos los endpoints | /api/v1/users |
| **Modelo de datos** | Schema de entidades | User con id, email, name, role |
| **Endpoints** | Definición de rutas | POST /users |
| **Requests** | Formato de datos de entrada | JSON con email, name, role |
| **Responses** | Códigos y formatos de salida | 201 Created, 400 Bad Request |

**Lección aprendida:** Las especificaciones de API detalladas permiten que los agentes de IA generen implementaciones consistentes con los contratos definidos, reduciendo errores de integración.
