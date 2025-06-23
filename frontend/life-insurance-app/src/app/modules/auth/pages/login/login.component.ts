import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, LoginRequest } from '../../services/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AlertService } from '../../../../shared/services/alert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  loginError: string | null = null;
  isLoading: boolean = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private alertService: AlertService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/)]]
    });
  }

  get email() {
    return this.loginForm.get('email')!;
  }

  get password() {
    return this.loginForm.get('password')!;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;

    this.loginError = null;
    this.isLoading = true;
    const credentials: LoginRequest = this.loginForm.value;

    this.authService.login(credentials).subscribe({
      next: () => {
        this.router.navigate(['/persons']);
      },
      error: (error: HttpErrorResponse) => {
        this.isLoading = false;
        console.log(error.error?.errors);

        if (error.status === 400 && error.error?.errors) {
          // Si la API devuelve un modelo de validación como en FluentValidation
          const errorGroups = error.error.errors; // Ej: { Password: ["msg1", "msg2"], Email: ["msg1"] }
          const messages = Object.entries(errorGroups).map(([field, errors]) => {
            const joinedErrors = (errors as string[]).join('. ') + '.';
            return `${field}: ${joinedErrors}`;
          });

          let message = messages.join('\n');
          this.alertService.error(message);
        } else if (error.status === 401 || error.status === 403) {
          this.alertService.error('Credenciales inválidas.');
        } else {
          this.alertService.error('Ocurrió un error inesperado. Intenta nuevamente.');
        }
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}
