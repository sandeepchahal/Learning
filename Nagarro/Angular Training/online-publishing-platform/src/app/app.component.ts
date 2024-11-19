import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule], // Import RouterModule for routing
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  isLoggedIn = false; // To track the login state
  userName: any; // To store the logged-in user's name
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((isAuthenticated) => {
      if (isAuthenticated) {
        this.authService.getUser().then((user) => {
          this.userName = user?.displayName;
          console.log('user alreayd logged in ', this.userName);
          this.isLoggedIn = !this.isLoggedIn;
        });
      }
    });
  }
}
