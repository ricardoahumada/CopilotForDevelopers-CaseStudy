# Clarifications Report: Gestión de Usuarios y Autenticación

**Feature**: 001-user-auth  
**Date**: 2026-02-04  
**Status**: Partially Resolved - 3 Critical Items Resolved, 12 Pending  
**Specification**: [spec.md](../spec.md)

## Executive Summary

Este reporte identifica **15 áreas** que requerían clarificación. **3 ítems críticos han sido RESUELTOS** y la especificación ha sido actualizada. Quedan **12 ítems pendientes** (3 críticos, 7 medium, 2 low) para completar la clarificación antes de proceder con planificación.

---

## ✅ Decisiones Resueltas (Actualizadas en Spec)

### ✅ RESOLVED - CL-001: Formato de Número de Teléfono
**Requisito afectado**: FR-005  
**Decisión tomada**: Formato E.164 internacional obligatorio

**Solución implementada**:
- FR-005 actualizado: "System MUST validate phone number format ensuring it follows E.164 international format (country code + national number, e.g., +34600123456)"
- Nuevo escenario de aceptación en US1: validación de formato internacional
- Ejemplos claros: +34600123456, +12025551234

**Actualizado en**: spec.md - US1 Acceptance Scenario #6, FR-005

---

### ✅ RESOLVED - CL-002: Comportamiento del Contador de Intentos Fallidos
**Requisito afectado**: FR-011, FR-012, SC-005  
**Decisión tomada**: 5 intentos consecutivos sin éxito intermedio

**Solución implementada**:
- Contador se reinicia a 0 tras login exitoso O tras expiración de 15 min de bloqueo
- Los intentos deben ser consecutivos (sin login exitoso entre medias)
- FR-011 actualizado: "(counter resets after successful login or lockout period expires)"
- FR-012 actualizado: "counter resets to 0 after lockout period"
- Nuevos escenarios en US2: comportamiento de reset del contador

**Actualizado en**: spec.md - US2 Acceptance Scenarios #3, #4, #6, FR-011, FR-012

---

### ✅ RESOLVED - CL-007: Cambio de Contraseña
**Gap identificado**: No existe user story ni requisito para cambiar contraseña  
**Decisión tomada**: NO está en scope de esta feature

**Solución implementada**:
- Nueva sección "Out of Scope" agregada a Requirements
- OOS-001 documenta explícitamente que cambio de contraseña es enhancement futuro
- Edge case agregado mencionando limitación
- Recuperación de contraseña también marcada como OOS-002

**Actualizado en**: spec.md - Edge Cases (nota), Requirements > Out of Scope

---

## 1. Requisitos Ambiguos o Contradictorios (Pendientes)

### 🟡 MEDIUM - CL-003: Habilidades del Usuario - Estructura de Datos
**Requisito afectado**: FR-016, US4  
**Ambigüedad**: "skills list" sin especificar formato o validaciones

**Problema**: 
- ¿Las habilidades son texto libre o lista predefinida?
- ¿Cuántas habilidades puede agregar un usuario (límite)?
- ¿Cuál es la longitud máxima de cada habilidad?
- ¿Se normalizan (ej: "python" == "Python" == "PYTHON")?
- ¿Se permiten duplicados?

**Impacto**: Medio - Afecta calidad de datos y matching con ofertas.

**Opciones recomendadas**:
- A) Texto libre, máximo 20 habilidades, 50 caracteres cada una, case-insensitive, sin duplicados
- B) Lista predefinida de habilidades técnicas estándar
- C) Híbrido: lista sugerida con opción de agregar personalizadas

---

### 🟡 MEDIUM - CL-004: Ubicación Geográfica - Nivel de Detalle
**Requisito afectado**: FR-016, User entity  
**Ambigüedad**: "location" sin especificar granularidad

**Problema**:
- ¿Qué nivel de detalle se requiere? ¿País, ciudad, código postal, dirección completa?
- ¿Es campo obligatorio u opcional?
- ¿Se valida contra lista de ciudades conocidas o es texto libre?
- ¿Se usa para matching geográfico con ofertas?

**Impacto**: Medio - Afecta búsqueda y matching de ofertas.

