# Checklist de Verificación de Implementación
/speckit.checklist Validar la implementación en src/ contra:
- El spec.md original en specs/001-mi-feature/
- Las decisiones arquitectónicas de plan.md.
- Los principios de Constitution.md (calidad del código, pruebas, seguridad)

Centrarse en:
1. Cobertura de los criterios de aceptación: verificar que cada criterio tenga el código correspondiente
2. Codificar estándares de calidad desde la constitución (denominación, documentación, principios SOLID)
3. Cobertura de pruebas: asegúrese de que existan pruebas unitarias/de integración para todos los métodos públicos.
4. Se manejan los casos extremos de spec.md
5. Se implementan los requisitos de seguridad (autenticación, validación).
6. No hay valores codificados ni secretos en el código.