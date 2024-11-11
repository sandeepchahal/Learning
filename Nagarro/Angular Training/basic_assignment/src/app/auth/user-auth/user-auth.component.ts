import { Component } from '@angular/core';

@Component({
  selector: 'app-user-auth',
  templateUrl: './user-auth.component.html',
  styleUrls: ['./user-auth.component.css'],
})
export class UserAuthComponent {
  mode: 'signup' | 'login' = 'signup';

  showSignup() {
    this.mode = 'signup';
  }

  showLogin() {
    this.mode = 'login';
  }
}
