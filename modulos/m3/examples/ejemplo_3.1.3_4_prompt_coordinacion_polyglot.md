# Prompt para Coordinación Polyglot

**Contexto de uso:** Este prompt genera configuración para coordinar proyecto multi-lenguaje (C# + Python + TypeScript).

**Prompt completo:**
```
@agent Genera configuración para coordinar proyecto C# + Python + TypeScript:

## Arquitectura Polyglot
```
portalempleo/
├── backend/           # C# ASP.NET Core 8
│   ├── src/
│   │   ├── API/
│   │   ├── Application/
│   │   ├── Domain/
│   │   └── Infrastructure/
│   └── tests/
├── ml-pipeline/       # Python 3.12
│   ├── src/
│   │   ├── data/
│   │   ├── models/
│   │   └── api/
│   └── notebooks/
├── frontend/          # TypeScript React 18
│   ├── src/
│   │   ├── components/
│   │   ├── screens/
│   │   ├── hooks/
│   │   └── services/
│   └── tests/
└── mobile/            # React Native
    ├── src/
    └── tests/
```

## Contratos Compartidos
```json
// shared-contracts/task.json
{
  "task": {
    "id": "uuid",
    "title": "string",
    "description": "string|null",
    "status": "pendiente|en_progreso|completada|cancelada",
    "priority": "baja|media|alta|urgente",
    "dueDate": "ISO8601|null",
    "createdAt": "ISO8601",
    "createdBy": "uuid",
    "assignedTo": "uuid|null"
  },
  "pagination": {
    "page": "number",
    "pageSize": "number",
    "total": "number",
    "totalPages": "number"
  }
}
```

## AGENTS.md Polyglot Configuration
```markdown
# PortalEmpleo - AGENTS.md (Polyglot)

## Multi-Language Coordination

### Backend (C#)
- Location: backend/
- Agent: @backend-dev
- Test Framework: xUnit + Moq
- Database: SQL Server + EF Core

### ML Pipeline (Python)
- Location: ml-pipeline/
- Agent: @ml-dev
- Test Framework: pytest
- Libraries: pandas, scikit-learn, fastapi

### Frontend (TypeScript)
- Location: frontend/
- Agent: @frontend-dev
- Test Framework: Jest + React Testing Library
- State: Zustand + TanStack Query

### Mobile (React Native)
- Location: mobile/
- Agent: @mobile-dev
- Test Framework: Jest
- Framework: Expo

## Shared Contracts
- All services use task.json schema
- API versioning: /api/v1/, /api/v2/
- Error format: ProblemDetails (backend), API error (Python)

## Cross-Language Standards

### Logging
```json
// Structured logging format
{
  "timestamp": "ISO8601",
  "level": "INFO|WARN|ERROR",
  "service": "backend|ml|frontend|mobile",
  "message": "string",
  "correlationId": "uuid",
  "metadata": {}
}
```

### API Communication
- REST over HTTPS
- JWT authentication (Bearer token)
- Rate limiting: 100 req/min
- Timeout: 30 seconds
```
```
