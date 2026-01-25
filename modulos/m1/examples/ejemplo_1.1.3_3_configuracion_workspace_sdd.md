# Configuración JSON: Settings del Workspace para SDD

**Contexto de uso:** Configuración de workspace compartida para proyectos Spec-Driven Development.

**Archivo: `.vscode/settings.json`**

```json
{
  "github.copilot.enable": {
    "*": true,
    "csharp": true,
    "python": true,
    "javascript": true,
    "typescript": true,
    "typescriptreact": true,
    "markdown": true
  },
  "editor.inlineSuggest.enabled": true,
  "editor.suggestOnTriggerCharacters": true,
  "editor.tabCompletion": "on",
  "files.autoSave": "afterDelay",
  "files.autoSaveDelay": 1000,
  "[markdown]": {
    "editor.wordWrap": "on",
    "editor.quickSuggestions": false
  },
  "markdown-preview-enhanced.enableScriptExecution": true
}
```

**Explicación de cada opción:**

| Opción | Valor | Descripción |
|--------|-------|-------------|
| `github.copilot.enable` | por lenguaje | Habilita Copilot selectivamente |
| `editor.inlineSuggest.enabled` | `true` | Sugerencias inline visibles |
| `editor.tabCompletion` | `"on"` | Tab para aceptar sugerencias |
| `files.autoSave` | `"afterDelay"` | Guardado automático |
| `[markdown].wordWrap` | `"on"` | Ajuste de línea en markdown |

**Ubicación:** Crear en la raíz del proyecto en carpeta `.vscode/`
