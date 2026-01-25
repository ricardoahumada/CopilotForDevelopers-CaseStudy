# Prompt para React Components

**Contexto de uso:** Este prompt implementa pantalla de lista de tareas con React, TypeScript, Zustand y TanStack Query.

**Prompt completo:**
```
Implementa pantalla de lista de tareas con React y Zustand:

## Especificación
- specs/003-task-mobile/spec.md#task list
- Endpoint: GET /api/tareas

## Stack Tecnológico
- React 18 con TypeScript
- Zustand para state management
- TanStack Query para data fetching
- React Navigation 6

## Estructura
```
src/
├── screens/tasks/
│   ├── TaskListScreen.tsx
│   ├── TaskListView.tsx
│   ├── TaskListViewModel.ts
│   └── index.ts
├── hooks/
│   └── useTasks.ts
├── store/
│   └── taskStore.ts
├── types/
│   └── task.ts
└── components/
    └── TaskCard.tsx
```

## Formato de Salida
```tsx
// types/task.ts
export interface Tarea {
  id: string;
  titulo: string;
  descripcion: string | null;
  estado: 'pendiente' | 'en_progreso' | 'completada' | 'cancelada';
  prioridad: 'baja' | 'media' | 'alta' | 'urgente';
  fechaLimite: string | null;
  creadoEn: string;
  actualizadoEn: string;
}

export interface TareaFilters {
  estado?: string;
  prioridad?: string;
  busqueda?: string;
  page: number;
  pageSize: number;
}

export interface PagedResult<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

// hooks/useTasks.ts
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '../services/api';
import { Tarea, TareaFilters } from '../types/task';

export const useTasks = (filters: TareaFilters) => {
  return useQuery({
    queryKey: ['tasks', filters],
    queryFn: async () => {
      const params = new URLSearchParams({
        page: filters.page.toString(),
        pageSize: filters.pageSize.toString(),
      });
      
      if (filters.estado) params.append('estado', filters.estado);
      if (filters.prioridad) params.append('prioridad', filters.prioridad);
      if (filters.busqueda) params.append('search', filters.busqueda);
      
      const response = await apiClient.get<PagedResult<Tarea>>(
        `/api/tareas?${params.toString()}`
      );
      
      return response.data;
    },
    staleTime: 30 * 1000, // 30 segundos
  });
};

export const useCompleteTask = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: async (taskId: string) => {
      const response = await apiClient.post<Tarea>(
        `/api/tareas/${taskId}/complete`,
        {}
      );
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['tasks'] });
    },
  });
};

export const useDeleteTask = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: async (taskId: string) => {
      await apiClient.delete(`/api/tareas/${taskId}`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['tasks'] });
    },
  });
};

// store/taskStore.ts
import { create } from 'zustand';
import { Tarea, TareaFilters } from '../types/task';

interface TaskState {
  selectedTask: Tarea | null;
  filters: TareaFilters;
  sortBy: 'creadoEn' | 'fechaLimite' | 'prioridad';
  sortOrder: 'asc' | 'desc';
  
  setSelectedTask: (task: Tarea | null) => void;
  setFilters: (filters: Partial<TareaFilters>) => void;
  setSortBy: (sortBy: 'creadoEn' | 'fechaLimite' | 'prioridad') => void;
  setSortOrder: (order: 'asc' | 'desc') => void;
  resetFilters: () => void;
}

const defaultFilters: TareaFilters = {
  page: 1,
  pageSize: 20,
};

export const useTaskStore = create<TaskState>((set) => ({
  selectedTask: null,
  filters: defaultFilters,
  sortBy: 'creadoEn',
  sortOrder: 'desc',
  
  setSelectedTask: (task) => set({ selectedTask: task }),
  
  setFilters: (newFilters) => set((state) => ({
    filters: { ...state.filters, ...newFilters, page: 1 } // Reset page
  })),
  
  setSortBy: (sortBy) => set({ sortBy }),
  setSortOrder: (sortOrder) => set({ sortOrder }),
  
  resetFilters: () => set({ filters: defaultFilters }),
}));

// screens/tasks/TaskListScreen.tsx
import React, { useState } from 'react';
import { View, FlatList, RefreshControl, ActivityIndicator } from 'react-native';
import { useTasks, useCompleteTask, useDeleteTask } from '../../hooks/useTasks';
import { useTaskStore } from '../../store/taskStore';
import { TaskCard } from '../../components/TaskCard';
import { TareaFiltersModal } from '../../components/TareaFiltersModal';
import { styles } from './TaskListScreen.styles';

export const TaskListScreen: React.FC = () => {
  const [showFilters, setShowFilters] = useState(false);
  const [refreshing, setRefreshing] = useState(false);
  
  const { filters, setFilters, resetFilters } = useTaskStore();
  
  const { data, isLoading, isFetching, refetch } = useTasks(filters);
  const completeTask = useCompleteTask();
  const deleteTask = useDeleteTask();
  
  const handleRefresh = async () => {
    setRefreshing(true);
    await refetch();
    setRefreshing(false);
  };
  
  const handleComplete = (taskId: string) => {
    completeTask.mutate(taskId);
  };
  
  const handleDelete = (taskId: string) => {
    deleteTask.mutate(taskId);
  };
  
  const renderItem = ({ item }: { item: Tarea }) => (
    <TaskCard
      tarea={item}
      onComplete={() => handleComplete(item.id)}
      onDelete={() => handleDelete(item.id)}
    />
  );
  
  const renderHeader = () => (
    <View style={styles.header}>
      <Text style={styles.title}>Mis Tareas</Text>
      <Text style={styles.subtitle}>
        {data?.total || 0} tareas
      </Text>
    </View>
  );
  
  const renderFooter = () => {
    if (!isLoading && data) {
      return (
        <View style={styles.pagination}>
          <Text>
            Página {data.page} de {data.totalPages}
          </Text>
        </View>
      );
    }
    return null;
  };
  
  if (isLoading && !data) {
    return (
      <View style={styles.centered}>
        <ActivityIndicator size="large" />
      </View>
    );
  }
  
  return (
    <View style={styles.container}>
      <FlatList
        data={data?.items}
        renderItem={renderItem}
        keyExtractor={(item) => item.id}
        ListHeaderComponent={renderHeader}
        ListFooterComponent={renderFooter}
        refreshControl={
          <RefreshControl
            refreshing={refreshing}
            onRefresh={handleRefresh}
          />
        }
        onEndReached={() => {
          if (data && data.page < data.totalPages) {
            setFilters({ page: filters.page + 1 });
          }
        }}
        onEndReachedThreshold={0.5}
      />
      
      <TareaFiltersModal
        visible={showFilters}
        filters={filters}
        onApply={(newFilters) => {
          setFilters(newFilters);
          setShowFilters(false);
        }}
        onReset={() => {
          resetFilters();
          setShowFilters(false);
        }}
        onClose={() => setShowFilters(false)}
      />
    </View>
  );
};
```
```
