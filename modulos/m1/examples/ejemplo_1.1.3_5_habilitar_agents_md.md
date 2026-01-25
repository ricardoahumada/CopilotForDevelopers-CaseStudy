# Configuración JSON: Habilitar lectura de AGENTS.md

**Contexto de uso:** Configuración para que Copilot reconozca y use el contexto de AGENTS.md.

**Archivo: `.vscode/settings.json` (adicional)**

```json
{
  "github.copilot.chat.attribution.enable": true,
  "github.copilot.chat.userPrompting.autoSendMemberPrivacyInformation": true
}
```

**Pasos de verificación:**

```
1. Crear un archivo AGENTS.md en la raíz del proyecto
2. Abrir el chat de Copilot (Ctrl+Alt+I)
3. El agente debería reconocer y usar el contexto del archivo
```

**Propósito:** Estas configuraciones permiten que Copilot Chat lea y utilice las instrucciones definidas en AGENTS.md para proporcionar respuestas contextualizadas al proyecto.
