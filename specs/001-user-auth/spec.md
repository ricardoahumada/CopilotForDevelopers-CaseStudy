# Feature Specification: Gestión de Usuarios y Autenticación

**Feature Branch**: `001-user-auth`  
**Created**: 2026-02-04  
**Status**: Draft  
**Input**: User description: "Gestión de usuarios y autenticación - Sistema completo de registro, login, gestión de perfiles y tokens JWT"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Registro de Candidatos (Priority: P1)

Como profesional en búsqueda de empleo, quiero poder crear una cuenta en el Portal de Empleo proporcionando mi información básica, para poder acceder a las ofertas laborales disponibles y gestionar mi perfil profesional.

**Why this priority**: Es la funcionalidad más crítica del sistema. Sin usuarios registrados no existe base de usuarios para ninguna otra funcionalidad. Un MVP viable debe permitir que candidatos se registren exitosamente con información validada.

**Independent Test**: Puede probarse completamente intentando registrar un nuevo usuario con credenciales válidas (email único, contraseña fuerte, nombre, fecha de nacimiento válida, teléfono) y verificando que la cuenta se crea exitosamente con rol CANDIDATE asignado por defecto.

**Acceptance Scenarios**:

1. **Given** no existe cuenta con email "juan@example.com", **When** proporciono datos válidos (email, contraseña segura "P@ssw0rd123", nombre "Juan Pérez", fecha nacimiento "1990-05-15", teléfono "+34600123456"), **Then** mi cuenta se crea exitosamente con rol CANDIDATE
2. **Given** ya existe cuenta con email "maria@example.com", **When** intento registrarme con ese mismo email, **Then** recibo mensaje de error "El email ya está registrado en el sistema"
3. **Given** proporciono contraseña débil "12345", **When** intento registrarme, **Then** recibo error detallando requisitos (mín 8 caracteres, mayúscula, minúscula, número, carácter especial)
4. **Given** mi fecha de nacimiento indica que tengo 15 años, **When** intento registrarme, **Then** recibo error "Debes tener al menos 16 años para registrarte"
5. **Given** proporciono email con formato inválido "juan.com", **When** intento registrarme, **Then** recibo error "Formato de email inválido"
6. **Given** proporciono teléfono sin formato internacional "600123456", **When** intento registrarme, **Then** recibo error "El teléfono debe estar en formato internacional E.164 (ej: +34600123456)"

---

### User Story 2 - Inicio de Sesión Seguro con JWT (Priority: P1)

Como usuario registrado, quiero iniciar sesión de forma segura con mi email y contraseña para recibir tokens de autenticación que me permitan acceder a las funcionalidades protegidas del portal durante mi sesión de trabajo.

**Why this priority**: Sin autenticación no hay seguridad, personalización ni control de acceso. Es absolutamente fundamental para que los usuarios puedan acceder a sus datos y funcionalidades personalizadas.

**Independent Test**: Puede probarse completamente iniciando sesión con credenciales válidas y verificando que se reciben dos tokens JWT: access token (válido 60 minutos) y refresh token (válido 7 días), permitiendo acceso a endpoints protegidos.

**Acceptance Scenarios**:

1. **Given** tengo cuenta activa con email "ana@example.com" y contraseña "SecureP@ss123", **When** inicio sesión con credenciales correctas, **Then** recibo access token (60 min) y refresh token (7 días) en la respuesta
2. **Given** proporciono email correcto pero contraseña incorrecta, **When** intento iniciar sesión, **Then** recibo error genérico "Credenciales inválidas" sin especificar qué campo es incorrecto
3. **Given** he fallado 4 intentos de login consecutivos sin login exitoso intermedio, **When** fallo el 5º intento consecutivo, **Then** mi cuenta se bloquea temporalmente por 15 minutos
4. **Given** he fallado 3 intentos y luego tengo login exitoso, **When** posteriormente fallo 1 intento, **Then** el contador se reinicia desde 1 (no desde 4)
5. **Given** mi cuenta fue marcada como inactiva (soft delete), **When** intento iniciar sesión con credenciales válidas, **Then** recibo error "Cuenta desactivada, contacta al administrador"
6. **Given** tengo cuenta bloqueada por intentos fallidos, **When** espero 15 minutos e intento nuevamente, **Then** puedo iniciar sesión exitosamente si las credenciales son correctas y el contador se reinicia a 0

---

### User Story 3 - Visualización de Perfil Personal (Priority: P2)

Como candidato autenticado, quiero poder consultar mi información personal actual en el sistema para verificar que mis datos están correctos y actualizados.

**Why this priority**: Una vez que los usuarios pueden autenticarse, necesitan visualizar su información para confirmar sus datos y decidir si necesitan actualizaciones. Es la base para la gestión de perfil.

**Independent Test**: Puede probarse completamente autenticándose y accediendo al endpoint de perfil propio, verificando que se devuelve toda la información personal del usuario sin datos sensibles como contraseñas.

