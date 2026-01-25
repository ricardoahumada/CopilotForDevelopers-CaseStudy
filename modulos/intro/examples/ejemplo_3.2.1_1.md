# Especificación de Producto - Sistema de Comercio Electrónico

**Contexto de uso:** Este ejemplo muestra una especificación de producto escrita manualmente, sin usar Spec Kit. Ilustra cómo documentar formalmente los requisitos de un proyecto antes de implementar.

**Especificación:**
```markdown
# Sistema de Comercio Electrónico - Especificación de Producto

## Proposito
Plataforma de comercio electronico B2C para venta de productos artesanales

## Usuarios Objetivo
- Vendedores: Artesanos que desean vender sus productos online
- Compradores: Clientes que buscan productos unicos y hechos a mano

## Funcionalidades Core
1. Catalogo de productos con busqueda y filtros
2. Carrito de compras y proceso de checkout
3. Sistema de pagos con tarjeta y PayPal
4. Seguimiento de pedidos
5. Sistema de reseñas y calificaciones

## APIs Requeridas
- GET /api/products - Lista productos con filtros
- POST /api/orders - Crea nueva orden
- POST /api/payments - Procesa pago
- GET /api/orders/:id - Consulta estado de orden

## Criterios de Aceptacion
- Tiempo de respuesta < 300ms
- Soporte para 500 usuarios concurrentes
- Disponibilidad 99.5%
```

**Elementos clave de la especificación:**

| Sección | Propósito | Ejemplo del documento |
|---------|-----------|----------------------|
| **Propósito** | Define qué es el producto | Plataforma B2C para productos artesanales |
| **Usuarios objetivo** | Identifica stakeholders | Vendedores y compradores |
| **Funcionalidades core** | Enumera features principales | Catálogo, carrito, pagos, seguimiento |
| **APIs requeridas** | Define contratos backend | GET /api/products, POST /api/orders |
| **Criterios de aceptación** | Métricas medibles | <300ms respuesta, 500 usuarios concurrentes |

**Lección aprendida:** Las especificaciones de producto bien estructuradas actúan como contrato entre stakeholders y desarrolladores, reduciendo ambigüedades y facilitando la validación.
