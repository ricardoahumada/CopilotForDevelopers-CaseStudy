# Prompt: Estructura de Proyecto Python Data Science con SDD

**Contexto de uso:** Prompt para generar estructura de proyecto Python para data science con SDD.

**Prompt para generar estructura:**

```
Genera estructura de proyecto Python para data science con SDD:

## Stack
- Python 3.12
- Jupyter Notebooks
- Pandas, NumPy, Scikit-learn
- MLflow para tracking

## Estructura
src/
├── data/
│   ├── ingestion/
│   ├── processing/
│   └── features/
├── models/
│   ├── training/
│   └── evaluation/
└── utils/
notebooks/
├── 01-data-exploration.ipynb
├── 02-feature-engineering.ipynb
├── 03-model-training.ipynb
└── 04-evaluation.ipynb
specs/
  └── [feature]/
    ├── spec.md
    ├── plan.md
    └── tasks.md
AGENTS.md
.github/
  ├── agents/
  └── skills/
```

**Organización de notebooks:**

| Notebook | Propósito |
|----------|-----------|
| 01-data-exploration | Análisis exploratorio |
| 02-feature-engineering | Ingeniería de features |
| 03-model-training | Entrenamiento de modelos |
| 04-evaluation | Evaluación de resultados |
