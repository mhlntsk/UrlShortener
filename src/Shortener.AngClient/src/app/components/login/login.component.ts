import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule, 
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email: string = "";
  password: string = "";
  errorMessage: string = "";
  private authSubscription: Subscription | undefined;

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    this.authSubscription = this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: () => {
        this.router.navigate(["/"]);
      },
      error: (error) => {
        this.errorMessage = error.message;
      }
    });
  }

  ngOnDestroy(): void {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }
}
