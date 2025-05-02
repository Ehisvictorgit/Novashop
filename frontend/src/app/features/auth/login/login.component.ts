import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  standalone: false,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  formSubmitted = false;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required], // Asegúrate de que el nombre sea 'username'
      password: ['', Validators.required],  // Asegúrate de que el nombre sea 'password'
    });
  }

  shouldShowError(controlName: string): boolean {
    const control = this.loginForm.get(controlName);
    return control ? control.invalid && (control.touched || this.formSubmitted) : false;
  }

  onSubmit() {
    this.formSubmitted = true;

    // Verifica que los valores de 'username' y 'password' no sean nulos
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

      console.log('Enviando login con:', { username, password });

      this.auth.login(username, password).subscribe({
        next: () => this.router.navigate(['/shop']),
        error: () => (this.errorMessage = 'Credenciales inválidas.'),
      });
    }
  }
}