**Acceptance Scenarios**:

1. **Given** estoy autenticado como candidato, **When** accedo a mi perfil en /api/v1/users/me, **Then** veo mi email, nombre, fecha de nacimiento, teléfono, habilidades y ubicación (sin contraseña)
2. **Given** soy administrador autenticado, **When** accedo a /api/v1/users/{userId} de otro usuario, **Then** puedo ver el perfil completo de ese usuario
3. **Given** soy candidato sin rol admin, **When** intento acceder a /api/v1/users/{otroUserId}, **Then** recibo error 403 Forbidden
4. **Given** no estoy autenticado, **When** intento acceder a /api/v1/users/me, **Then** recibo error 401 Unauthorized

---

### User Story 4 - Actualización de Perfil Personal (Priority: P2)

Como candidato autenticado, quiero poder actualizar mi información personal (nombre, teléfono, habilidades, ubicación) para mantener mi perfil profesional actualizado y relevante para las empresas.

**Why this priority**: Después de poder ver su perfil, los usuarios necesitan poder actualizarlo. Es esencial para mantener información precisa y mejorar su matching con ofertas laborales.

**Independent Test**: Puede probarse completamente actualizando campos modificables del perfil y verificando que los cambios se persisten correctamente mientras el email permanece inmutable.

**Acceptance Scenarios**:

1. **Given** estoy autenticado, **When** actualizo mi nombre de "Juan Pérez" a "Juan Carlos Pérez", **Then** el cambio se guarda y se refleja en próximas consultas
2. **Given** estoy autenticado, **When** actualizo mi lista de habilidades agregando "Python, Docker", **Then** las nuevas habilidades se añaden a mi perfil
3. **Given** estoy autenticado, **When** intento cambiar mi email de "juan@example.com" a "nuevo@example.com", **Then** recibo error "El email no puede ser modificado"
4. **Given** proporciono datos parcialmente válidos (nombre válido pero teléfono inválido), **When** intento actualizar, **Then** recibo errores de validación específicos para cada campo inválido

---

### User Story 5 - Renovación de Sesión sin Re-autenticación (Priority: P2)

Como usuario de aplicación web o móvil, quiero que mi sesión se renueve automáticamente usando mi refresh token cuando mi access token expire, para mantener una experiencia fluida sin tener que volver a iniciar sesión constantemente.

**Why this priority**: Mejora significativamente la experiencia de usuario, especialmente crítico en aplicaciones móviles donde las sesiones deben persistir entre usos de la app sin molestar al usuario con logins repetidos.

**Independent Test**: Puede probarse completamente usando un refresh token válido para obtener un nuevo access token sin requerir email/contraseña, entregando una experiencia de sesión continua.

**Acceptance Scenarios**:

1. **Given** tengo refresh token válido no expirado, **When** mi access token expira tras 60 minutos, **Then** puedo usar el refresh token para obtener un nuevo access token sin re-autenticarme
2. **Given** mi refresh token ha expirado (más de 7 días), **When** intento usarlo para renovar, **Then** recibo error "Token expirado, inicia sesión nuevamente"
3. **Given** tengo refresh token inválido o malformado, **When** intento usarlo, **Then** recibo error "Token inválido"
4. **Given** mi refresh token fue revocado (por ejemplo, al cambiar contraseña), **When** intento usarlo, **Then** recibo error "Token revocado, inicia sesión nuevamente"

---

### User Story 6 - Eliminación de Cuenta (Soft Delete) (Priority: P3)

Como usuario, quiero poder solicitar la eliminación de mi cuenta cuando ya no desee usar el servicio, manteniendo el cumplimiento de normativas de privacidad mientras se preserva el historial para auditorías empresariales.

**Why this priority**: Es funcionalidad importante para cumplimiento legal (GDPR) y confianza del usuario, pero no crítica para MVP inicial. Los usuarios pueden gestionar su privacidad sin comprometer integridad de datos históricos.

**Independent Test**: Puede probarse completamente solicitando eliminación de cuenta, verificando que se marca como inactiva (no borrado físico), el usuario no puede autenticarse pero su historial de postulaciones permanece accesible para empresas.

**Acceptance Scenarios**:

1. **Given** estoy autenticado, **When** solicito eliminar mi cuenta, **Then** mi cuenta se marca como inactiva (isActive=false) pero no se elimina físicamente
2. **Given** mi cuenta está inactiva, **When** intento iniciar sesión, **Then** recibo error "Cuenta desactivada"
3. **Given** tengo postulaciones activas a ofertas, **When** elimino mi cuenta, **Then** el historial de mis postulaciones se preserva para que las empresas mantengan su registro
4. **Given** soy administrador, **When** consulto usuario eliminado, **Then** puedo ver sus datos históricos con indicador de cuenta inactiva

---

### Edge Cases

