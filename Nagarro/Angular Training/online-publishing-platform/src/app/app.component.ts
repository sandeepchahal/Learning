import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
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
  userName: string | null = null; // To store the logged-in user's name
  isAdmin: boolean = false; // To track if the user is an admin

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    // Listen for authentication state changes
    this.authService.authStateChanged.subscribe((user) => {
      if (user) {
        this.userName = user.displayName;
        console.log('User already logged in', this.userName);

        // Check if the user is an admin (or any other role we need)
        this.isAdmin = user.isAdmin || false; // Update based on user data
        this.isLoggedIn = true;
      } else {
        this.isLoggedIn = false;
        this.isAdmin = false; // Reset admin status if no user is logged in
        this.userName = null;
      }
    });
  }
  logout() {
    this.authService.signOut();
    this.router.navigate(['/articles']);
  }
}
