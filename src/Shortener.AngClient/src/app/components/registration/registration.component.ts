import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent implements OnInit {
  private authSubscription: Subscription | undefined; //ToDo
  errorMessage: string | undefined; //ToDo
  
  private authService: AuthService = inject(AuthService);
  private router: Router = inject(Router);

  registrationForm!: FormGroup;
  firstName!: FormControl;
  lastName!: FormControl;
  email!: FormControl;
  password!: FormControl;
  passwordConfirmation!: FormControl;

  ngOnInit(): void {
    this.createFormControls();
    this.createForm();
  }

  ngOnDestroy(): void {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  createFormControls() {
    this.firstName = new FormControl("type your name...", Validators.required);
    this.lastName = new FormControl("");
    this.email = new FormControl("", [Validators.required, Validators.email]);
    this.password = new FormControl("", [Validators.required, Validators.minLength(6)]);
    this.passwordConfirmation = new FormControl("", Validators.required);
  }

  createForm() {
    this.registrationForm = new FormGroup({
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
      passwordConfirmation: this.passwordConfirmation,
    })
  }

  onSubmit() {
    if (this.registrationForm?.valid) {
      this.register();
    }
  }

  register(): void {
    this.authSubscription = this.authService.register(this.registrationForm?.value).subscribe({
      next: () => {
        this.router.navigate(["/"]);
      },
      error: (error) => {
        this.errorMessage = error.message;
      }
    });
  }
}