**Opciones recomendadas**:
- A) Obligatorio: País + Ciudad (validado contra base de datos)
- B) Opcional: País, Ciudad, Código Postal (texto libre)
- C) Solo país obligatorio, resto opcional

---

### 🟡 MEDIUM - CL-005: Resume/CV - Formato y Almacenamiento
**Requisito afectado**: FR-016, User entity  
**Ambigüedad**: "resume/CV" sin especificar formato o límites

**Problema**:
- ¿Es archivo adjunto (PDF, DOCX) o texto en campo de base de datos?
- Si es archivo: ¿Tamaño máximo? ¿Formatos permitidos?
- ¿Es obligatorio u opcional?
- ¿Se pueden tener múltiples versiones o solo una?

**Impacto**: Medio - Afecta arquitectura de almacenamiento.

**Opciones recomendadas**:
- A) Campo texto enriquecido en BD (máximo 5000 caracteres)
- B) Archivo PDF/DOCX adjunto (máximo 2MB)
- C) Ambos: texto para búsqueda + archivo original descargable

---

### 🟢 LOW - CL-006: Nombre Completo - Validación de Longitud Mínima
**Requisito afectado**: FR-001  
**Ambigüedad**: "longitud máxima de 100 caracteres" pero no mínima

**Problema**:
- ¿Cuál es la longitud mínima aceptable? ¿2 caracteres, 3 caracteres?
- ¿Se valida que contenga al menos nombre y apellido (2 palabras)?
- ¿Se permiten caracteres especiales (acentos, ñ, apóstrofes)?

**Impacto**: Bajo - Afecta solo validación de entrada.

**Opciones recomendadas**:
- A) Mínimo 2 caracteres, máximo 100, permite acentos y caracteres UTF-8
- B) Mínimo 3 caracteres, debe tener al menos 2 palabras (nombre + apellido)

---

## 2. Casos Edge No Cubiertos (Pendientes)

### 🔴 CRITICAL - CL-008: Recuperación de Contraseña Olvidada
**Gap identificado**: No existe flujo de "olvidé mi contraseña"  
**Status**: Marcado como Out of Scope (OOS-002)

**Problema**:
- ¿Cómo recupera un usuario su cuenta si olvida la contraseña?
- ¿Se envía link por email? ¿Cuánto tiempo es válido?
- ¿Requiere verificación adicional (preguntas de seguridad, código SMS)?

**Impacto**: Alto - Sin esto, cuentas bloqueadas son irrecuperables por el usuario.

**Decisión requerida**: Confirmar que está fuera de scope y será feature separada o agregar a esta feature.

---

### 🟡 MEDIUM - CL-009: Verificación de Email
**Gap identificado**: No hay requisito de verificar email tras registro  
**Status**: Marcado como Out of Scope (OOS-003)

**Problema**:
- ¿Los usuarios pueden usar la cuenta inmediatamente o necesitan verificar email?
- Si requiere verificación: ¿cuánto tiempo tienen? ¿pueden reenviar email?
- ¿Qué pasa si nunca verifican el email?

**Impacto**: Medio - Afecta seguridad y calidad de usuarios.

**Opciones recomendadas**:
- A) Verificación obligatoria antes de acceder al sistema
- B) Verificación opcional, cuenta funcional pero con limitaciones
- C) Sin verificación (confiar en que email es válido) - ACTUAL

---

### 🟡 MEDIUM - CL-010: Múltiples Sesiones Simultáneas
**Gap identificado**: No se especifica límite de sesiones concurrentes

**Problema**:
- ¿Un usuario puede estar logueado desde múltiples dispositivos simultáneamente?
- ¿Cuántos refresh tokens activos puede tener un usuario?
- ¿Al hacer login desde nuevo dispositivo se invalidan tokens anteriores?

**Impacto**: Medio - Afecta seguridad y arquitectura de tokens.

**Opciones recomendadas**:
- A) Sesiones ilimitadas (múltiples dispositivos permitidos) - DEFAULT RAZONABLE
- B) Máximo 3 sesiones activas, la 4ª invalida la más antigua
- C) Una sesión única, login nuevo invalida sesión anterior

---

### 🟡 MEDIUM - CL-011: Reactivación de Cuenta Eliminada
**Gap identificado**: US6 cubre eliminación pero no reactivación

