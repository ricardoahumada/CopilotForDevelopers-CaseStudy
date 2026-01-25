# Prompt: Específico para SDD - Validación de Emails

**Contexto de uso:** Ejemplo de prompt estructurado siguiendo metodología Spec-Driven Development.

**Prompt específico:**

```
"Crea una función de validación de emails para el módulo de autenticación de PortalEmpleo API con las siguientes especificaciones:

## Requisitos Funcionales
- Validar formato RFC 5322 estándar de emails
- Longitud máxima: 254 caracteres
- Permitir caracteres especiales en dominio: -, _
- Rechazar emails con doble @ consecutivos
- Rechazar emails que empiezan o terminan con punto

## Contexto del Proyecto
- Lenguaje: C# 12
- Framework: ASP.NET Core 8
- Patrón: Repository con Dependency Injection
- Ubicación: src/Domain/ValueObjects/Email.cs
- Convenciones: Microsoft C# Coding Conventions

## Criterios de Aceptación
- Tests unitarios cubriendo casos válidos e inválidos
- Mensajes de error específicos para cada tipo de validación fallida
- Integración con el resultado base ValueObject

## Restricciones
- No usar librerías externas de validación
- Mantener método simple y sin dependencias
- Internacionalización preparada para mensajes de error
- Rendimiento: < 1ms por validación

Genera el código C# completo incluyendo tests xUnit"
```

**Elementos clave del prompt SDD:**

| Elemento | Propósito |
|----------|-----------|
| Requisitos Funcionales | Define QUÉ debe hacer |
| Contexto del Proyecto | Define DÓNDE y CÓMO |
| Criterios de Aceptación | Define CUÁNDO está completo |
| Restricciones | Define limitaciones técnicas |