- ¿Qué sucede cuando un usuario intenta registrarse exactamente en su 16º cumpleaños (límite de edad)?
- ¿Cómo maneja el sistema múltiples intentos de registro simultáneos con el mismo email desde diferentes clientes?
- ¿Qué pasa si un refresh token expira durante una operación en progreso que usa el access token?
- ¿Cómo se comporta el sistema cuando se actualiza un perfil con datos parcialmente válidos (algunos campos correctos, otros inválidos)?
- ¿Qué sucede si se intenta actualizar el perfil de un usuario que fue eliminado (soft delete) mientras la sesión aún estaba activa?
- ¿Cómo maneja el sistema intentos de login cuando el contador de fallos está en 4 y se hacen múltiples intentos simultáneos que llevarían a bloqueo?
- ¿Qué sucede si un usuario necesita cambiar su contraseña comprometida? (Nota: cambio de contraseña NO está en scope de esta feature)

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST allow users to register providing email, password, full name, birth date, and phone number
- **FR-002**: System MUST validate email format using standard RFC 5322 pattern and ensure uniqueness across all users
- **FR-003**: System MUST enforce password complexity requirements: minimum 8 characters, at least one uppercase letter, one lowercase letter, one number, and one special character
- **FR-004**: System MUST verify users are at least 16 years old by validating birth date against current date
- **FR-005**: System MUST validate phone number format ensuring it follows E.164 international format (country code + national number, e.g., +34600123456)
- **FR-006**: System MUST assign CANDIDATE role by default to all newly registered users
- **FR-007**: System MUST prevent auto-registration of company accounts (companies created only by administrators)
- **FR-008**: System MUST authenticate users with email and password, returning JWT access token (60 minutes validity) and refresh token (7 days validity)
- **FR-009**: System MUST hash all passwords using BCrypt algorithm with work factor 12 before storage
- **FR-010**: System MUST never store passwords in plain text
- **FR-011**: System MUST implement account lockout mechanism after 5 consecutive failed login attempts (counter resets after successful login or lockout period expires)
- **FR-012**: System MUST temporarily lock accounts for 15 minutes after reaching failed attempt threshold, counter resets to 0 after lockout period
- **FR-013**: System MUST include mandatory JWT claims: sub (user ID), email, role, exp (expiration), iss (issuer), aud (audience)
- **FR-014**: System MUST provide endpoint for authenticated users to view their own profile (/api/v1/users/me)
- **FR-015**: System MUST provide endpoint for administrators to view any user profile (/api/v1/users/{id})
- **FR-016**: System MUST allow users to update personal information: full name, birth date, phone, resume, skills, and location
- **FR-017**: System MUST prevent modification of email address once account is created
- **FR-018**: System MUST allow users to renew access tokens using valid refresh tokens without re-authentication
- **FR-019**: System MUST validate refresh tokens and reject expired, invalid, or revoked tokens
- **FR-020**: System MUST implement soft delete for user accounts, marking them as inactive instead of physical deletion
- **FR-021**: System MUST prevent authentication attempts from soft-deleted (inactive) users
- **FR-022**: System MUST preserve user historical data (applications, activity) even after soft delete for audit purposes

### Out of Scope

- **OOS-001**: Password change functionality is NOT included in this feature (future enhancement)
- **OOS-002**: Password recovery/reset via email is NOT included in this feature (future enhancement)
- **OOS-003**: Email verification during registration is NOT included in this feature (future enhancement)
- **OOS-004**: Creation of COMPANY or ADMIN accounts is NOT included in this feature (separate admin feature required)

### Key Entities

- **User**: Represents a system user with attributes: unique identifier, email (unique), password hash (BCrypt), full name, birth date, phone number, role (CANDIDATE/COMPANY/ADMIN), skills list, location, resume/CV, active status (for soft delete), created timestamp, last login timestamp
- **RefreshToken**: Represents authentication refresh token with attributes: token value (unique), associated user reference, expiration date (7 days), revoked status, created timestamp, last used timestamp

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Users can complete account registration in under 90 seconds when providing valid information on first attempt
- **SC-002**: System successfully authenticates users and returns JWT tokens in under 500ms for 95% of requests under normal load
- **SC-003**: 95% of users successfully update their profile information on first attempt without validation errors
- **SC-004**: System handles 500 concurrent registration attempts without data corruption or race conditions
- **SC-005**: Account lockout mechanism activates correctly 100% of the time after exactly 5 failed attempts within any rolling 15-minute window
- **SC-006**: Refresh token renewal succeeds with 99.5% success rate for valid, non-expired tokens
- **SC-007**: Soft delete preserves 100% of user historical data while preventing authentication with 100% effectiveness
- **SC-008**: Password validation rejects 100% of passwords not meeting complexity requirements before attempting storage
- **SC-009**: System prevents duplicate email registrations with 100% accuracy even under concurrent registration scenarios
- **SC-010**: JWT tokens contain all mandatory claims (sub, email, role, exp, iss, aud) in 100% of successful authentication responses
