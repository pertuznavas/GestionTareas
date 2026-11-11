import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CreateTask, TaskPriority } from '../../../../shared/models/task.model';
import { User } from '../../../../shared/models/user.model';
import { getErrorMessage } from '../../../../shared/utils/http-error';
import { UserService } from '../../../users/services/user';
import { TaskService } from '../../services/task';

@Component({
  imports: [ReactiveFormsModule, RouterLink],
  selector: 'app-task-form',
  styleUrl: './task-form.css',
  templateUrl: './task-form.html',
})
export class TaskForm {
  private readonly taskService = inject(TaskService);
  private readonly router = inject(Router);

  protected readonly priorities: TaskPriority[] = ['Low', 'Medium', 'High'];
  protected readonly users = signal<User[]>([]);
  protected readonly error = signal<string | null>(null);

  protected readonly form = inject(FormBuilder).nonNullable.group({
    title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
    description: ['', Validators.maxLength(150)],
    userId: [0, Validators.min(1)],
    priority: [''],
    estimatedEndDate: [''],
    tags: [''],
  });

  constructor() {
    inject(UserService).getAll().subscribe({
      next: (users) => this.users.set(users),
      error: (err: HttpErrorResponse) => this.error.set(getErrorMessage(err)),
    });
  }

  protected save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.taskService.create(this.toRequest()).subscribe({
      next: () => this.router.navigate(['/tasks']),
      error: (err: HttpErrorResponse) => this.error.set(getErrorMessage(err)),
    });
  }

  private toRequest(): CreateTask {
    const value = this.form.getRawValue();
    const tags = value.tags
      .split(',')
      .map((tag) => tag.trim())
      .filter((tag) => tag !== '');

    const hasAdditionalInfo = value.priority || value.estimatedEndDate || tags.length > 0;

    return {
      title: value.title.trim(),
      description: value.description.trim() || null,
      userId: value.userId,
      additionalInfo: hasAdditionalInfo
        ? {
            priority: (value.priority as TaskPriority) || null,
            estimatedEndDate: value.estimatedEndDate || null,
            tags: tags.length > 0 ? tags : null,
          }
        : null,
    };
  }
}
