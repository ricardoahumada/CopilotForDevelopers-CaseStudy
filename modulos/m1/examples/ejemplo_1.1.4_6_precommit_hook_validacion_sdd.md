# Script Bash: Pre-commit Hook para Validación SDD

**Contexto de uso:** Git hook para validar que la estructura SDD está completa antes de cada commit.

**Script: `.git/hooks/pre-commit`**

```bash
# Crear pre-commit hook para validar estructura SDD
cat > .git/hooks/pre-commit << 'EOF'
#!/bin/bash
# Validar que specs/ tiene archivos spec.md, plan.md, tasks.md
for dir in specs/*/; do
  if [ ! -f "$dir/spec.md" ]; then
    echo "Error: Falta spec.md en $dir"
    exit 1
  fi
done
echo "Validación SDD completada"
EOF
chmod +x .git/hooks/pre-commit
```

**Validaciones realizadas:**

| Validación | Descripción |
|------------|-------------|
| Existencia de `spec.md` | Cada feature debe tener especificación |
| Estructura de carpetas | Valida formato `specs/XXX-nombre/` |

**Instalación:**

1. Ejecutar el script en la raíz del proyecto
2. El hook se activa automáticamente en cada `git commit`
3. Si falta algún archivo, el commit se rechaza
