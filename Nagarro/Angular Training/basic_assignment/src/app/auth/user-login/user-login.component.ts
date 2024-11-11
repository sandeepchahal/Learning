import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './User-login.component.html',
  styleUrls: ['./User-login.component.css'],
})
export class UserLoginComponent {
  email: string = '';
  password: string = '';
  loginMessage: string = '';

  constructor(private authService: AuthService) {}

  // Method to handle login form submission
  onSubmit(): void {
    const isLoggedIn = this.authService.login(this.email, this.password);
    if (isLoggedIn) {
      this.loginMessage = 'Login successful!';
    } else {
      this.loginMessage = 'Invalid email or password!';
    }
  }
}
