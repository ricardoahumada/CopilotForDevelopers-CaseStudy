# Configuración: Incluir Archivos SDD en Git

**Contexto de uso:** Configuración de .gitignore para asegurar que los archivos SDD importantes se incluyan en el repositorio.

**Archivo: `.gitignore` (negaciones)**

```gitignore
# No ignorar estos archivos SDD
 !AGENTS.md
 !.specify/
 !specs/
 !.github/agents/
 !.github/skills/
 !CLAUDE.md
```

**Archivos SDD que DEBEN incluirse:**

| Archivo/Carpeta | Propósito | Prioridad |
|-----------------|-----------|-----------|
| `AGENTS.md` | Configuración global de agentes | ✅ Crítico |
| `.specify/` | Templates y constitución | ✅ Crítico |
| `specs/` | Especificaciones del proyecto | ✅ Crítico |
| `.github/agents/` | Agentes personalizados | ✅ Importante |
| `.github/skills/` | Agent Skills compartidos | ✅ Importante |
| `CLAUDE.md` | Configuración adicional | ⚠️ Opcional |
