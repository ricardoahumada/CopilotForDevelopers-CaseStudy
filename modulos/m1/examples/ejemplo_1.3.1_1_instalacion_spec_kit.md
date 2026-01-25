# Comandos Bash: Instalación de Spec Kit

**Contexto de uso:** Comandos para instalar y configurar Spec Kit en un proyecto.

**Instalación de Spec Kit:**

```bash
# Método 1: Instalación persistente con uv (recomendado)
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

# Verificar instalación
specify check

# Actualizar Spec Kit
uv tool install specify-cli --force --from git+https://github.com/github/spec-kit.git

# Método 2: Uso único con uvx
uvx --from git+https://github.com/github/spec-kit.git specify init <PROJECT_NAME>
```

**Métodos de instalación:**

| Método | Comando | Uso |
|--------|---------|-----|
| Persistente | `uv tool install` | Instalación global |
| Uso único | `uvx` | Sin instalación |
