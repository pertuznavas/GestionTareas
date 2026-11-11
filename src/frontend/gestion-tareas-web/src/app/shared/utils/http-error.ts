import { HttpErrorResponse } from '@angular/common/http';

/** Gets a readable message from API errors, which follow the ProblemDetails format (RFC 9457). */
export function getErrorMessage(error: HttpErrorResponse): string {
  if (error.status === 0) {
    return 'Could not connect to the API.';
  }

  const body = error.error;

  // [ApiController] validation errors (ValidationProblemDetails).
  if (body?.errors) {
    return Object.values(body.errors as Record<string, string[]>).flat().join(' ');
  }

  return body?.detail ?? body?.title ?? 'An unexpected error occurred.';
}
