import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  username = '';
  password = '';
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, @Inject(PLATFORM_ID) private platformId: object) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    this.authService.login(this.username, this.password).subscribe(
      (response) => {
        this.router.navigate(['/carModel']); // Redirect to dashboard after login
      },
      (error) => {
        this.errorMessage = 'Invalid credentials'; // Show error message on failed login
      }
    );
  }
}
