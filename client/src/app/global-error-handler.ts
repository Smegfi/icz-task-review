import { ErrorHandler, inject, Injectable } from '@angular/core';
import { NGXLogger } from 'ngx-logger';
import Swal from 'sweetalert2';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

  private logger = inject(NGXLogger);

  handleError(error: Error): void {
     
    this.logger.error('Global error handler:', error);
    Swal.fire({
    title: "Error",
    text: error.message,
    icon: "error",
    confirmButtonText: "Reload",
  }).then(() => {
    window.location.reload();
    });
  }
}