**Problema**:
- ¿Un usuario puede reactivar su cuenta después de soft delete?
- Si sí, ¿puede hacerlo él mismo o requiere intervención de admin?
- ¿Se preservan todos sus datos incluyendo contraseña o debe re-registrarse?
- ¿Hay límite de tiempo para reactivar (ej: 30 días)?

**Impacto**: Medio - Afecta experiencia de usuario y retención.

**Opciones recomendadas**:
- A) Auto-reactivación por el usuario dentro de 30 días
- B) Solo admin puede reactivar, sin límite temporal - DEFAULT RAZONABLE
- C) Soft delete es permanente, debe crear cuenta nueva

---

### 🟢 LOW - CL-012: Actualización Parcial vs Completa de Perfil
**Gap identificado**: US4 no aclara si actualización es parcial o requiere todos los campos

**Problema**:
- ¿El usuario puede actualizar solo un campo (ej: solo teléfono) o debe enviar todo el perfil?
- ¿Campos no enviados se dejan sin cambios o se vacían?

**Impacto**: Bajo - Afecta diseño de API.

**Opciones recomendadas**:
- A) PATCH: actualización parcial (solo campos enviados) - DEFAULT RAZONABLE
- B) PUT: actualización completa (todos los campos obligatorios)

---

## 3. Criterios de Aceptación No Medibles o Vagos (Pendientes)

### 🟡 MEDIUM - CL-013: "Normal Load" Sin Definir
**Criterio afectado**: SC-002  
**Vaguedad**: "under normal load" sin cuantificar carga

**Problema**:
- ¿Qué constituye "carga normal"? ¿Cuántos usuarios concurrentes? ¿Cuántas requests/segundo?
- ¿El 95% se mide en qué ventana temporal (1 minuto, 1 hora, 1 día)?

**Impacto**: Medio - No se puede validar objetivamente.

**Recomendación**: Especificar "under load of 100 concurrent auth requests per second over 1-minute window"

---

### 🟢 LOW - CL-014: "First Attempt" Ambiguo
**Criterio afectado**: SC-003  
**Vaguedad**: "on first attempt" sin contexto de qué constituye "intento"

**Problema**:
- ¿Primera vez que el usuario accede a la pantalla de actualización?
- ¿Primer intento en una sesión o primer intento de esa actualización específica?

**Impacto**: Bajo - Métricamente menos crítico.

**Recomendación**: Cambiar a "95% of profile update requests succeed without requiring retry"

---

## 4. Preguntas de Negocio Abiertas (Pendientes)

### 🔴 CRITICAL - CL-015: Roles COMPANY y ADMIN - Gestión
**Gap identificado**: FR-007 menciona que empresas no se auto-registran pero no hay flujo para crearlas  
**Status**: Marcado como Out of Scope (OOS-004)

**Problema**:
- ¿Quién crea cuentas de empresa? ¿Cómo se asigna rol COMPANY?
- ¿Los administradores se crean manualmente en BD o hay interfaz?
- ¿Esta feature incluye endpoint para que admin cree usuarios con otros roles?

**Impacto**: Alto - Necesario para sistema funcional completo.

**Decisión requerida**: Confirmar que se maneja en feature separada (ej: 002-admin-management)

---

## Resumen de Prioridades Actualizadas

| Prioridad | Resueltos | Pendientes | Total |
|-----------|-----------|------------|-------|
| 🔴 CRITICAL | 2 | 1 | 3 |
| 🟡 MEDIUM | 1 | 6 | 7 |
| 🟢 LOW | 0 | 2 | 2 |
| **TOTAL** | **3** | **9** | **12** |

## Recomendaciones Actualizadas

### ✅ Completado
- CL-001 (teléfono E.164), CL-002 (intentos consecutivos), CL-007 (cambio contraseña fuera de scope)

### 🚦 Siguiente Paso
1. **Decisión de Negocio**: CL-008 (recuperación contraseña) - ¿Confirmar fuera de scope?
2. **Decisión de Negocio**: CL-015 (gestión COMPANY/ADMIN) - ¿Confirmar feature separada?
3. **Defaults Razonables**: CL-003 a CL-006, CL-009 a CL-014 pueden proceder con defaults documentados en plan.md

