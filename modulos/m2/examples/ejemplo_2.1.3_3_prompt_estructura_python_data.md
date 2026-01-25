# Prompt: Estructura de Proyecto Python Data Science

**Contexto de uso:** Prompt para generar estructura de proyecto Python para análisis de datos con SDD.

**Prompt para generar estructura de notebooks Python:**

````
Genera estructura de proyecto Python para análisis de productividad:

## Especificación de Referencia
- Feature: Productivity Analytics
- Spec: specs/002-productivity-analytics/spec.md

## Stack Tecnológico
- Python 3.12
- Jupyter Notebooks
- Pandas, NumPy, Scikit-learn
- MLflow para tracking
- Plotly para visualización
- FastAPI para serving

## Estructura Requerida
```
notebooks/
├── 01-data-exploration.ipynb
├── 02-feature-engineering.ipynb
├── 03-model-training.ipynb
├── 04-model-evaluation.ipynb
├── 05-prediction-api.ipynb
└── 06-dashboard-analysis.ipynb

src/
├── data/
│   ├── __init__.py
│   ├── ingest.py
│   ├── clean.py
│   └── validate.py
├── features/
│   ├── __init__.py
│   ├── engineer.py
│   └── selection.py
├── models/
│   ├── __init__.py
│   ├── train.py
│   ├── evaluate.py
│   └── predict.py
├── api/
│   ├── __init__.py
│   ├── main.py
│   └── schemas.py
└── utils/
    ├── __init__.py
    ├── metrics.py
    └── visualization.py

tests/
├── test_data.py
├── test_features.py
└── test_models.py

experiments/
├── mlruns/
└── config.yaml

docs/
├── data-dictionary.md
└── model-card.md

requirements.txt
setup.py
README.md
```

## Templates de Notebook Requeridos
- Header con info de metadata
- Secciones: Objective, Data, Methodology, Results, Conclusions
- Configuración de MLflow al inicio
- Celda de Imports estandarizada
"""
````

**Organización de notebooks:**

| Notebook | Propósito |
|----------|-----------|
| 01-data-exploration | Análisis exploratorio |
| 02-feature-engineering | Ingeniería de features |
| 03-model-training | Entrenamiento de modelos |
| 04-model-evaluation | Evaluación de resultados |
