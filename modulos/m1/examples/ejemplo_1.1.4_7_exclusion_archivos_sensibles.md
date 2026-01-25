# Configuración JSON: Exclusión de Archivos Sensibles

**Contexto de uso:** Configuración de VS Code para ocultar archivos sensibles del explorador.

**Archivo: `.vscode/settings.json`**

```json
{
  "files.exclude": {
    "**/*.key": true,
    "**/*.pem": true,
    "**/.env*": true,
    "**/secrets.json": true,
    "**/connectionStrings.secret.json": true,
    "**/credentials.json": true
  }
}
```

**Tipos de archivos excluidos:**

| Patrón | Tipo | Razón |
|--------|------|-------|
| `**/*.key` | Claves privadas | Seguridad |
| `**/*.pem` | Certificados | Autenticación |
| `**/.env*` | Variables de entorno | API keys, contraseñas |
| `**/secrets.json` | Secretos | Datos sensibles |
| `**/connectionStrings.secret.json` | Conexiones BD | Credenciales |
| `**/credentials.json` | Credenciales | Tokens, passwords |

**Beneficio:** Previene exposición accidental de archivos sensibles al agente de IA.
