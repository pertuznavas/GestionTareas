import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Task, TaskStatus } from '../../../../shared/models/task.model';
import { User } from '../../../../shared/models/user.model';
import { getErrorMessage } from '../../../../shared/utils/http-error';
import { UserService } from '../../../users/services/user';
import { TaskService } from '../../services/task';

@Component({
  imports: [RouterLink, DatePipe],
  selector: 'app-task-list',
  styleUrl: './task-list.css',
  templateUrl: './task-list.html',
})
export class TaskList {
  private readonly taskService = inject(TaskService);
  private readonly userService = inject(UserService);

  protected readonly statuses = [
    { value: TaskStatus.Pending, label: 'Pendiente' },
    { value: TaskStatus.InProgress, label: 'En progreso' },
    { value: TaskStatus.Done, label: 'Completada' },
  ];

  protected readonly tasks = signal<Task[]>([]);
  protected readonly users = signal<User[]>([]);
  protected readonly error = signal<string | null>(null);
  private statusFilter?: TaskStatus;

  constructor() {
    this.userService.getAll().subscribe({
      next: (users) => this.users.set(users),
      error: (err: HttpErrorResponse) => this.error.set(getErrorMessage(err)),
    });
    this.load();
  }

  protected filterByStatus(value: string): void {
    this.statusFilter = value ? (Number(value) as TaskStatus) : undefined;
    this.load();
  }

  protected changeStatus(task: Task, select: HTMLSelectElement): void {
    this.taskService.updateStatus(task.id, Number(select.value) as TaskStatus).subscribe({
      next: (updated) => this.onUpdated(updated),
      error: (err: HttpErrorResponse) => this.onUpdateError(err, select, task.status),
    });
  }

  protected assign(task: Task, select: HTMLSelectElement): void {
    this.taskService.assign(task.id, Number(select.value)).subscribe({
      next: (updated) => this.onUpdated(updated),
      error: (err: HttpErrorResponse) => this.onUpdateError(err, select, task.userId),
    });
  }

  private load(): void {
    this.taskService.getAll(this.statusFilter).subscribe({
      next: (tasks) => this.tasks.set(tasks),
      error: (err: HttpErrorResponse) => this.error.set(getErrorMessage(err)),
    });
  }

  private onUpdated(updated: Task): void {
    this.error.set(null);
    // With an active filter, reload when the task no longer matches so it leaves the list.
    if (this.statusFilter !== undefined && updated.status !== this.statusFilter) {
      this.load();
      return;
    }
    this.tasks.update((tasks) => tasks.map((t) => (t.id === updated.id ? updated : t)));
  }

  private onUpdateError(err: HttpErrorResponse, select: HTMLSelectElement, previousValue: number): void {
    this.error.set(getErrorMessage(err));
    // The model did not change, so Angular does not update the DOM: reset the select manually.
    select.value = String(previousValue);
  }
}
