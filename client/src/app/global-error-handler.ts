import { ErrorHandler, inject, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NGXLogger } from 'ngx-logger';
import Swal from 'sweetalert2';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  private logger = inject(NGXLogger);

  handleError(error: unknown): void {
    const err = (error as { rejection?: unknown }).rejection || error;

    this.logger.error('Global error handler:', err);

    let title = 'Application error';
    let message = 'An unexpected error occurred.';

    switch (true) {
      case err instanceof HttpErrorResponse:{
        const httpErrorMap: Record<number, { title: string; message: string }> = {
          401: {
            title: 'Invalid credentials',
            message: 'Your session is not valid (401).'
          },
          500: {
            title: 'Server error',
            message: 'The server is temporarily unavailable (500).'
          }
        };
        
        const mapped = httpErrorMap[err.status];
        if (mapped) {
          title = mapped.title;
          message = mapped.message;
        } 
   
        break;
      }
      case err instanceof Error:
        message = err.message;
        break;

      case typeof err === 'string':
        message = err;
        break;
    }

    Swal.fire({
      title: title,
      text: message,
      icon: 'error',
      confirmButtonText: 'Reload',
      allowOutsideClick: false 
    }).then(() => {
      window.location.reload();
    });
  }
}