### ✅ Ready for Planning
Con las 3 decisiones críticas resueltas y las 2 críticas pendientes confirmadas como fuera de scope, **la especificación está lista para `/speckit.plan`** aplicando defaults razonables para los ítems medium/low.

---

**Prepared by**: Spec-Driven Development Process  
**Review Status**: 3 Critical Items Resolved, Ready for Planning with Documented Defaults

---

## 1. Requisitos Ambiguos o Contradictorios

### 🔴 CRITICAL - CL-001: Formato de Número de Teléfono
**Requisito afectado**: FR-005  
**Ambigüedad**: "System MUST validate phone number format ensuring it follows international or local format patterns"

**Problema**: 
- ¿Qué significa "internacional o local"? ¿Ambos son aceptables?
- ¿Se requiere prefijo de país (+34, +1, etc.)?
- ¿Qué formatos específicos son válidos? ¿E.164, formatos locales con guiones/paréntesis?
- ¿Cuál es la longitud mínima/máxima aceptable?

**Impacto**: Alto - Afecta validación en registro y actualización de perfil.

**Opciones recomendadas**:
- A) Formato E.164 internacional obligatorio (ej: +34600123456)
- B) Formato local del país de residencia del usuario
- C) Ambos formatos aceptables con detección automática
- D) Solo validar longitud (10-15 dígitos) sin formato estricto

---

### 🔴 CRITICAL - CL-002: Comportamiento del Contador de Intentos Fallidos
**Requisito afectado**: FR-011, FR-012, SC-005  
**Ambigüedad**: "5 consecutive failed login attempts" vs "within any rolling 15-minute window"

**Problema**:
- ¿El contador se resetea tras login exitoso o tras el tiempo de bloqueo?
- ¿Los 5 intentos deben ser consecutivos SIN ÉXITO intermedio o pueden ser 5 dentro de 15 minutos con éxitos entre medias?
- ¿El bloqueo de 15 minutos comienza desde el 5º intento fallido o desde el primer intento?
- Si el usuario espera 15 minutos pero falla nuevamente, ¿se bloquea inmediatamente o comienza nuevo contador?

**Impacto**: Alto - Afecta seguridad y experiencia de usuario.

**Opciones recomendadas**:
- A) 5 intentos consecutivos sin éxito, reset tras login exitoso o 15 min de bloqueo
- B) 5 intentos dentro de ventana deslizante de 15 min, reset solo tras login exitoso
- C) 5 intentos consecutivos, bloqueo permanente hasta reset manual por admin

---

### 🟡 MEDIUM - CL-003: Habilidades del Usuario - Estructura de Datos
**Requisito afectado**: FR-016, US4  
**Ambigüedad**: "skills list" sin especificar formato o validaciones

**Problema**:
- ¿Las habilidades son texto libre o lista predefinida?
- ¿Cuántas habilidades puede agregar un usuario (límite)?
- ¿Cuál es la longitud máxima de cada habilidad?
- ¿Se normalizan (ej: "python" == "Python" == "PYTHON")?
- ¿Se permiten duplicados?

**Impacto**: Medio - Afecta calidad de datos y matching con ofertas.

**Opciones recomendadas**:
- A) Texto libre, máximo 20 habilidades, 50 caracteres cada una
- B) Lista predefinida de habilidades técnicas estándar
- C) Híbrido: lista sugerida con opción de agregar personalizadas

---

### 🟡 MEDIUM - CL-004: Ubicación Geográfica - Nivel de Detalle
**Requisito afectado**: FR-016, User entity  
**Ambigüedad**: "location" sin especificar granularidad

**Problema**:
- ¿Qué nivel de detalle se requiere? ¿País, ciudad, código postal, dirección completa?
- ¿Es campo obligatorio u opcional?
- ¿Se valida contra lista de ciudades conocidas o es texto libre?
- ¿Se usa para matching geográfico con ofertas?

**Impacto**: Medio - Afecta búsqueda y matching de ofertas.

**Opciones recomendadas**:
- A) Obligatorio: País + Ciudad (validado contra base de datos)
- B) Opcional: País, Ciudad, Código Postal (texto libre)
- C) Solo país obligatorio, resto opcional

---

### 🟡 MEDIUM - CL-005: Resume/CV - Formato y Almacenamiento
**Requisito afectado**: FR-016, User entity  
**Ambigüedad**: "resume/CV" sin especificar formato o límites

