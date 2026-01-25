# Prompt para Procesamiento de Datos

**Contexto de uso:** Este prompt implementa algoritmos de procesamiento de productividad en Python con Pandas y NumPy.

**Prompt completo:**
```
Implementa el motor de procesamiento de productividad según specs/002-productivity/spec.md:

## Especificación de Referencia
- Requisito: REQ-DATA-001 (Cálculo de métricas)
- Criterios: CA-DATA-001 a CA-DATA-004

## Algoritmos Requeridos
1. Tasa de completación: tareas completadas / tareas totales * 100
2. Tiempo promedio: suma(duración) / count(tareas completadas)
3. Productividad semanal: promedio(tasas semanales)
4. Predicción: regresión lineal de últimas 4 semanas

## Modelos de Datos
```python
@dataclass
class TareaProductividad:
    tarea_id: str
    usuario_id: str
    fecha_creacion: datetime
    fecha_completado: Optional[datetime]
    duracion_horas: float
    estado: str  # 'pendiente', 'en_progreso', 'completada', 'cancelada'
    prioridad: int  # 1-4
    esfuerzo_estimado_horas: float

@dataclass
class MetricaProductividad:
    usuario_id: str
    periodo_inicio: datetime
    periodo_fin: datetime
    tasa_completacion: float
    tiempo_promedio_horas: float
    tareas_totales: int
    tareas_completadas: int
    productividad_score: float  # 0-100
```

## Requisitos de Rendimiento
- Procesamiento de 10,000 registros en < 5 segundos
- Uso de vectorización NumPy
- Cacheo de resultados por usuario/día

## Formato de Salida
```python
import pandas as pd
import numpy as np
from datetime import datetime, timedelta
from typing import Optional
import logging

logger = logging.getLogger(__name__)

class ProductividadCalculator:
    """Calculador de métricas de productividad por usuario."""
    
    def __init__(self, cache_enabled: bool = True):
        self._cache: dict[str, MetricaProductividad] = {}
        self._cache_enabled = cache_enabled
    
    def calcular_tasa_completacion(
        self, 
        df: pd.DataFrame, 
        usuario_id: Optional[str] = None
    ) -> float:
        """
        Calcula la tasa de completación de tareas.
        
        Args:
            df: DataFrame con datos de tareas
            usuario_id: Filtrar por usuario específico
            
        Returns:
            float: Porcentaje de tareas completadas (0-100)
        """
        # Filtrar por usuario si aplica
        if usuario_id:
            df = df[df['usuario_id'] == usuario_id]
        
        # Filtrar tareas completadas
        completadas = df[df['estado'] == 'completada']
        total = len(df)
        
        if total == 0:
            return 0.0
        
        tasa = (len(completadas) / total) * 100
        
        logger.info(
            "Tasa completación: {:.2f}% ({}/{})".format(
                tasa, len(completadas), total
            )
        )
        
        return round(tasa, 2)
    
    def calcular_tiempo_promedio(
        self, 
        df: pd.DataFrame,
        usuario_id: Optional[str] = None
    ) -> float:
        """
        Calcula el tiempo promedio de completación.
        
        Args:
            df: DataFrame con datos de tareas
            usuario_id: Filtrar por usuario específico
            
        Returns:
            float: Tiempo promedio en horas
        """
        if usuario_id:
            df = df[df['usuario_id'] == usuario_id]
        
        # Solo tareas completadas
        completadas = df[
            (df['estado'] == 'completada') & 
            (df['duracion_horas'].notna())
        ]
        
        if len(completadas) == 0:
            return 0.0
        
        tiempo_promedio = completadas['duracion_horas'].mean()
        
        return round(tiempo_promedio, 2)
    
    def calcular_score_productividad(
        self,
        df: pd.DataFrame,
        usuario_id: Optional[str] = None
    ) -> MetricaProductividad:
        """
        Calcula el score de productividad completo.
        
        Args:
            df: DataFrame con datos de tareas
            usuario_id: Filtrar por usuario específico
            
        Returns:
            MetricaProductividad: Métricas calculadas
        """
        # Cache check
        cache_key = f"{usuario_id}_{datetime.now().date()}"
        if self._cache_enabled and cache_key in self._cache:
            return self._cache[cache_key]
        
        # Filtrar datos
        if usuario_id:
            df_usuario = df[df['usuario_id'] == usuario_id]
        else:
            df_usuario = df.copy()
        
        # Calcular métricas
        tasa_completacion = self.calcular_tasa_completacion(df_usuario)
        tiempo_promedio = self.calcular_tiempo_promedio(df_usuario)
        
        # Score compuesto (pesos configurables)
        score = (
            tasa_completacion * 0.6 +  # 60% peso en completación
            (100 - min(tiempo_promedio * 5, 50)) * 0.4  # 40% peso en velocidad
        )
        
        metricas = MetricaProductividad(
            usuario_id=usuario_id or 'all',
            periodo_inicio=df_usuario['fecha_creacion'].min(),
            periodo_fin=df_usuario['fecha_completado'].max() 
                if 'fecha_completado' in df_usuario.columns else datetime.now(),
            tasa_completacion=tasa_completacion,
            tiempo_promedio_horas=tiempo_promedio,
            tareas_totales=len(df_usuario),
            tareas_completadas=len(df_usuario[df_usuario['estado'] == 'completada']),
            productividad_score=round(min(score, 100), 2)
        )
        
        # Cache
        if self._cache_enabled:
            self._cache[cache_key] = metricas
        
        return metricas
    
    def predecir_productividad_proxima_semana(
        self,
        df_historico: pd.DataFrame,
        semanas_historico: int = 4
    ) -> dict:
        """
        Predice productividad de la próxima semana usando regresión lineal.
        
        Args:
            df_historico: DataFrame histórico de métricas
            semanas_historico: Semanas a considerar
            
        Returns:
            dict: Predicción con intervalo de confianza
        """
        # Preparar datos para regresión
        df_recent = df_historico.tail(semanas_historico).copy()
        
        if len(df_recent) < 2:
            return {
                'prediccion': None,
                'mensaje': 'Datos insuficientes para predicción',
                'confidence': 0
            }
        
        # Regresión lineal simple
        x = np.arange(len(df_recent))
        y = df_recent['productividad_score'].values
        
        coefficients = np.polyfit(x, y, 1)
        slope, intercept = coefficients
        
        # Predicción próxima semana
        prediccion = slope * len(df_recent) + intercept
        prediccion = max(0, min(100, prediccion))  # Clampear 0-100
        
        # Intervalo de confianza (basado en varianza)
        residuals = y - (slope * x + intercept)
        std_error = np.std(residuals)
        
        return {
            'prediccion_proxima_semana': round(prediccion, 2),
            'intervalo_confianza': (
                round(prediccion - 1.96 * std_error, 2),
                round(prediccion + 1.96 * std_error, 2)
            ),
            'tendencia': 'ascendente' if slope > 0 else 'descendente',
            'pendiente': round(slope, 4),
            'confianza': round(1 - std_error / 100, 2)
        }
```
```
