# Configuración: .gitignore para Artefactos SDD

**Contexto de uso:** Configuración de .gitignore para excluir artefactos temporales de Spec Kit.

**Archivo: `.gitignore`**

```gitignore
# Spec Kit
.specify/cache/
.specify/logs/

# Agent Skills (si son personales)
~/.copilot/skills/

# Archivos temporales de prompts
*.prompt.md.bak
```

**Explicación:**

| Patrón | Razón de exclusión |
|--------|-------------------|
| `.specify/cache/` | Archivos temporales de caché |
| `.specify/logs/` | Logs de ejecución de Spec Kit |
| `~/.copilot/skills/` | Skills personales del usuario |
| `*.prompt.md.bak` | Backups de prompts |
