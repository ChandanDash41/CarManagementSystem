import { HttpInterceptorFn } from '@angular/common/http';
import { NotificationService } from '../Services/notification.service';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
  const notification = inject(NotificationService); // Injecting service within the function

  return next(req).pipe(
    catchError((error: { message: string; }) => {
      notification.showError(error.message, 'API Error');
      return throwError(() => error);
    })
  );
};
