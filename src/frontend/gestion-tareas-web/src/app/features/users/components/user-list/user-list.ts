import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../../../../shared/models/user.model';
import { getErrorMessage } from '../../../../shared/utils/http-error';
import { UserService } from '../../services/user';

@Component({
  imports: [ReactiveFormsModule, DatePipe],
  selector: 'app-user-list',
  styleUrl: './user-list.css',
  templateUrl: './user-list.html',
})
export class UserList {
  private readonly userService = inject(UserService);

  protected readonly users = signal<User[]>([]);
  protected readonly error = signal<string | null>(null);

  protected readonly form = inject(FormBuilder).nonNullable.group({
    name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
    email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
  });

  constructor() {
    this.load();
  }

  protected save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.userService.create(this.form.getRawValue()).subscribe({
      next: (user) => {
        this.users.update((users) => [...users, user]);
        this.form.reset();
        this.error.set(null);
      },
      error: (err: HttpErrorResponse) => this.error.set(getErrorMessage(err)),
    });
  }

  private load(): void {
    this.userService.getAll().subscribe({
      next: (users) => this.users.set(users),
      error: (err: HttpErrorResponse) => this.error.set(getErrorMessage(err)),
    });
  }
}
