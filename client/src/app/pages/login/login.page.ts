import { Component, inject, InjectionToken } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { AuthApiService } from '../../api-client';
import { Router } from '@angular/router';
import AccountService from '../../services/account.service';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

export interface Login {
  login: string;
  password: string;
}

export interface LoginBehavior {

  autoLogins: Login[];


}


export const LOGIN_BEHAVIOR = new InjectionToken<LoginBehavior>('LOGIN_BEHAVIOR');


@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    MatAutocompleteModule
  ],
  templateUrl: './login.page.html',
  styleUrl: './login.page.scss'
})
export class LoginPage {

  private authApi = inject(AuthApiService);
  private accountService = inject(AccountService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  public behavior = inject(LOGIN_BEHAVIOR);

  error = '';
  form: FormGroup;

  constructor() {

    this.form = this.fb.group({
      login: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });

  }


  onSubmit() {
    if (this.form.valid) {

      this.error = '';

      this.authApi.apiAuthLoginPost({ login: this.form.value.login, password: this.form.value.password }).pipe(
      ).subscribe({
        next: async (data) => {

          localStorage.setItem('jwt', data.token!);

          await this.accountService.identity()

          this.router.navigate(['/backend/task']);



        },
        error: (err) => {
          if (err.status === 401) {
            this.error = err.error?.message;
          }
        }
      });
    }
  }



  onLoginSelected(event: MatAutocompleteSelectedEvent) {
    const selectedLogin = event.option.value;
    const password = this.behavior.autoLogins.find(login => login.login === selectedLogin)!.password;
    this.form.patchValue({ password });
  }

}
