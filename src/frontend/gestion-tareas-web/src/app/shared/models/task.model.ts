export enum TaskStatus {
  Pending = 1,
  InProgress = 2,
  Done = 3,
}

export type TaskPriority = 'Low' | 'Medium' | 'High';

export interface TaskAdditionalInfo {
  priority?: TaskPriority | null;
  /** Format: yyyy-MM-dd (DateOnly en la API). */
  estimatedEndDate?: string | null;
  tags?: string[] | null;
}

export interface Task {
  id: number;
  title: string;
  description: string | null;
  status: TaskStatus;
  userId: number;
  createdAt: string;
  additionalInfo: TaskAdditionalInfo | null;
}

export interface CreateTask {
  title: string;
  description?: string | null;
  userId: number;
  additionalInfo?: TaskAdditionalInfo | null;
}

export interface UpdateTaskStatus {
  status: TaskStatus;
}

export interface AssignTask {
  userId: number;
}
