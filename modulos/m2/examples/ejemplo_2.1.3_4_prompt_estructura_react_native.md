# Prompt: Estructura de Proyecto React Native con Expo

**Contexto de uso:** Prompt para generar estructura de proyecto React Native escalable con SDD.

**Prompt para generar estructura React Native:**

````
Genera estructura de proyecto React Native con Expo para PortalEmpleo Mobile:

## Especificación de Referencia
- Feature: Task Management Mobile
- Spec: specs/003-task-mobile/spec.md

## Stack Tecnológico
- React Native con Expo 51
- TypeScript 5
- React Query para data fetching
- React Navigation 6
- Zustand para state management
- TanStack Query

## Estructura Requerida
```
src/
├── components/
│   ├── common/
│   │   ├── Button/
│   │   ├── Card/
│   │   ├── Input/
│   │   ├── Modal/
│   │   └── Loading/
│   ├── forms/
│   │   ├── TaskForm/
│   │   └── FilterForm/
│   └── layout/
│       ├── Header/
│       ├── Footer/
│       └── ScreenContainer/
├── screens/
│   ├── auth/
│   │   ├── LoginScreen/
│   │   └── RegisterScreen/
│   ├── tasks/
│   │   ├── TaskListScreen/
│   │   ├── TaskDetailScreen/
│   │   └── TaskCreateScreen/
│   └── home/
│       └── HomeScreen/
├── hooks/
│   ├── useAuth.ts
│   ├── useTasks.ts
│   └── useNotifications.ts
├── services/
│   ├── api/
│   │   ├── client.ts
│   │   ├── authApi.ts
│   │   └── tasksApi.ts
│   └── storage/
│       └── secureStorage.ts
├── store/
│   ├── authStore.ts
│   └── tasksStore.ts
├── types/
│   ├── auth.ts
│   ├── tasks.ts
│   └── api.ts
├── utils/
│   ├── formatting.ts
│   ├── validation.ts
│   └── constants.ts
└── navigation/
    ├── AppNavigator.tsx
    └── RootStackParamList.ts

__tests__/
├── components/
├── hooks/
└── screens/

docs/
├── api-contract.md
└── component-library.md

app.json
App.tsx
tsconfig.json
eas.json
```

## Configuraciones
- TypeScript strict mode
- ESLint con React hooks rules
- Prettier para formatting
- Jest + React Testing Library
"""
````
