# Referencia: Ubicación de Agent Skills Personales

**Contexto de uso:** Rutas del sistema donde se almacenan Agent Skills personales según el sistema operativo.

**Ubicaciones por sistema operativo:**

```
# Ubicación de Agent Skills personales
Windows: %APPDATA%/Copilot/skills/
Linux/macOS: ~/.copilot/skills/
```

**Estructura de un Agent Skill:**

```
~/.copilot/skills/
└── mi-skill-personal/
    ├── SKILL.md
    └── templates/
        └── *.md
```

**Diferencia entre Skills personales y de proyecto:**

| Ubicación | Alcance | Compartido |
|-----------|---------|------------|
| `~/.copilot/skills/` | Personal | ❌ No |
| `.github/skills/` | Proyecto | ✅ Sí (via Git) |

**Recomendación:** Usar `.github/skills/` para skills compartidos en el equipo.
