# Prompt: Validación de Arquitectura contra Especificaciones

**Contexto de uso:** Prompt para validar que las decisiones arquitectónicas cumplen con los requisitos no funcionales.

**Prompt de validación arquitectónica:**

````
"Valida la siguiente propuesta de arquitectura contra constitution.md:

## Propuesta de Arquitectura
```
- API: REST sobre HTTPS (TLS 1.3)
- Database: SQL Server 2022 en Azure
- Caching: Redis 7 en Azure Cache
- Auth: JWT con refresh tokens (30 min / 7 días)
- Deployment: Azure App Service con auto-scaling
```

## Requisitos No Funcionales de constitution.md

### Rendimiento
- APIs responden en < 200ms (p95)
- Sistema maneja 10,000 usuarios concurrentes
- Processing asíncrono para ops > 5 segundos

### Seguridad
- Todas las APIs requieren JWT de Microsoft Entra ID
- Datos cifrados in transit (TLS 1.2+) y at rest (AES-256)
- No logging de PII

### Cumplimiento
- GDPR compliance para usuarios EU
- Audit log completo de cambios en datos de usuario

## Matriz de Cumplimiento
| Requisito NF | Propuesta | Cumple | Gap |
|--------------|-----------|--------|-----|
| TLS 1.3 | TLS 1.3 | ✓ | - |
| JWT Entra ID | JWT custom | ✗ | Migrar a Entra ID |
| AES-256 | TDE enabled | ✓ | - |
| GDPR | No EU data | N/A | - |
| Audit logging | Sin implementar | ✗ | Agregar |
| Async processing | Sync only | ✗ | Agregar queue |

## Salida Esperada
```
## Validación de Arquitectura

### Cumplimiento: PARCIAL (3/6 requisitos)

### Issues Críticos
1. [Alta] JWT personalizado no cumple política de identidad
2. [Alta] Falta logging de auditoría

### Recomendaciones
- Integrar Microsoft Entra ID para auth
- Agregar Application Insights para audit
```
````

**Resultado:** Reporte de gaps con recomendaciones de corrección.
