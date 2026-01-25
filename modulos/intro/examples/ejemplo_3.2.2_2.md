# CONSTITUTION.md - Principios del Proyecto

**Contexto de uso:** Este ejemplo muestra un archivo CONSTITUTION.md que define los principios fundamentales y restricciones que deben respetarse en todo el código del proyecto.

**Ejemplo completo:**
```markdown
# Constitucion del Proyecto: Sistema de Gestion de Tareas

## Principios Fundamentales

### Seguridad
- Todas las APIs deben autenticar solicitudes mediante JWT tokens
- Datos sensibles se cifran en transito (TLS 1.3) y en reposo (AES-256)
- Implementar rate limiting para prevenir ataques de fuerza bruta
- Logs no deben contener informacion personal identificable (PII)

### Rendimiento
- Tiempo de respuesta maximo para APIs: 200ms (p95)
- Consultas a base de datos deben completarse en menos de 50ms
- El sistema debe soportar 10,000 usuarios concurrentes sin degradacion

### Cumplimiento Normativo
- Cumplimiento GDPR para usuarios de la UE
- Auditoria completa de cambios en datos de usuarios
```

**Secciones principales:**

| Sección | Propósito | Ejemplos |
|---------|-----------|----------|
| **Seguridad** | Define requisitos de seguridad | JWT, TLS 1.3, rate limiting, no PII en logs |
| **Rendimiento** | Establece métricas de performance | <200ms APIs, <50ms BD, 10K usuarios concurrentes |
| **Cumplimiento** | Requisitos legales y normativos | GDPR, auditoría de cambios |

**Lección aprendida:** CONSTITUTION.md actúa como contrato de referencia que los agentes de IA deben consultar antes de generar código, asegurando que las implementaciones cumplan estándares organizacionales.
