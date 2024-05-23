import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subscription, debounceTime, distinctUntilChanged, of, switchMap } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { emailValidator } from './validators/email.validator';
import { emailExistsValidator } from './validators/email.exists.validator';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent implements OnInit {
  private authSubscription: Subscription | undefined;
  private checkEmailSubscription: Subscription | undefined;

  private authService: AuthService = inject(AuthService);
  private router: Router = inject(Router);

  emailExists = false;

  registrationForm!: FormGroup;
  firstName!: FormControl;
  lastName!: FormControl;
  email!: FormControl;
  password!: FormControl;
  passwordConfirmation!: FormControl;

  ngOnInit(): void {
    this.createFormControls();
    this.createForm();
    this.checkEmail();
  }

  createFormControls() {
    this.firstName = new FormControl("", Validators.required);
    this.lastName = new FormControl("");
    this.email = new FormControl("", [Validators.required, emailValidator(), emailExistsValidator(() => this.emailExists)]);
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

  checkEmail() {
    this.checkEmailSubscription = this.registrationForm?.get('email')!.valueChanges
      .pipe(
        debounceTime(1000),
        distinctUntilChanged(),
        switchMap(value => {
          if (this.registrationForm.get('email')!.valid) {
            return this.authService.checkEmail(value);
          } else {
            return of(null);
          }
        })
      ).subscribe({
        next: response => {
          this.emailExists = response ? response.exists : false;
          this.updateEmailValidators();
        },
        error: (error) => {
          if (error.status === 400) {
            alert(`Bad request: ${error.error.detail}`);
          } else if (error.status === 500) {
            alert(`Internal server error: ${error.error.detail}`)
          } else {
            alert(`An error occurred during email checking: ${error.error.detail}`);
          }
        }
      });
  }

  updateEmailValidators() {
    const emailControl = this.registrationForm.get('email');
    if (emailControl) {
      emailControl.updateValueAndValidity();
    }
  }

  register() {
    this.authSubscription = this.authService.register(this.registrationForm?.value).subscribe({
      next: obj => {
        console.log(obj);
        this.router.navigate(["auth/login"]);
      },
      error: (error) => {
        if (error.status === 400) {
          alert(`Bad request: ${error.error.detail}`);
        } else if (error.status === 500) {
          alert(`Internal server error: ${error.error.detail}`)
        } else {
          alert(`An error occurred during registration: ${error.error.detail}`);
        }
      }
    });
  }

  ngOnDestroy() {
    if (this.authSubscription) {
      this.authSubscription?.unsubscribe();
      this.checkEmailSubscription?.unsubscribe();
    }
  }
}
