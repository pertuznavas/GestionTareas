import { HttpErrorResponse } from '@angular/common/http';

/** Extrae un mensaje legible de los errores de la API, que siguen el formato ProblemDetails (RFC 9457). */
export function getErrorMessage(error: HttpErrorResponse): string {
  if (error.status === 0) {
    return 'No se pudo conectar con la API.';
  }

  const body = error.error;

  // Errores de validación de [ApiController] (ValidationProblemDetails).
  if (body?.errors) {
    return Object.values(body.errors as Record<string, string[]>).flat().join(' ');
  }

  return body?.detail ?? body?.title ?? 'Ocurrió un error inesperado.';
}
