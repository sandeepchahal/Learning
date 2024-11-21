import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { CommonModule } from '@angular/common';
import { User } from '@angular/fire/auth';
import { MyUser } from '../../models/user.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  user: MyUser | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((isAuthenticated) => {
      if (isAuthenticated) {
        this.authService.getUser().then((user) => {
          this.user = user;
          // Redirect to articles after login
          this.router.navigate(['/articles']);
        });
      }
    });
  }

  signInWithGoogle(): void {
    this.authService.signInWithGoogle().then(() => {
      // Redirect on successful login
      this.router.navigate(['/articles']);
    });
  }

  signInWithFacebook(): void {
    this.authService.signInWithFacebook().then(() => {
      // Redirect on successful login
      this.router.navigate(['/articles']);
    });
  }

  signOut(): void {
    this.authService.signOut().then(() => {
      this.user = null;
    });
  }
}