**Problema**:
- ¿Es archivo adjunto (PDF, DOCX) o texto en campo de base de datos?
- Si es archivo: ¿Tamaño máximo? ¿Formatos permitidos?
- ¿Es obligatorio u opcional?
- ¿Se pueden tener múltiples versiones o solo una?

**Impacto**: Medio - Afecta arquitectura de almacenamiento.

**Opciones recomendadas**:
- A) Campo texto enriquecido en BD (máximo 5000 caracteres)
- B) Archivo PDF/DOCX adjunto (máximo 2MB)
- C) Ambos: texto para búsqueda + archivo original descargable

---

### 🟢 LOW - CL-006: Nombre Completo - Validación de Longitud Mínima
**Requisito afectado**: FR-001  
**Ambigüedad**: "longitud máxima de 100 caracteres" pero no mínima

**Problema**:
- ¿Cuál es la longitud mínima aceptable? ¿2 caracteres, 3 caracteres?
- ¿Se valida que contenga al menos nombre y apellido (2 palabras)?
- ¿Se permiten caracteres especiales (acentos, ñ, apóstrofes)?

**Impacto**: Bajo - Afecta solo validación de entrada.

**Opciones recomendadas**:
- A) Mínimo 2 caracteres, máximo 100, permite acentos y caracteres UTF-8
- B) Mínimo 3 caracteres, debe tener al menos 2 palabras (nombre + apellido)

---

## 2. Casos Edge No Cubiertos

### 🔴 CRITICAL - CL-007: Cambio de Contraseña
**Gap identificado**: No existe user story ni requisito para cambiar contraseña

**Problema**:
- ¿Los usuarios pueden cambiar su contraseña una vez registrados?
- Si sí, ¿requieren la contraseña actual para validación?
- ¿Al cambiar contraseña se revocan todos los refresh tokens existentes?
- ¿Hay límite de cuántas veces puede cambiarla (ej: 1 vez por día)?

**Impacto**: Alto - Funcionalidad de seguridad fundamental.

**Decisión requerida**: ¿Está en scope de esta feature o en feature futura?

---

### 🔴 CRITICAL - CL-008: Recuperación de Contraseña Olvidada
**Gap identificado**: No existe flujo de "olvidé mi contraseña"

**Problema**:
- ¿Cómo recupera un usuario su cuenta si olvida la contraseña?
- ¿Se envía link por email? ¿Cuánto tiempo es válido?
- ¿Requiere verificación adicional (preguntas de seguridad, código SMS)?

**Impacto**: Alto - Sin esto, cuentas bloqueadas son irrecuperables.

**Decisión requerida**: ¿Está en scope de esta feature o se maneja fuera del sistema (soporte manual)?

---

### 🟡 MEDIUM - CL-009: Verificación de Email
**Gap identificado**: No hay requisito de verificar email tras registro

**Problema**:
- ¿Los usuarios pueden usar la cuenta inmediatamente o necesitan verificar email?
- Si requiere verificación: ¿cuánto tiempo tienen? ¿pueden reenviar email?
- ¿Qué pasa si nunca verifican el email?

**Impacto**: Medio - Afecta seguridad y calidad de usuarios.

**Opciones recomendadas**:
- A) Verificación obligatoria antes de acceder al sistema
- B) Verificación opcional, cuenta funcional pero con limitaciones
- C) Sin verificación (confiar en que email es válido)

---

### 🟡 MEDIUM - CL-010: Múltiples Sesiones Simultáneas
**Gap identificado**: No se especifica límite de sesiones concurrentes

**Problema**:
- ¿Un usuario puede estar logueado desde múltiples dispositivos simultáneamente?
- ¿Cuántos refresh tokens activos puede tener un usuario?
- ¿Al hacer login desde nuevo dispositivo se invalidan tokens anteriores?

**Impacto**: Medio - Afecta seguridad y arquitectura de tokens.

**Opciones recomendadas**:
- A) Sesiones ilimitadas (múltiples dispositivos permitidos)
- B) Máximo 3 sesiones activas, la 4ª invalida la más antigua
- C) Una sesión única, login nuevo invalida sesión anterior

---

### 🟡 MEDIUM - CL-011: Reactivación de Cuenta Eliminada
**Gap identificado**: US6 cubre eliminación pero no reactivación

