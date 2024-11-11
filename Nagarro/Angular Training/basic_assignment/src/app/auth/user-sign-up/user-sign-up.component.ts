import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './user-sign-up.component.html',
  styleUrls: ['./user-sign-up.component.css'],
})
export class UserSignUpComponent {
  user: User = new User('', '', ''); // Initialize an empty user object
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  // Method to handle form submission
  onSubmit(signUpForm: NgForm): void {
    if (signUpForm.invalid) {
      // If the form is invalid, do not proceed with the submission
      return;
    }

    // Check if password and confirm password match
    if (this.user.password !== this.user.confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      this.successMessage = '';
      return;
    }

    const success = this.authService.signUp(this.user);
    if (success) {
      this.successMessage = 'User signed up successfully!';
      this.errorMessage = '';
      this.user = new User('', '', ''); // Reset form
      this.navigateToHome();
    } else {
      this.errorMessage = 'Email is already in use.';
      this.successMessage = '';
    }
  }
  private navigateToHome() {
    this.router.navigate(['/']);
  }
}
