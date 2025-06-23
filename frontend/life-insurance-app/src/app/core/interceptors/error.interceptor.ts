import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AlertService } from 'src/app/shared/services/alert.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private alertService: AlertService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 200) return throwError(() => error);
        
        let message = 'Ocurrió un error inesperado. Intenta nuevamente.';

        if (error.error?.errors) {
          // Si viene del modelo de FluentValidation
          const errorGroups = error.error.errors; // Ej: { Password: ["msg1", "msg2"], Email: ["msg1"] }
          const messages = Object.entries(errorGroups).map(([field, errors]) => {
            const joinedErrors = (errors as string[]).join('. ') + '.';
            return `${field}: ${joinedErrors}`;
          });

          message = messages.join('\n');
        } else if (typeof error.error === 'string') {
          message = error.error;
        } else if (error.status === 401) {
          message = 'No estás autorizado.';
        } else if (error.status === 403) {
          message = 'Acceso denegado.';
        } else if (error.status === 404) {
          message = 'Recurso no encontrado.';
        }

        this.alertService.error(message);
        return throwError(() => error);
      })
    );
  }
}