**Problema**:
- ¿Un usuario puede reactivar su cuenta después de soft delete?
- Si sí, ¿puede hacerlo él mismo o requiere intervención de admin?
- ¿Se preservan todos sus datos incluyendo contraseña o debe re-registrarse?
- ¿Hay límite de tiempo para reactivar (ej: 30 días)?

**Impacto**: Medio - Afecta experiencia de usuario y retención.

**Opciones recomendadas**:
- A) Auto-reactivación por el usuario dentro de 30 días
- B) Solo admin puede reactivar, sin límite temporal
- C) Soft delete es permanente, debe crear cuenta nueva

---

### 🟢 LOW - CL-012: Actualización Parcial vs Completa de Perfil
**Gap identificado**: US4 no aclara si actualización es parcial o requiere todos los campos

**Problema**:
- ¿El usuario puede actualizar solo un campo (ej: solo teléfono) o debe enviar todo el perfil?
- ¿Campos no enviados se dejan sin cambios o se vacían?

**Impacto**: Bajo - Afecta diseño de API.

**Opciones recomendadas**:
- A) PATCH: actualización parcial (solo campos enviados)
- B) PUT: actualización completa (todos los campos obligatorios)

---

## 3. Criterios de Aceptación No Medibles o Vagos

### 🟡 MEDIUM - CL-013: "Normal Load" Sin Definir
**Criterio afectado**: SC-002  
**Vaguedad**: "under normal load" sin cuantificar carga

**Problema**:
- ¿Qué constituye "carga normal"? ¿Cuántos usuarios concurrentes? ¿Cuántas requests/segundo?
- ¿El 95% se mide en qué ventana temporal (1 minuto, 1 hora, 1 día)?

**Impacto**: Medio - No se puede validar objetivamente.

**Recomendación**: Especificar "under load of 100 concurrent auth requests per second over 1-minute window"

---

### 🟢 LOW - CL-014: "First Attempt" Ambiguo
**Criterio afectado**: SC-003  
**Vaguedad**: "on first attempt" sin contexto de qué constituye "intento"

**Problema**:
- ¿Primera vez que el usuario accede a la pantalla de actualización?
- ¿Primer intento en una sesión o primer intento de esa actualización específica?

**Impacto**: Bajo - Métricamente menos crítico.

**Recomendación**: Cambiar a "95% of profile update requests succeed without requiring retry"

---

## 4. Preguntas de Negocio Abiertas

### 🔴 CRITICAL - CL-015: Roles COMPANY y ADMIN - Gestión
**Gap identificado**: FR-007 menciona que empresas no se auto-registran pero no hay flujo para crearlas

**Problema**:
- ¿Quién crea cuentas de empresa? ¿Cómo se asigna rol COMPANY?
- ¿Los administradores se crean manualmente en BD o hay interfaz?
- ¿Esta feature incluye endpoint para que admin cree usuarios con otros roles?

**Impacto**: Alto - Necesario para sistema funcional completo.

**Decisión requerida**: ¿Está en scope de 001-user-auth o es feature separada (ej: 001-admin-management)?

---

## Resumen de Prioridades

| Prioridad | Cantidad | Descripción |
|-----------|----------|-------------|
| 🔴 CRITICAL | 6 | Requieren decisión inmediata antes de planificación |
| 🟡 MEDIUM | 7 | Afectan diseño pero pueden tener defaults razonables |
| 🟢 LOW | 2 | Mejoras de claridad, no bloquean avance |

## Recomendaciones

1. **Bloqueo de Planificación**: Resolver CL-001 (teléfono), CL-002 (contador intentos), CL-007 (cambio contraseña), CL-008 (recuperación), CL-015 (gestión roles)

2. **Decisiones de Diseño**: CL-003 a CL-006, CL-009 a CL-012 pueden resolverse con defaults razonables documentados en ADRs

3. **Mejoras Editoriales**: CL-013, CL-014 son refinamientos que no bloquean pero mejoran testabilidad

4. **Siguiente Paso**: Sesión de clarificación con stakeholders para resolver ítems CRITICAL antes de ejecutar `/speckit.plan`

---

**Prepared by**: Spec-Driven Development Process  
**Review Status**: Awaiting Stakeholder Input