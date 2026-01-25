# Prompt para Pipeline ML

**Contexto de uso:** Este prompt implementa pipeline de Machine Learning para predicción de productividad en Python.

**Prompt completo:**
```
Implementa pipeline de ML para predicción de productividad:

## Especificación
- specs/002-productivity/spec.md#ml-pipeline
- REQ-ML-001: Entrenamiento automatizado
- REQ-ML-002: Predicción en tiempo real

## Pipeline Stages
1. Data Ingestion: Leer datos de SQL Server
2. Data Cleaning: Missing values, outliers
3. Feature Engineering: Crear features
4. Model Training: Regresión lineal + Random Forest
5. Model Evaluation: Cross-validation
6. Model Serving: API para predicciones

## Estructura del Proyecto
```
src/ml/
├── data/
│   ├── ingest.py
│   ├── clean.py
│   └── features.py
├── models/
│   ├── train.py
│   ├── evaluate.py
│   └── predict.py
├── api/
│   └── main.py
└── utils/
    └── metrics.py
```

## Formato de Salida
```python
"""
Pipeline de Machine Learning para Predicción de Productividad.
Implementa el flujo completo desde ingestión de datos hasta serving.
"""

import pandas as pd
import numpy as np
from datetime import datetime
from typing import Tuple, Dict, Optional
import logging
from abc import ABC, abstractmethod

from .data.ingest import DataIngestor
from .data.clean import DataCleaner
from .data.features import FeatureEngineer
from .models.train import ModelTrainer
from .models.evaluate import ModelEvaluator

logger = logging.getLogger(__name__)


class MLPipeline:
    """
    Pipeline completo de Machine Learning para productividad.
    
    Stages:
        1. Ingestión de datos
        2. Limpieza de datos
        3. Feature engineering
        4. Entrenamiento de modelo
        5. Evaluación
        6. Guardado de modelo
    """
    
    def __init__(
        self,
        config: Dict,
        enable_logging: bool = True
    ):
        self.config = config
        self.enable_logging = enable_logging
        
        # Inicializar componentes
        self.ingestor = DataIngestor(config['database'])
        self.cleaner = DataCleaner()
        self.engineer = FeatureEngineer()
        self.trainer = ModelTrainer(config['model'])
        self.evaluator = ModelEvaluator()
        
        self._log("Pipeline inicializado")
    
    def run(self, mode: str = 'train') -> Dict:
        """
        Ejecuta el pipeline completo.
        
        Args:
            mode: 'train' para entrenar, 'predict' para predecir
            
        Returns:
            Dict con resultados del pipeline
        """
        try:
            # Stage 1: Ingestión
            self._log("Stage 1: Ingestión de datos")
            raw_data = self.ingestor.extract(
                query=self.config['data_query'],
                mode=mode
            )
            
            # Stage 2: Limpieza
            self._log("Stage 2: Limpieza de datos")
            clean_data = self.cleaner.process(raw_data)
            
            # Stage 3: Features
            self._log("Stage 3: Feature engineering")
            if mode == 'train':
                featured_data, feature_names = self.engineer.create_features(clean_data)
                
                # Stage 4: Entrenamiento
                self._log("Stage 4: Entrenamiento")
                X, y = self._split_features_target(featured_data)
                model, metrics = self.trainer.train(X, y)
                
                # Stage 5: Evaluación
                self._log("Stage 5: Evaluación")
                eval_metrics = self.evaluator.evaluate(model, X, y)
                
                # Stage 6: Guardado
                self._log("Stage 6: Guardado de modelo")
                self.trainer.save_model(model, feature_names)
                
                return {
                    'status': 'success',
                    'metrics': eval_metrics,
                    'model_path': self.config['model_path']
                }
            else:
                # Solo predicción
                featured_data, _ = self.engineer.create_features(clean_data, training=False)
                return self._predict(featured_data)
                
        except Exception as e:
            self._log(f"Error en pipeline: {str(e)}", level='error')
            raise
    
    def _split_features_target(self, df: pd.DataFrame) -> Tuple[pd.DataFrame, pd.Series]:
        """Separa features y variable objetivo."""
        target_col = 'productividad_score'
        X = df.drop(columns=[target_col])
        y = df[target_col]
        return X, y
    
    def _predict(self, featured_data: pd.DataFrame) -> Dict:
        """Ejecuta predicción."""
        model = self.trainer.load_model()
        predictions = model.predict(featured_data)
        return {
            'status': 'success',
            'predictions': predictions.tolist()
        }
    
    def _log(self, message: str, level: str = 'info'):
        """Logging interno."""
        if self.enable_logging:
            getattr(logger, level)(f"[MLPipeline] {message}")


# Interfaz para serving
class ProductividadPredictor:
    """Wrapper para predicciones en tiempo real."""
    
    def __init__(self, model_path: str):
        self.model = None
        self.model_path = model_path
        self._load_model()
    
    def _load_model(self):
        """Carga el modelo entrenado."""
        from .models.train import ModelLoader
        self.model, self.feature_names = ModelLoader.load(self.model_path)
    
    def predict(self, features: Dict) -> Dict:
        """
        Genera predicción para un usuario.
        
        Args:
            features: Dictionary con features del usuario
            
        Returns:
            Dict con predicción y confidence
        """
        import pandas as pd
        
        # Convertir a DataFrame
        df = pd.DataFrame([features])
        df = df[self.feature_names]
        
        # Predicción
        productividad = self.model.predict(df)[0]
        
        # Confidence (basado en varianza del modelo)
        confidence = self._calculate_confidence(df)
        
        return {
            'productividad_predicha': round(productividad, 2),
            'confidence': confidence,
            'timestamp': datetime.utcnow().isoformat()
        }
    
    def _calculate_confidence(self, features: pd.DataFrame) -> float:
        """Calcula confidence basado en distancia al training data."""
        # Implementar cálculo de confianza
        return 0.85
```
```
