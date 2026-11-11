import { HttpErrorResponse } from '@angular/common/http';

/** Extrae un mensaje legible de los errores que devuelve la API. */
export function getErrorMessage(error: HttpErrorResponse): string {
  if (error.status === 0) {
    return 'No se pudo conectar con la API.';
  }

  const body = error.error;

  if (body?.errorMessage) {
    return body.errorMessage;
  }

  // Errores de validación de [ApiController] (ValidationProblemDetails).
  if (body?.errors) {
    return Object.values(body.errors as Record<string, string[]>).flat().join(' ');
  }

  return body?.title ?? 'Ocurrió un error inesperado.';
}
