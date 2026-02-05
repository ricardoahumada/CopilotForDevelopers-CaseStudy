<!--
Sync Impact Report:
- Version change: Initial → 1.0.0
- Added sections: All core principles and governance structure
- Templates requiring updates: ✅ Constitution created from template
- Follow-up TODOs: None
-->

# Portal Empleo API Constitution

## Core Principles

### I. Clean Architecture (NON-NEGOTIABLE)
The API MUST implement Clean Architecture with strict layer separation: Domain → Application → Infrastructure → Api. Each layer can only depend on inner layers. Domain entities cannot depend on external frameworks. Dependency Injection is mandatory via constructor injection. All cross-cutting concerns (logging, validation, exception handling) are handled through middleware and services in appropriate layers.

### II. API-First Design
Every feature MUST be defined through OpenAPI specification before implementation. All endpoints require comprehensive XML documentation. Swagger UI must be accessible and fully functional. API versioning follows semantic versioning (MAJOR.MINOR.PATCH). Breaking changes require version bump and deprecation notices. Response contracts are immutable once published.

### III. Security-First (NON-NEGOTIABLE)
JWT authentication with HS256 algorithm is mandatory for protected endpoints. Password hashing uses BCrypt with work factor 12 minimum. No sensitive data (passwords, tokens, secrets) in logs or responses. CORS policy strictly configured for allowed origins. Input validation through FluentValidation on all DTOs. SQL injection prevention through Entity Framework parameterized queries only.

### IV. Test-Driven Development
Minimum 80% code coverage for business logic enforced through CI/CD gates. xUnit + Moq + FluentAssertions for testing stack. Unit tests for services, integration tests for repositories and controllers. Test naming convention: Should_ExpectedBehavior_When_StateUnderTest. Tests must be written before implementation for new features (Red-Green-Refactor).

### V. Performance & Scalability
All database operations MUST be asynchronous using async/await pattern. Pagination mandatory for list endpoints with configurable page size (default: 10, max: 100). Response times under 200ms for 95th percentile. Health checks at /health, /health/live, /health/ready endpoints. Structured logging with correlation IDs for request tracing. In-Memory database for development, prepared for production database migration.

## Technology Standards

**Stack Requirements:**
- .NET 8.0 with ASP.NET Core 8.0 (mandatory)
- Entity Framework Core 8.0 for data access
- FluentValidation 11.9+ for input validation
- BCrypt.Net-Next 4.0.3+ for password hashing
- Swashbuckle.AspNetCore 6.5+ for API documentation
- xUnit + Moq + FluentAssertions for testing

**Coding Standards:**
- PascalCase for public members, camelCase for private fields with underscore prefix
- XML documentation mandatory for all public APIs
- SOLID principles strictly enforced
- Repository and Unit of Work patterns for data access
- DTO pattern for data transfer between layers

## Quality Gates

**Code Quality:**
- SonarQube quality gate must pass (no high severity issues)
- All code must be formatted using `dotnet format`
- StyleCop analyzers enabled for consistent code style
- No compiler warnings allowed in production builds

**Security Requirements:**
- No secrets committed to version control
- Environment variables for all configuration
- Dependency vulnerability scanning in CI/CD
- OWASP Top 10 compliance verification

**Performance Benchmarks:**
- API response time SLA: 95th percentile under 200ms
- Memory usage under 100MB for typical workload
- Concurrent request handling: minimum 100 requests/second

## Governance

This constitution supersedes all other development practices and guidelines. All pull requests must demonstrate compliance with these principles. Any deviation requires explicit justification and architectural decision record (ADR). Use AGENTS.md for runtime development guidance and context.

Amendment process requires:
1. Documented rationale for change
2. Impact analysis on existing codebase
3. Migration plan if breaking changes
4. Team consensus and approval

Compliance verification occurs at:
- Code review phase (manual)
- CI/CD pipeline (automated)
- Sprint retrospectives (continuous improvement)

**Version**: 1.0.0 | **Ratified**: 2026-02-03 | **Last Amended**: 2026-02-03
