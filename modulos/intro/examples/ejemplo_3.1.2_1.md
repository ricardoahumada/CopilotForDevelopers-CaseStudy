# Instalación de Spec Kit

**Contexto de uso:** Este ejemplo muestra el comando de instalación de Spec Kit y las características clave del toolkit, ilustrando cómo inicializar un proyecto con esta herramienta.

**Comando de instalación:**
```bash
uvx --from git+https://github.com/github/spec-kit.git specify init <PROJECT_NAME>
```

**Características clave de Spec Kit:**

| Característica | Descripción |
|----------------|-------------|
| **Multi-stack** | Funciona en Python, JavaScript, Go, etc. |
| **Integración organizacional** | Soporta estándares, políticas de seguridad y compliance |
| **Refinamiento iterativo** | Permite actualizar specs y regenerar planes |
| **Puntos de verificación** | Verificación humana explícita en cada fase |
| **Código abierto** | Toolkit experimental de GitHub |

**Comandos de Spec Kit:**

| Comando | Propósito |
|---------|-----------|
| `/specify` | Generar PRODUCT_SPEC.md desde descripción de alto nivel |
| `/plan` | Crear ARCHITECTURE_PLAN.md definiendo stack y arquitectura |
| `/tasks` | Generar IMPLEMENTATION_TASKS.md descompuesto en unidades |
| `/implement` | Ejecutar implementación tarea por tarea |

**Casos de uso óptimos:**

| Caso | Descripción |
|------|-------------|
| **Proyectos greenfield** | Iniciar nuevos proyectos con intención clara |
| **Trabajo de features** | Agregar características a códigos base complejos |
| **Modernización de legado** | Reconstruir sistemas capturando lógica original |

**Lección aprendida:** Spec Kit proporciona una estructura formal para SDD, transformando descripciones de alto nivel en especificaciones ejecutables.
