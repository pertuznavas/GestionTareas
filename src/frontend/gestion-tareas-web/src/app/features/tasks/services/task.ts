import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { CreateTask, Task, TaskStatus } from '../../../shared/models/task.model';

@Service()
export class TaskService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/tasks`;

  getAll(status?: TaskStatus): Observable<Task[]> {
    let params = new HttpParams();
    if (status !== undefined) {
      params = params.set('status', status);
    }
    return this.http.get<Task[]>(this.baseUrl, { params });
  }

  getById(id: number): Observable<Task> {
    return this.http.get<Task>(`${this.baseUrl}/${id}`);
  }

  create(task: CreateTask): Observable<Task> {
    return this.http.post<Task>(this.baseUrl, task);
  }

  updateStatus(id: number, status: TaskStatus): Observable<Task> {
    return this.http.put<Task>(`${this.baseUrl}/${id}/status`, { status });
  }

  assign(id: number, userId: number): Observable<Task> {
    return this.http.put<Task>(`${this.baseUrl}/${id}/user`, { userId });
  }
}
