import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  error(message: string, title: string = 'Error'): void {
    Swal.fire({
      icon: 'error',
      title,
      text: message,
      confirmButtonColor: '#d33'
    });
  }

  success(message: string, title: string = 'Éxito'): void {
    Swal.fire({
      icon: 'success',
      title,
      text: message,
      confirmButtonColor: '#3085d6'
    });
  }

  info(message: string, title: string = 'Información'): void {
    Swal.fire({
      icon: 'info',
      title,
      text: message,
      confirmButtonColor: '#3085d6'
    });
  }

  confirm(
    message: string,
    title: string = 'Confirmación',
    confirmText: string = 'Sí',
    cancelText: string = 'Cancelar'
  ): Promise<boolean> {
    return Swal.fire({
      title,
      text: message,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#aaa',
      confirmButtonText: confirmText,
      cancelButtonText: cancelText
    }).then(result => result.isConfirmed);
  }
}
