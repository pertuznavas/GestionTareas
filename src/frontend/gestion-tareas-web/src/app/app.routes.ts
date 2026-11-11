import { Routes } from '@angular/router';
import { TaskForm } from './features/tasks/components/task-form/task-form';
import { TaskList } from './features/tasks/components/task-list/task-list';
import { UserList } from './features/users/components/user-list/user-list';

export const routes: Routes = [
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },
  { path: 'tasks', component: TaskList },
  { path: 'tasks/new', component: TaskForm },
  { path: 'users', component: UserList },
  { path: '**', redirectTo: 'tasks' },
];
