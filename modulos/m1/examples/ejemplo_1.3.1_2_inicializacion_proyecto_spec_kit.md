# Comandos Bash: Inicialización de Proyecto con Spec Kit

**Contexto de uso:** Comandos para inicializar un nuevo proyecto con Spec Kit.

**Inicialización de proyecto:**

```bash
# Inicializar nuevo proyecto
specify init mi-proyecto-sdd

# Especificar agente de IA a usar
specify init mi-proyecto-sdd --ai copilot
specify init mi-proyecto-sdd --ai claude

# Inicializar en directorio actual
specify init . --ai copilot
specify init --here --ai copilot

# Forzar inicialización en directorio no vacío
specify init . --force --ai copilot

# Omitir inicialización de Git
specify init mi-proyecto --no-git --ai copilot

# Habilitar salida de depuración
specify init mi-proyecto --debug --ai copilot

# Token de GitHub para entornos corporativos
specify init mi-proyecto --github-token ghp_tu_token --ai copilot
```

**Opciones de inicialización:**

| Opción | Descripción |
|--------|-------------|
| `--ai copilot` | Configurar para GitHub Copilot |
| `--ai claude` | Configurar para Claude |
| `--here` | Inicializar en directorio actual |
| `--force` | Forzar en directorio no vacío |
| `--no-git` | Omitir inicialización Git |
| `--debug` | Modo depuración |
| `--github-token` | Token para entornos corporativos |
